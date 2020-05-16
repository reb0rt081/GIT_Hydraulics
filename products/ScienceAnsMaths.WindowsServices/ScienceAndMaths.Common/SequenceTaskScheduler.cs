using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Common.TaskExtension;
using ScienceAndMaths.Domain;

namespace ScienceAndMaths.Common
{
    public class SequenceTaskScheduler
    {
        private readonly Dictionary<string, MessageQueue> currentlyUsedQueues = new Dictionary<string, MessageQueue>();

        public SequenceTaskScheduler()
        {
            TaskScheduler = TaskScheduler.Default;
        }

        /// <summary>
        /// Gets or sets the task scheduler all tasks will be executed on. (default is TaskScheduler.Default)
        /// </summary>
        public TaskScheduler TaskScheduler { get; set; }

        public void EnqueueWork<T>(Func<T> action, Action<ActionCompletedEventArgs<T>> eventInvoker,
            string correlationId, string sequenceToken = null)
        {
            Task task = new Task(() =>
            {
                T result = default(T);
                Exception exception = null;

                try
                {
                    result = action.Invoke();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                if (exception == null)
                {
                    eventInvoker.Invoke(new ActionCompletedEventArgs<T>(correlationId, result));
                }
                else
                {
                    eventInvoker.Invoke(new ActionCompletedEventArgs<T>(correlationId, exception));
                }

            });

            EnqueueWorkAsync(task, sequenceToken).ObserveExceptions();
        }

        public void EnqueueWork(Action action, Action<ActionCompletedEventArgs> eventInvoker, string correlationId,
            string sequenceToken = null)
        {
            Task task = new Task(() =>
            {
                Exception exception = null;

                try
                {
                    action.Invoke();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                if (exception == null)
                {
                    eventInvoker.Invoke(new ActionCompletedEventArgs(correlationId));
                }
                else
                {
                    eventInvoker.Invoke(new ActionCompletedEventArgs(correlationId, exception));
                }

            });

            EnqueueWorkAsync(task, sequenceToken).ObserveExceptions();
        }

        public Task EnqueueWorkAsync(Task task, string sequenceToken)
        {
            if (string.IsNullOrEmpty(sequenceToken))
            {
                task.Start(TaskScheduler);
                return task;
            }
            
            QueueMessage(task, sequenceToken);

            return task;
        }

        private void QueueMessage(Task task, string sequenceToken)
        {
            // we need to remove the items
            lock (currentlyUsedQueues)
            {
                if (currentlyUsedQueues.TryGetValue(sequenceToken, out var messageQueue))
                {
                    messageQueue.QueueMessage(task);
                }
                else
                {
                    messageQueue = new MessageQueue(sequenceToken, TaskScheduler, () => RemoveMessageDispatcherByToken(sequenceToken));
                    currentlyUsedQueues.Add(sequenceToken, messageQueue);
                    messageQueue.QueueMessage(task);
                }
            }
        }

        private void RemoveMessageDispatcherByToken(string token)
        {
            lock (currentlyUsedQueues)
            {
                MessageQueue queue;

                if (currentlyUsedQueues.TryGetValue(token, out queue))
                {
                    if (queue.IsEmpty())
                    {
                        currentlyUsedQueues.Remove(token);
                    }
                }
            }
        }
    }

    internal class MessageQueue : IDisposable
    {
        private readonly List<Task> queuedTasks = new List<Task>();
        private readonly Action disposeAction;

        internal TaskScheduler TaskScheduler { get; set; }

        internal string SequenceToken { get; set; }

        internal MessageQueue(string sequenceToken, TaskScheduler taskScheduler, Action disposingAction)
        {
            SequenceToken = sequenceToken;
            TaskScheduler = taskScheduler;
            disposeAction = disposingAction;
        }

        internal void QueueMessage(Task newTask)
        {
            lock (queuedTasks)
            {
                queuedTasks.Add(newTask);

                if (queuedTasks.Count == 1)
                {
                    // Do not wait for this
                    RunQueueWorker();
                }
            }
        }

        internal async void RunQueueWorker()
        {
            bool canContinue = queuedTasks.Count > 0;

            while(canContinue)
            {
                Task task = queuedTasks[0];
                try
                {
                    task.Start(TaskScheduler);

                    await task;
                }
                catch (Exception exception)
                {
                    throw;
                }
                finally
                {
                    lock (queuedTasks)
                    {
                        queuedTasks.Remove(task);
                        canContinue = queuedTasks.Count > 0;

                        if (!canContinue)
                        {
                            Dispose();
                        }
                    }
                }
            }

        }

        internal bool IsEmpty()
        {
            return queuedTasks.Count == 0;
        }

        public void Dispose()
        {
            disposeAction();
        }
    }
}
