using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Regions;
using ScienceAndMaths.Client.Modules.Canal.Views;
using Unity;

namespace ScienceAndMaths.Client.Modules.Canal.ViewModels
{
    public class LocationViewModel : ILocationViewModel
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
            RegionManager.RequestNavigate(Shared.Constants.MainRegion, typeof(CanalView).Name);
        }
    }
}
