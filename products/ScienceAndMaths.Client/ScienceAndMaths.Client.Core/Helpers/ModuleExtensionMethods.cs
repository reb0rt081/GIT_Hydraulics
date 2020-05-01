using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Client.Shared;

using Unity.Lifetime;
using Unity;

namespace ScienceAndMaths.Client.Core.Helpers
{
    public static class ModuleExtensionMethods
    {
        public static void RegisterViewInRegion<T>(this ScienceAndMathsModule ScienceAndMathsModule, string regionName, string viewName)
        {
            ScienceAndMathsModule.RegionManager.Regions[regionName].Add(ScienceAndMathsModule.Container.Resolve(typeof(T), viewName), viewName);
        }

        public static void RegisterViewInRegionAndContainer<T>(this ScienceAndMathsModule ScienceAndMathsModule, string regionName,
            string viewName)
        {
            ScienceAndMathsModule.Container.RegisterType(typeof(T), typeof(T), viewName, new ContainerControlledLifetimeManager());

            RegisterViewInRegion<T>(ScienceAndMathsModule, regionName, viewName);
        }

        public static void RegisterViewAndViewModelInRegionAndContainer<T1, T2, T3>(this ScienceAndMathsModule ScienceAndMathsModule, T3 viewModel, string regionName, string viewName) where T3 : T2
        {
            ScienceAndMathsModule.Container.RegisterInstance<T2>(viewModel, new ContainerControlledLifetimeManager());
            ScienceAndMathsModule.Container.BuildUp(viewModel);

            RegisterViewInRegionAndContainer<T1>(ScienceAndMathsModule, regionName, viewName);
        }
    }
}
