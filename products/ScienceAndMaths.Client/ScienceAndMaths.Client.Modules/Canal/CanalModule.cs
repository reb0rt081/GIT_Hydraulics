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
            this.RegisterViewAndViewModelInRegionAndContainer<LocationView, ICanalViewModel, CanalViewModel>(new CanalViewModel(), Shared.Constants.MainRegion, Shared.Constants.LocationView);

            this.RegisterViewInRegionAndContainer<CanalView>(Shared.Constants.MainRegion,
                Shared.Constants.CanalView);

            this.RegisterViewInRegionAndContainer<CanalRibbon>(Shared.Constants.RibbonRegion, Shared.Constants.CanalRibbon);
        }
    }
}
