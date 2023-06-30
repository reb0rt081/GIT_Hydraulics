using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Common.TaskExtension
{
    public static class TaskExtensionMethods
    {
        public static void ObserveExceptions(this Task task)
        {
            task.ContinueWith(t =>
            {
                if (task.IsFaulted && task.Exception != null && task.Exception.InnerException != null)
                {
                    throw task.Exception.InnerException;
                }
            });
        }
    }
}
