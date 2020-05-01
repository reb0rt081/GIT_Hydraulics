using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ScienceAndMaths.Client.Core.Helpers;
using ScienceAndMaths.Client.Modules.Canal.Ribbon;
using ScienceAndMaths.Client.Modules.Canal.Views;
using ScienceAndMaths.Client.Shared;
using Unity;

namespace ScienceAndMaths.Client.Modules.Canal.ViewModels
{
    public class LocationViewModel : BindableBase, INavigationAware, ILocationViewModel
    {
        public DelegateCommand<string> LocationEnteredCommand { get; set; }

        [InjectionMethod]
        public void Initialize()
        {
            LocationEnteredCommand = new DelegateCommand<string>(OnLocationEnteredCommandExecuted);
        }

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        private void OnLocationEnteredCommandExecuted(string obj)
        {
            RegionManager.NavigateToViewAndRibbon(Shared.Constants.MainRegion, typeof(CanalView).Name);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //  Navigation to the required Ribbon
            RegionManager.RequestNavigate(Constants.RibbonRegion, Constants.LocationRibbon);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
