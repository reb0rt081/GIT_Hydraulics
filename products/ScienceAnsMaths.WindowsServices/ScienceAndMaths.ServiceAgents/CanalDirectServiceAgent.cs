using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Domain;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Canals;
using Unity;

namespace ScienceAndMaths.ServiceAgents
{
    public class CanalDirectServiceAgent : ICanalServiceAgent
    {
        private readonly ConcurrentDictionary<string, TaskCompletionSource<CanalData>> canalSimulationPendingRequests = new ConcurrentDictionary<string, TaskCompletionSource<CanalData>>();

        [InjectionMethod]
        public void Initialize()
        {
            CanalFlowService.CanalSimulationCompleted += CanalFlowServiceOnCanalSimulationCompleted;
        }

        [Dependency]
        public ICanalFlowService CanalFlowService { get; set; }

        public Task<CanalData> ExecuteCanalSimulationAsync()
        {
            // Normally use correlation Ids to correlate messages and async methods
            TaskCompletionSource<CanalData> completionSource = new TaskCompletionSource<CanalData>();
            string correlationId = Guid.NewGuid().ToString();

            canalSimulationPendingRequests.TryAdd(correlationId, completionSource);

            CanalFlowService.ExecuteCanalSimulationAsync(correlationId);

            return completionSource.Task;
        }

        private void CanalFlowServiceOnCanalSimulationCompleted(object sender, ActionCompletedEventArgs<CanalData> eventArgs)
        {
            if (canalSimulationPendingRequests.TryRemove(eventArgs.CorrelationId,
                    out TaskCompletionSource<CanalData> runningTask))
            {
                runningTask.TrySetResult(eventArgs.Result);
            }
        }
    }
}
