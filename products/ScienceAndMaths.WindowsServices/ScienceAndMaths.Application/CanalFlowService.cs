using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Common;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Canals;
using Unity;

namespace ScienceAndMaths.Application
{
    public class CanalFlowService : ICanalFlowService
    {
        public event ActionCompletedEventHandler<CanalData> CanalSimulationCompleted;

        [Dependency]
        public ICanalManager CanalManager { get; set; }

        [Dependency]
        public SequenceTaskScheduler SequenceTaskScheduler { get; set; }

        public void ExecuteCanalSimulationAsync(string correlationId)
        {
            //  Make sure here we control how many request and threads can execute (SequenceTokenDispatcher)
            SequenceTaskScheduler.EnqueueWork(() => CanalManager.ExecuteCanalSimulation(), ExecuteCanalSimulationCompleted, correlationId, "CanalSimulation");
        }

        private void ExecuteCanalSimulationCompleted(ActionCompletedEventArgs<CanalData> eventArgs)
        {
            ActionCompletedEventHandler<CanalData> handler =
                CanalSimulationCompleted;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }
    }
}
