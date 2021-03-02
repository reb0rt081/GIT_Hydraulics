using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ScienceAndMaths.Common.TaskExtension;
using ScienceAndMaths.Domain;

namespace ScienceAndMaths.Common
{
    public class SequenceTaskScheduler
    {
        private readonly Dictionary<object, MessageQueue> currentlyUsedQueues = new Dictionary<object, MessageQueue>();

        public SequenceTaskScheduler()
        {
            TaskScheduler = TaskScheduler.Default;
        }

        /// <summary>
        /// Gets or sets the task scheduler all tasks will be executed on. (default is TaskScheduler.Default)
        /// </summary>
        public TaskScheduler TaskScheduler { get; set; }

        public void EnqueueWork<T>(Func<T> action, Action<ActionCompletedEventArgs<T>> eventInvoker,
            string correlationId, params object[] sequenceTokens)
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

            Func<Task> func = () =>
            {
                task.Start(TaskScheduler);
                return task;
            };

            EnqueueWorkAsync(func, sequenceTokens).ObserveExceptions();
        }

        public void EnqueueWork(Action action, Action<ActionCompletedEventArgs> eventInvoker, string correlationId,
            params object[] sequenceTokens)
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

            Func<Task> func = () =>
            {
                task.Start(TaskScheduler);
                return task;
            };

            EnqueueWorkAsync(func, sequenceTokens).ObserveExceptions();
        }

        public Task EnqueueWorkAsync(Task task, params object[] sequenceTokens)
        {
            Func<Task> func = () =>
            {
                task.Start();
                return task;
            };
            return EnqueueWorkAsync(func, sequenceTokens);
        }

        public Task EnqueueWorkAsync(Func<Task> workTask, params object[] sequenceTokens)
        {
            if (sequenceTokens.Length == 0 || (sequenceTokens.Length == 1 && sequenceTokens[0] == null) || sequenceTokens.All(o => o == null))
            {
                return workTask();
            }

            WorkItem workItem;
            if (sequenceTokens.Length == 1)
            {
                workItem = new WorkItem(1, workTask);
                GetQueueForToken(sequenceTokens[0]).QueueMessage(workItem);
                return workItem.Completion;
            }

            object[] checkedList = sequenceTokens.Where(o => o != null).Distinct().ToArray();
            workItem = new WorkItem(checkedList.Length, workTask);

            foreach (object token in checkedList)
            {
                GetQueueForToken(token).QueueMessage(workItem);
            }

            return workItem.Completion;
        }

        private MessageQueue GetQueueForToken(object sequenceToken)
        {
            lock (currentlyUsedQueues)
            {
                if (!currentlyUsedQueues.TryGetValue(sequenceToken, out MessageQueue messageQueue))
                {
                    messageQueue = new MessageQueue(sequenceToken.ToString(), () => RemoveMessageDispatcherByToken(sequenceToken));
                    currentlyUsedQueues.Add(sequenceToken, messageQueue);
                }

                return messageQueue;
            }
        }

        private void RemoveMessageDispatcherByToken(object token)
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
        private readonly List<WorkItem> queuedTasks = new List<WorkItem>();
        private readonly Action disposeAction;

        internal string SequenceToken { get; set; }

        internal MessageQueue(string sequenceToken, Action disposingAction)
        {
            SequenceToken = sequenceToken;
            disposeAction = disposingAction;
        }

        internal void QueueMessage(WorkItem newTask)
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
                WorkItem workTask = null;
                try
                {
                    workTask = queuedTasks[0];

                    await workTask.WorkAsync();
                }
                catch (Exception exception)
                {
                    throw;
                }
                finally
                {
                    lock (queuedTasks)
                    {
                        if(workTask != null)
                        {
                            queuedTasks.Remove(workTask);
                        }

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

    internal class WorkItem
    {
        #region Constants and Fields
        private readonly TaskCompletionSource<bool> tcs;
        private readonly Func<Task> workTask;
        private int awaitingOperations;
       
        #endregion

        public Task Completion => tcs.Task;

        /// <summary>
        ///     Creates a new instance of the WorkItem class.
        /// </summary>
        /// <param name="awaitingOperations">The awaiting operations.</param>
        /// <param name="workTask">          The work task.</param>
        public WorkItem(int awaitingOperations, Func<Task> workTask)
        {
            this.awaitingOperations = awaitingOperations;
            this.workTask = workTask;
            tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        public async Task WorkAsync()
        {
            if (Interlocked.Decrement(ref awaitingOperations) == 0)
            {
                try
                {
                    Task task = workTask();
                    await task;

                    if (task is Task<Task> nested)
                    {
                        await nested.Unwrap();
                    }

                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                    throw;
                }
            }
            else
            {
                await tcs.Task;
            }
        }
    }
}
