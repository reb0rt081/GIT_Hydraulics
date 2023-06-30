using ScienceAndMaths.Hydraulics.Canals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Domain
{
    public interface IConfigurationService
    {
        ICanal LoadCanalConfiguration(string file);
    }
}
