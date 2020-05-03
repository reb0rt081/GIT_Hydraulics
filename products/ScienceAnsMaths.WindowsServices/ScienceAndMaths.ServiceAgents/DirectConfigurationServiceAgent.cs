using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;
using Unity;

namespace ScienceAndMaths.ServiceAgents
{
    public class DirectConfigurationServiceAgent : IConfigurationServiceAgent
    {
        [Dependency]
        public IConfigurationService ConfigurationService { get; set; }

        public Canal LoadCanalConfiguration(string file)
        {
            // Normally use correlation Ids to correlate messages and async methods
            return ConfigurationService.LoadCanalConfiguration(file);
        }
    }
}
