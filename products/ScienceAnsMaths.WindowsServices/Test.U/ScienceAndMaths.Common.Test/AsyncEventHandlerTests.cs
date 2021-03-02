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
        public async Task BasicTestAsyncEventHandler()
        {
            EventPatternTest eventRaiser = new EventPatternTest(new SequenceTaskScheduler());
            
            eventRaiser.TaskCompletedAsync += EventRaiserOnTaskCompletedAsync;

            eventRaiser.OnTaskCompleted(Guid.NewGuid().ToString());

            await processFinishedTask.Task;

            Assert.IsTrue(eventRaised);
        }

        private async Task EventRaiserOnTaskCompletedAsync(object sender, string eventargs)
        {
            await Task.Delay(100);

            Assert.IsNotNull(eventargs);

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

        public virtual void OnTaskCompleted(string id)
        {
            TaskCompletedAsync.RaiseQueued(this, id, Scheduler, id);
        }
    }
}
