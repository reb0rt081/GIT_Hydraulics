using System;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<string, TaskCompletionSource<CanalSimulationResult>> canalSimulationPendingRequests = new ConcurrentDictionary<string, TaskCompletionSource<CanalSimulationResult>>();

        [InjectionMethod]
        public void Initialize()
        {
            CanalFlowService.CanalSimulationCompleted += CanalFlowServiceOnCanalSimulationCompleted;
        }

        [Dependency]
        public ICanalFlowService CanalFlowService { get; set; }

        public Task<CanalSimulationResult> ExecuteCanalSimulationAsync()
        {
            // Normally use correlation Ids to correlate messages and async methods
            TaskCompletionSource<CanalSimulationResult> completionSource = new TaskCompletionSource<CanalSimulationResult>();
            string correlationId = Guid.NewGuid().ToString();

            canalSimulationPendingRequests.TryAdd(correlationId, completionSource);

            CanalFlowService.ExecuteCanalSimulationAsync(correlationId);

            return completionSource.Task;
        }

        private void CanalFlowServiceOnCanalSimulationCompleted(object sender, ActionCompletedEventArgs<CanalSimulationResult> eventArgs)
        {
            if (canalSimulationPendingRequests.TryRemove(eventArgs.CorrelationId,
                    out TaskCompletionSource<CanalSimulationResult> runningTask))
            {
                runningTask.TrySetResult(eventArgs.Result);
            }

        }
    }
}
