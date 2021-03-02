using System;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScienceAndMaths.Common.EventHandlerExtensions;

namespace ScienceAndMaths.Common.Test
{
    [TestClass]
    public class AsyncEventHandlerTests
    {
        private bool eventRaised;
        private TaskCompletionSource<bool> processFinishedTask;

        [TestInitialize]
        public void Initialize()
        {
            processFinishedTask = new TaskCompletionSource<bool>();
            eventRaised = false;
        }
        [TestMethod]
        public async Task BasicTestAsyncScheduleEventHandler()
        {
            EventPatternTest eventRaiser = new EventPatternTest(new SequenceTaskScheduler());
            
            eventRaiser.TaskCompletedAsync += EventRaiserOnTaskCompletedAsync;

            eventRaiser.QueueOnTaskCompleted(Guid.NewGuid().ToString());

            await processFinishedTask.Task;

            Assert.IsTrue(eventRaised);
        }

        [TestMethod]
        public async Task BasicTestAsyncEventHandler()
        {
            EventPatternTest eventRaiser = new EventPatternTest(new SequenceTaskScheduler());

            eventRaiser.TaskCompletedAsync += EventRaiserOnTaskCompletedAsync;

            await eventRaiser.OnTaskCompletedAsync(Guid.NewGuid().ToString());

            Assert.IsTrue(eventRaised);
            Assert.IsTrue(processFinishedTask.Task.IsCompleted);
        }

        private async Task EventRaiserOnTaskCompletedAsync(object sender, string eventArgs)
        {
            await Task.Delay(100);

            Assert.IsNotNull(eventArgs);

            eventRaised = true;

            processFinishedTask.TrySetResult(true);
        }
    }

    internal class EventPatternTest
    {
        public event AsyncEventHandler<string> TaskCompletedAsync;

        public SequenceTaskScheduler Scheduler { get; set; }

        public EventPatternTest(SequenceTaskScheduler scheduler)
        {
            Scheduler = scheduler;
        }

        public void QueueOnTaskCompleted(string id)
        {
            TaskCompletedAsync.RaiseQueued(this, id, Scheduler, id);
        }

        public async  Task OnTaskCompletedAsync(string id)
        {
            AsyncEventHandler<string> handler = TaskCompletedAsync;

            if(handler != null)
            {
                await handler.RaiseAsync(this, id);
            }
        }
    }
}
