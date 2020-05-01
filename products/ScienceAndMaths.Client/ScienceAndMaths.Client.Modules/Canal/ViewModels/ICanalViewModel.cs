using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Prism.Commands;
using Prism.Mvvm;

namespace ScienceAndMaths.Client.Modules.Canal.ViewModels
{
    public interface ICanalViewModel
    {
        DelegateCommand<string> LocationEnteredCommand { get; set; }

        DelegateCommand<string> BarcodeEnteredCommand { get; set; }

        DelegateCommand ConfirmPickCommand { get; set; }

        DelegateCommand IncreaseQuantityCommand { get; set; }

        DelegateCommand DecreaseQuantityCommand { get; set; }

        
        string ItemImagePath { get; set; }

        int QuantitySelected { get; set; }
    }
}
