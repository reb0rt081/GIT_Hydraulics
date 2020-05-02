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
using Unity;
using Microsoft.Win32;

namespace ScienceAndMaths.Client.Modules.Canal.ViewModels
{
    public class CanalViewModel : ScienceAndMathsViewModel, INavigationAware, ICanalViewModel
    {
        #region Constructor and Initialize

        public CanalViewModel()
        {
            ResetView();
        }

        [InjectionMethod]
        public void Initialize()
        {
            LoadCanalCommand = new DelegateCommand<string>(OnLoadCanalCommandExecuted);
            LocationEnteredCommand = new DelegateCommand<string>(OnLocationEnteredCommandExecuted);
            BarcodeEnteredCommand = new DelegateCommand<string>(OnBarcodeEnteredCommandExecuted);
            ConfirmPickCommand = new DelegateCommand(OnPickConfirmCommandExecuted, OnPickConfirmCommandCanExecute);
            IncreaseQuantityCommand = new DelegateCommand(OnIncreaseQuantityCommandExecuted, OnIncreaseQuantityCommandCanExecute);
            DecreaseQuantityCommand = new DelegateCommand(OnDecreaseQuantityCommandExecuted, OnDecreaseQuantityCommandCanExecute);
        }

        #endregion

        #region Commands

        public DelegateCommand<string> LoadCanalCommand { get; set; }

        public DelegateCommand<string> LocationEnteredCommand { get; set; }

        public DelegateCommand<string> BarcodeEnteredCommand { get; set; }

        public DelegateCommand ConfirmPickCommand { get; set; }

        public DelegateCommand IncreaseQuantityCommand { get; set; }

        public DelegateCommand DecreaseQuantityCommand { get; set; }

        #endregion

        #region Properties

        public string ItemImagePath { get; set; }

        public int QuantitySelected { get; set; }
        
        #endregion

        #region Private methods

        private void ResetView()
        {
            ItemImagePath = @"pack://application:,,,/ScienceAndMaths.Client.Shared;component/Images/questionMark.png";
            QuantitySelected = 0;
        }

        private void OnLoadCanalCommandExecuted(string obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string file = openFileDialog.FileName;
            }                
        }

        private void OnBarcodeEnteredCommandExecuted(string barcode)
        {
            if (IncreaseQuantityCommand.CanExecute())
            {
                    IncreaseQuantityCommand.Execute();
            }

            RaisePropertiesChanged();
        }

        private void OnLocationEnteredCommandExecuted(string barcode)
        {
            if (!string.IsNullOrEmpty(barcode))
            {
                RaisePropertiesChanged();
                
                RegionManager.RequestNavigate(Shared.Constants.MainRegion, typeof(CanalView).Name);
            }
        }

        private void OnIncreaseQuantityCommandExecuted()
        {
            QuantitySelected++;
            RaisePropertiesChanged();
        }

        private void OnDecreaseQuantityCommandExecuted()
        {
            QuantitySelected--;
            RaisePropertiesChanged();
        }

        private bool OnDecreaseQuantityCommandCanExecute()
        {
            return QuantitySelected > 0;
        }

        private bool OnIncreaseQuantityCommandCanExecute()
        {
            return true;
        }

        private void OnPickConfirmCommandExecuted()
        {
            ResetView();

            RaisePropertiesChanged();

            RegionManager.RequestNavigate(Shared.Constants.MainRegion, typeof(LocationView).Name);
        }

        private bool OnPickConfirmCommandCanExecute()
        {
            return QuantitySelected > 0;
        }

        private void RaisePropertiesChanged()
        {
            RaisePropertyChanged(nameof(ItemImagePath));
            RaisePropertyChanged(nameof(QuantitySelected));
            DecreaseQuantityCommand.RaiseCanExecuteChanged();
            IncreaseQuantityCommand.RaiseCanExecuteChanged();
            ConfirmPickCommand.RaiseCanExecuteChanged();
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
