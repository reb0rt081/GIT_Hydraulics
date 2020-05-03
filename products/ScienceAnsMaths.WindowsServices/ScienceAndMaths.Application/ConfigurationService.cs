using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ScienceAndMaths.Application
{
    public class ConfigurationService : IConfigurationService
    {
        [Dependency]
        public ICanalConfigurationLoader ConfigurationLoader { get; set; }
        public Canal LoadCanalConfiguration(string file)
        {
            return ConfigurationLoader.LoadCanalConfiguration(file);
        }
    }
}
