using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Domain
{
    public class ActionCompletedEventArgs
    {
        public string CorrelationId { get; }

        public Exception Exception { get; }

        public ActionCompletedEventArgs(string correlationId)
        {
            CorrelationId = correlationId;
            Exception = null;
        }

        public ActionCompletedEventArgs(string correlationId, Exception exception)
        {
            CorrelationId = correlationId;
            Exception = exception;
        }
    }

    public class ActionCompletedEventArgs<T> : ActionCompletedEventArgs
    {
        public T Result { get; set; }
        public ActionCompletedEventArgs(string correlationId, T result) : base(correlationId)
        {
            Result = result;
        }

        public ActionCompletedEventArgs(string correlationId, Exception exception) : base(correlationId, exception)
        {
            Result = default(T);
        }
    }

    public delegate void ActionCompletedEventHandler<T>(object sender, ActionCompletedEventArgs<T> eventArgs);

    public delegate void ActionCompletedEventHandler(object sender, ActionCompletedEventArgs eventArgs);
}
