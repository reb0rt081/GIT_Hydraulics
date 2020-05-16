using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Client.Modules.Canal.Views;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ScienceAndMaths.Client.Shared;
using ScienceAndMaths.Shared.Canals;
using Unity;
using Microsoft.Win32;
using ScienceAndMaths.ServiceAgents;

namespace ScienceAndMaths.Client.Modules.Canal.ViewModels
{
    public class CanalViewModel : ScienceAndMathsViewModel, INavigationAware, ICanalViewModel
    {
        #region Constructor and Initialize

        [Dependency]
        public IConfigurationServiceAgent ConfigurationServiceAgent { get; set; }

        [Dependency]
        public ICanalServiceAgent CanalServiceAgent { get; set; }

        public CanalViewModel()
        {
        }

        [InjectionMethod]
        public void Initialize()
        {
            LoadCanalCommand = new DelegateCommand<string>(OnLoadCanalCommandExecuted);
            SimulateCanalCommand = new DelegateCommand<string>(OnSimulateCanalCommandExecuted, OnSimulateCanalCommandCanExecute);
        }

        private bool OnSimulateCanalCommandCanExecute(string arg)
        {
            return Canal != null;
        }

        private void OnSimulateCanalCommandExecuted(string obj)
        {
            Task<CanalSimulationResult> task = CanalServiceAgent.ExecuteCanalSimulationAsync();

            task.Wait();

            CanalResult = task.Result;

            RaisePropertiesChanged();
        }

        #endregion

        #region Commands

        public DelegateCommand<string> LoadCanalCommand { get; set; }
        public DelegateCommand<string> SimulateCanalCommand { get; set; }

        #endregion

        #region Properties

        public ICanal Canal { get; set; }

        public CanalSimulationResult CanalResult { get; set; }

        #endregion

        #region Private methods

        private void OnLoadCanalCommandExecuted(string obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // C:\Users\rbo\Documents\ScienceAndMaths
                string file = openFileDialog.FileName;

                Canal = ConfigurationServiceAgent.LoadCanalConfiguration(file);

                RaisePropertiesChanged();
            }                
        }

        private void RaisePropertiesChanged()
        {
            RaisePropertyChanged(nameof(Canal));
            RaisePropertyChanged(nameof(CanalResult));
            LoadCanalCommand.RaiseCanExecuteChanged();
            SimulateCanalCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //  Navigation to the required Ribbon
            //RegionManager.RequestNavigate(Constants.RibbonRegion, Constants.CanalRibbon);
            RaisePropertiesChanged();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        #endregion

    }
}
