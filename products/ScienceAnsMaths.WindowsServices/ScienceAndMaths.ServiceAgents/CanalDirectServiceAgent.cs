using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Shared.Canals;
using Unity;

namespace ScienceAndMaths.ServiceAgents
{
    public class CanalDirectServiceAgent : ICanalServiceAgent
    {
        [Dependency]
        public ICanalFlowService CanalFlowService { get; set; }

        public CanalSimulationResult ExecuteCanalSimulation()
        {
            // Normally use correlation Ids to correlate messages
            return CanalFlowService.ExecuteCanalSimulation();
        }
    }
}
