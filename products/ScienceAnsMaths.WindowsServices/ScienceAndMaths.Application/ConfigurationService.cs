using ScienceAndMaths.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared.Canals;

using Unity;

namespace ScienceAndMaths.Application
{
    public class ConfigurationService : IConfigurationService
    {
        [Dependency]
        public ICanalConfigurationLoader ConfigurationLoader { get; set; }
        public ICanal LoadCanalConfiguration(string file)
        {
            return ConfigurationLoader.LoadCanalConfiguration(file);
        }
    }
}
