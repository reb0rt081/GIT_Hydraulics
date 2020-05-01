using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Client.Core;
using ScienceAndMaths.Client.Core.Helpers;
using ScienceAndMaths.Client.Modules.Canal.Ribbon;
using ScienceAndMaths.Client.Modules.Canal.ViewModels;
using ScienceAndMaths.Client.Modules.Canal.Views;
using ScienceAndMaths.Client.Shared;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;

using Unity;
using Unity.Lifetime;

namespace ScienceAndMaths.Client.Modules.Canal
{
    public class CanalModule : ScienceAndMathsModule
    {
        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CanalRibbon>();
            containerRegistry.RegisterForNavigation<CanalView>();
            containerRegistry.RegisterForNavigation<LocationView>();
        }

        public override ScienceAndMathsModuleInfo GetModuleInfo()
        {
            return new ScienceAndMathsModuleInfo()
            {
                Name = "Canals",
                MainViewUri = typeof(LocationView).Name,
                ImageUri = "pack://application:,,,/ScienceAndMaths.Client.Shared;component/Images/canal_icon.png"
            };
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            this.RegisterViewAndViewModelInRegionAndContainer<LocationView, ILocationViewModel, LocationViewModel>(new LocationViewModel(), Shared.Constants.MainRegion, Shared.Constants.LocationView);

            this.RegisterViewAndViewModelInRegionAndContainer<CanalView, ICanalViewModel, CanalViewModel>(new CanalViewModel(), Shared.Constants.MainRegion, Shared.Constants.CanalView);

            // TODO be able to update ribbon upon navigation to main view

            ViewModelLocationProvider.Register(typeof(LocationRibbon).ToString(), () => Container.Resolve<ILocationViewModel>());

            this.RegisterViewInRegionAndContainer<LocationRibbon>(Shared.Constants.RibbonRegion, Shared.Constants.LocationRibbon);

            ViewModelLocationProvider.Register(typeof(CanalRibbon).ToString(), () => Container.Resolve<ICanalViewModel>());

            this.RegisterViewInRegionAndContainer<CanalRibbon>(Shared.Constants.RibbonRegion, Shared.Constants.CanalRibbon);
        }
    }
}
