using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Common
{
    /// <summary>
    ///     Delegate representing an asynchronous event.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="eventArgs">The event arguments.</param>
    /// <returns>Returns the task representing the asynchronous operation.</returns>
    public delegate Task AsyncEventHandler(object sender, System.EventArgs eventArgs);

    /// <summary>
    ///     Delegate representing an asynchronous event.
    /// </summary>
    /// <typeparam name="TEventArgs">The Event Argument Type.</typeparam>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="eventArgs">The event arguments.</param>
    /// <returns>Returns the task representing the asynchronous operation.</returns>
    public delegate Task AsyncEventHandler<in TEventArgs>(object sender, TEventArgs eventArgs);
}
