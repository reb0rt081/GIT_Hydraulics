using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace ScienceAndMaths.Client.Shared
{
    public abstract class ScienceAndMathsModule : IModule
    {
        public ScienceAndMathsModule()
        {
            NavigateBackCommand = new DelegateCommand(OnNavigateBackCommandExecuted);
        }

        protected virtual void OnNavigateBackCommandExecuted()
        {
            RegionManager.Regions[Constants.MainRegion].NavigationService.Journal.GoBack();
        }

        public DelegateCommand NavigateBackCommand { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public abstract void RegisterTypes(IContainerRegistry containerRegistry);

        public abstract ScienceAndMathsModuleInfo GetModuleInfo();

        public abstract void OnInitialized(IContainerProvider containerProvider);
    }
}
