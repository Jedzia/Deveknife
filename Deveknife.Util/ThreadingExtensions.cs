// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThreadingExtensions.cs" company="EvePanix">
//   (c) 2010 Jedzia
// </copyright>
// <summary>
//   Extension methods for control thread handling.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife
{
    #region Imports

    using System;
    using System.Windows.Forms;

    #endregion

    /// <summary>
    /// Extension methods for control thread handling.
    /// </summary>
    public static class ThreadingExtensions
    {
        /// <summary>
        /// Invokes a method on a control if required. With null and dispose check.
        /// </summary>
        /// <param name="control">The source control.</param>
        /// <param name="methodcall">The method to call.</param>
        /// <remarks><para>Simple helper extension method to marshall to correct
        /// thread if its required.</para>
        /// This also checks the control for null and disposed.</remarks>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static void InvokeIfRequired(this Control control, Action methodcall)
        {
            // Check for a valid control and if its not disposed
            // or not in the disposing state.
            if ((control == null) || control.IsDisposed || control.Disposing)
            {
                return;
            }

            // see if we need to Invoke call to Dispatcher thread
            if (control.InvokeRequired)
            {
                control.Invoke(methodcall);
            }
            else
            {
                // no invocation needed.
                methodcall();
            }
        }

        /// <summary>
        /// Invokes a method on a control if required. With no null and dispose check.
        /// </summary>
        /// <param name="control">The source control.</param>
        /// <param name="methodcall">The method to call.</param>
        /// <remarks><para>Simple helper extension method to marshall to correct
        /// thread if its required.</para>
        /// </remarks>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public static void IvReq(this Control control, Action methodcall)
        {
            // see if we need to Invoke call to Dispatcher thread
            if (control.InvokeRequired)
            {
                control.Invoke(methodcall);
            }
            else
            {
                // no invocation needed.
                methodcall();
            }
        }
    }
}