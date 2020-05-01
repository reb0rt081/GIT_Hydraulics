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
        public static void RegisterViewInRegion<T>(this ScienceAndMathsModule scienceAndMathsModule, string regionName, string viewName)
        {
            scienceAndMathsModule.RegionManager.Regions[regionName].Add(scienceAndMathsModule.Container.Resolve(typeof(T), viewName), viewName);
        }

        public static void RegisterViewInRegionAndContainer<T>(this ScienceAndMathsModule scienceAndMathsModule, string regionName,
            string viewName)
        {
            scienceAndMathsModule.Container.RegisterType(typeof(T), typeof(T), viewName, new ContainerControlledLifetimeManager());

            RegisterViewInRegion<T>(scienceAndMathsModule, regionName, viewName);
        }

        public static void RegisterViewAndViewModelInRegionAndContainer<T1, T2, T3>(this ScienceAndMathsModule scienceAndMathsModule, T3 viewModel, string regionName, string viewName) where T3 : T2
        {
            scienceAndMathsModule.Container.RegisterInstance<T2>(viewModel, new ContainerControlledLifetimeManager());
            scienceAndMathsModule.Container.BuildUp(viewModel);

            RegisterViewInRegionAndContainer<T1>(scienceAndMathsModule, regionName, viewName);
        }

        public static void RegisterViewAndViewModelInRegionAndRibbonAndContainer<T1, T2, T3, T4>(this ScienceAndMathsModule scienceAndMathsModule, T3 viewModel, T4 ribbon, string regionName, string viewName, string ribbonName) where T3 : T2
        {
            scienceAndMathsModule.Container.RegisterInstance<T2>(viewModel, new ContainerControlledLifetimeManager());
            scienceAndMathsModule.Container.BuildUp(viewModel);

            RegisterViewInRegionAndContainer<T1>(scienceAndMathsModule, regionName, viewName);
            RegisterViewInRegionAndContainer<T4>(scienceAndMathsModule, Constants.RibbonRegion, ribbonName);

            RegionManagerExtensionMethods.RegistrationMapping.Add(viewName, ribbonName);
        }
    }
}
