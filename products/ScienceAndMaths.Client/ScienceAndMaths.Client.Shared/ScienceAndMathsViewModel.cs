using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ScienceAndMaths.Client.Shared
{
    public class ScienceAndMathsViewModel : BindableBase
    {
        public DelegateCommand NavigateBackCommand { get; set; }

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public ScienceAndMathsViewModel()
        {
            NavigateBackCommand = new DelegateCommand(OnNavigateBackCommandExecuted);
        }

        protected virtual void OnNavigateBackCommandExecuted()
        {
            RegionManager.Regions[Constants.MainRegion].NavigationService.Journal.GoBack();
            RegionManager.Regions[Constants.RibbonRegion].NavigationService.Journal.GoBack();
        }
    }

}
