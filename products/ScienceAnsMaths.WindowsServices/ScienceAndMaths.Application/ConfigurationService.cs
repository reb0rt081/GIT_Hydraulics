using ScienceAndMaths.Domain;
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
        public void LoadCanalConfiguration(string file)
        {
            ConfigurationLoader.LoadCanalConfiguration(file);
        }
    }
}
