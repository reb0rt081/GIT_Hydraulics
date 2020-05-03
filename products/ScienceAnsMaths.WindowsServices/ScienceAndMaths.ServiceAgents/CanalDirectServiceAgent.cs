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

        public async Task<CanalSimulationResult> ExecuteCanalSimulationAsync()
        {
            // Normally use correlation Ids to correlate messages and async methods
            return await CanalFlowService.ExecuteCanalSimulationAsync();

            //  Normally the direct service agent will register to event in CanalFlowService when simulation is completed.
            //  It will then check the correlation id provided in the event and find the executing task that is related to this correlation id, and set the result
        }
    }
}
