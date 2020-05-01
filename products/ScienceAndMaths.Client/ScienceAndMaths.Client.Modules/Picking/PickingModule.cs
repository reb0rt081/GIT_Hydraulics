using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Client.Core;
using ScienceAndMaths.Client.Core.Helpers;
using ScienceAndMaths.Client.Modules.Picking.Ribbon;
using ScienceAndMaths.Client.Modules.Picking.ViewModels;
using ScienceAndMaths.Client.Modules.Picking.Views;
using ScienceAndMaths.Client.Shared;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

using Unity;
using Unity.Lifetime;

namespace ScienceAndMaths.Client.Modules.Picking
{
    public class PickingModule : ScienceAndMathsModule
    {
        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PickingRibbon>();
            containerRegistry.RegisterForNavigation<PickingView>();
            containerRegistry.RegisterForNavigation<LocationView>();
        }

        public override ScienceAndMathsModuleInfo GetModuleInfo()
        {
            return new ScienceAndMathsModuleInfo()
            {
                Name = "Picking",
                MainViewUri = typeof(LocationView).Name,
                ImageUri = "pack://application:,,,/ScienceAndMaths.Client.Shared;component/Images/picking_icon.png"
            };
        }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            this.RegisterViewAndViewModelInRegionAndContainer<LocationView, IPickingViewModel, PickingViewModel>(new PickingViewModel(), Shared.Constants.MainRegion, Shared.Constants.LocationView);

            this.RegisterViewInRegionAndContainer<PickingView>(Shared.Constants.MainRegion,
                Shared.Constants.PickingView);

            this.RegisterViewInRegionAndContainer<PickingRibbon>(Shared.Constants.RibbonRegion, Shared.Constants.PickingRibbon);
        }
    }
}
