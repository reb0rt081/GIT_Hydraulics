using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Hydraulics.Canals;

namespace ScienceAndMaths.Domain
{
    public interface ICanalConfigurationLoader
    {
        void LoadCanalConfiguration();

        Canal LoadCanalConfiguration(string configurationLocation);

        void SaveCanalConfiguration(Canal canal);

        void SaveCanalConfiguration(Canal canal, string saveLocation);
    }
}
