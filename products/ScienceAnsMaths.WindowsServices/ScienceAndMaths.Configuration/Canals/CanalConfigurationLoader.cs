using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;

namespace ScienceAndMaths.Configuration.Canals
{
    public class CanalConfigurationLoader : ICanalConfigurationLoader
    {
        public Canal LoadCanalConfiguration(string configurationLocation)
        {
            return new Canal();
        }
    }
}
