using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Common.TaskExtension;

namespace ScienceAndMaths.Common.EventHandlerExtensions
{
    public static class EventHandlerExtensionMethods
    {
        /// <summary>
        /// Queues an AsyncEventHandler invocation to the SequenceTokenWorkDispatcher.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="tokens">The tokens.</param>
        public static void RaiseQueued<T>(this AsyncEventHandler<T> eventHandler, object sender, T args, SequenceTaskScheduler dispatcher, params object[] tokens)
        {
            // queue a task that produces a
            var task = new Task<Task>(async () => { await eventHandler.RaiseAsync(sender, args); });
            dispatcher.EnqueueWorkAsync(task, tokens).ObserveExceptions();
        }

        /// <summary>
        /// sequentially iterates, invokes and awaits all registered AsyncEventHandlers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The EventHandler to invoke.</param>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>Returns a task representing the asynchronous operation.</returns>
        public static async Task RaiseAsync<T>(this AsyncEventHandler<T> eventHandler, object sender, T args)
        {
            if (eventHandler != null)
            {
                foreach (var del in eventHandler.GetInvocationList().Cast<AsyncEventHandler<T>>())
                {
                    await del.Invoke(sender, args);
                }
            }
        }
    }
}
