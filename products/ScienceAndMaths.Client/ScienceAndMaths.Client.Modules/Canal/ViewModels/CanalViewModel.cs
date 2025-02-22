﻿using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Regions;
using ScienceAndMaths.Client.Shared;
using ScienceAndMaths.Shared.Canals;
using Unity;
using Microsoft.Win32;
using ScienceAndMaths.ServiceAgents;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Client.Modules.Canal.ViewModels
{
    public class CanalViewModel : ScienceAndMathsViewModel, INavigationAware, ICanalViewModel
    {
        #region

        private CanalGeometryData activeCanalGeometryData;

        #endregion

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

        #endregion

        #region Commands

        public DelegateCommand<string> LoadCanalCommand { get; set; }
        public DelegateCommand<string> SimulateCanalCommand { get; set; }

        #endregion

        #region Properties

        public CanalData CanalData { get; set; }

        public CanalGeometryData ActiveCanalGeometryData
        {
            get { return activeCanalGeometryData; }
            set
            {
                activeCanalGeometryData = value;
                RaisePropertiesChanged();
            }
        }

        #endregion

        #region Private methods

        private bool OnSimulateCanalCommandCanExecute(string arg)
        {
            return CanalData != null;
        }

        private void OnSimulateCanalCommandExecuted(string obj)
        {
            Task<CanalData> task = CanalServiceAgent.ExecuteCanalSimulationAsync();

            task.Wait();

            CanalData = task.Result;

            ActiveCanalGeometryData = CanalData.GetCanalGeometryData(CanalData.Canal.CanalStretches.Select(cs => cs.Id).FirstOrDefault());

            RaisePropertiesChanged();
        }

        private void OnLoadCanalCommandExecuted(string obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // C:\Users\rbo\Documents\ScienceAndMaths
                string file = openFileDialog.FileName;

                ICanal canal = ConfigurationServiceAgent.LoadCanalConfiguration(file);

                CanalData = new CanalData(canal);

                ActiveCanalGeometryData = CanalData.GetCanalGeometryData(CanalData.Canal.CanalStretches.Select(cs => cs.Id).FirstOrDefault());

                RaisePropertiesChanged();
            }                
        }

        private void RaisePropertiesChanged()
        {
            RaisePropertyChanged(nameof(CanalData));
            RaisePropertyChanged(nameof(ActiveCanalGeometryData));
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
