

#region usings

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;

#endregion

namespace ScienceAndMaths.Common
{

    /// <summary>
    ///     Provides tools to extend the default CLR Assembly loading behavior to allow loading of assemblies in a different
    ///     version.    
    /// </summary>
    public static class AssemblyResolveExtensions
    {
        #region Constants and Fields

        private static readonly List<string> processed = new List<string>();

        private static string[] directories = new string[0];

        public static bool ResolveEnabled = true;

        #endregion

        static AssemblyResolveExtensions()
        {
            // We need to add the directory where the application runs with all its binaries
            TryAddDirectory("D:\\rbo\\Documents");
        }
        /// <summary>
        /// Tries to add a directory to the assembly resolve locations.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>true if the directory was added.</returns>
        public static bool TryAddDirectory(string dir)
        {
            if (string.IsNullOrEmpty(dir))
            {
                return false;
            }
            if (Directory.Exists(dir))
            {
                lock (processed)
                {
                    if (directories.Contains(dir))
                    {
                        return false;
                    }
                    else
                    {
                        directories = directories.Add(dir);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///     Returns a new array consisting of the source array contents plus a new item.
        ///     If the source array is null, a new array consisting only of the item to append is returned.
        /// </summary>
        [Pure]
        public static T[] Add<T>(this T[] sourceArray, T append)
        {
            if (sourceArray == null || sourceArray.Length == 0)
            {
                return new[] { append };
            }
            else
            {
                T[] newArray = new T[sourceArray.Length + 1];
                Array.Copy(sourceArray, newArray, sourceArray.Length);
                newArray[sourceArray.Length] = append;
                return newArray;
            }
        }

        /// <summary>
        /// Call Enable.
        /// </summary>
        [Obsolete("Refactored -> will be removed in Sprint 10 -> call enable")]
        public static void ResolveByNameAndIgnoreVersion()
        {
            Enable();
        }
        
        /// <summary>
        /// Disables the assembly resolve extension
        /// </summary>
        public static void Disable()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= ResolveByNameAndIgnoreVersion;
        }
        /// <summary>
        /// Enables the assembly resolve extension
        /// </summary>
        public static void Enable()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= ResolveByNameAndIgnoreVersion;
            AppDomain.CurrentDomain.AssemblyResolve += ResolveByNameAndIgnoreVersion;
        }
        #region Event Handlers

        private static Assembly ResolveByNameAndIgnoreVersion(object sender, ResolveEventArgs args)
        {
            string prefix = "Try resolve unknown assembly";
            try
            {
                if (!ResolveEnabled)
                {
                    //disabled - return;
                    return null;
                }

                //we just try to resolve the name to the current bin directory, ignoring versions.
                AssemblyName requestedAssemblyName = new AssemblyName(args.Name);

                prefix = $"TryResolve assembly {requestedAssemblyName.Name} v{requestedAssemblyName.Version} :";
                bool isRessource = requestedAssemblyName.Name.EndsWith(".resources", StringComparison.InvariantCultureIgnoreCase);

                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var loadedName = assembly.GetName();
                    if (loadedName.Name == requestedAssemblyName.Name)
                    {
                        if (isRessource)
                        {
                            if (Equals(loadedName.GetTwoLetterCulture(), requestedAssemblyName.GetTwoLetterCulture()))
                            {
                                return assembly;
                            }
                        }
                        else
                        {
                            return assembly;
                        }
                    }
                }

                lock (processed)
                {
                    //remember the current assembly to prevent stackoverflow
                    var reentryName = requestedAssemblyName.Name + "---" + requestedAssemblyName.GetTwoLetterCulture();
                    if (processed.Contains(reentryName))
                    {
                        return null;
                    }

                    processed.Add(reentryName);
                }

                foreach (var directory in directories)
                {
                    var searchPattern = $"{requestedAssemblyName.Name}.dll";
                    var possibleFiles = Directory.GetFiles(directory, searchPattern, SearchOption.AllDirectories);

                    if (isRessource)
                    {
                        //Can't make Directory.GetFiles to search for files in a specific directory like start in C:\Temp but in there it has to be 'de\sample.resources.dll'
                        //crappy filter (for now?) : search filespecific and filter results with string compare...

                        if (possibleFiles.Length > 0)
                        {
                            var langSpec = $"{requestedAssemblyName.GetTwoLetterCulture()}\\{searchPattern}";
                            possibleFiles = possibleFiles.Where(f => f.EndsWith(langSpec, StringComparison.InvariantCultureIgnoreCase)).ToArray();
                        }
                    }

                    if (TryGetFile(possibleFiles, prefix, searchPattern, out var resolvedFileName))
                    {
                        if (File.Exists(resolvedFileName))
                        {
                            //see the following link for allowed methods we can use:
                            //https://msdn.microsoft.com/en-us/library/ff527268.aspx
                            //other api's (like Assembly.LoadFrom(filename) would raise this event again -> Stackoverflow
                            try
                            {
                                var resolvedAssembly = Assembly.LoadFile(resolvedFileName);
                                var resolvedAssemblyName = resolvedAssembly.GetName();

                                string infoMessage = $"{prefix} Try use ";

                                if (resolvedAssemblyName.Version != requestedAssemblyName.Version)
                                {

                                    infoMessage += $"version {resolvedAssemblyName.Version} ";
                                    var attrib = resolvedAssembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
                                    if (attrib != null)
                                    {
                                        if (resolvedAssemblyName.Version.ToString() != attrib.Version)
                                        {
                                            infoMessage += $"(File:{attrib.Version}) ";
                                        }
                                    }
                                }

                                infoMessage += $"from {resolvedAssembly.CodeBase}.";

                                

                                return resolvedAssembly;
                            }
                            catch (Exception ex)
                            {
                                Console.Write($"{prefix} Can't load assembly {resolvedFileName} : {ex}");
                            }
                        }
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal static bool TryGetFile(string[] possibleFiles, string prefix, string pattern, out string resolvedFileName)
        {
            if (possibleFiles.Length == 0)
            {
                resolvedFileName = null;

                return false;
            }

            if (possibleFiles.Length == 1)
            {
                resolvedFileName = possibleFiles[0];
                return true;
            }

            resolvedFileName = possibleFiles.OrderBy(f => f.Count(c => c == '\\')).ThenBy(f => f).First();
            return true;
        }

        #endregion

        private static string GetTwoLetterCulture(this AssemblyName name)
        {
            return name?.CultureInfo?.IetfLanguageTag ?? "NONE";
        }
    }
}
