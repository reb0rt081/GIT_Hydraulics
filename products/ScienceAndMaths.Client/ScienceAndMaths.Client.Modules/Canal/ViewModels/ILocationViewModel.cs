using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace ScienceAndMaths.Client.Modules.Canal.ViewModels
{
    public interface ILocationViewModel
    {
        DelegateCommand<string> LocationEnteredCommand { get; set; }
    }
}
