using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Prism.Commands;
using Prism.Mvvm;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Client.Modules.Canal.ViewModels
{
    public interface ICanalViewModel
    {
        DelegateCommand<string> LoadCanalCommand { get; set; }

        DelegateCommand<string> SimulateCanalCommand { get; set; }

        CanalData CanalData { get; set; }

        ICanalStretch ActiveCanalStretch { get; set; }
    }
}
