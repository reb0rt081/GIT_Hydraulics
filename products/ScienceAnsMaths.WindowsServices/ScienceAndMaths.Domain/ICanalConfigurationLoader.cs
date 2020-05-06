using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Domain
{
    public interface ICanalConfigurationLoader
    {
        void LoadCanalConfiguration();

        ICanal LoadCanalConfiguration(string configurationLocation);

        void SaveCanalConfiguration(Canal canal);

        void SaveCanalConfiguration(Canal canal, string saveLocation);
    }
}
