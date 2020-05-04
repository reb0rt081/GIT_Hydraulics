using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Hydraulics.Canals;
using ScienceAndMaths.Shared.Canals;
using Unity;

namespace ScienceAndMaths.Application
{
    public class CanalFlowService : ICanalFlowService
    {
        [Dependency]
        public ICanalManager CanalManager { get; set; }

        public async Task<CanalSimulationResult> ExecuteCanalSimulationAsync()
        {
            //  Make sure here we control how many request and threads can execute (SequenceTokenDispatcher)
            return await Task.Run(() => CanalManager.ExecuteCanalSimulation());

            // Normally we would create an event "OnCanalSimulationCompleted" that is raised when the task is finally run on a queue
            // The "CanalServiceAgent" registers to "OnCanalSimulationCompleted" event in "CanalFlowService" and makes sure it returns a result when checking running task against the correlation id
            //SequenceTokenWorkDispatcher.EnqueueUnitOfWork(() => CanalManager.ExecuteCanalSimulation(),
            //                 OnCanalSimulationCompleted, correlationId, TransactionManager, name);
        }
    }
}
