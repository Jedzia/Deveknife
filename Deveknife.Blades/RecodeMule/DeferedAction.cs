//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="DeferedAction.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 12:30</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule
{
    using System;
    using System.Threading;

    /// <summary>
    /// Start an Action after an amount of time.
    /// </summary>
    public class DeferedAction
    {
        private readonly Action action;

        private readonly Timer timer;

        private readonly int waitingTime;

        public DeferedAction(int waitingTime, Action action)
        {
            this.action = action;
            this.waitingTime = waitingTime;
            this.timer = new Timer(this.Callback);
            this.timer.Change(this.waitingTime, int.MaxValue);
            //this.timer.Tick += this.TTick;
        }

        /// <summary>
        /// Runs the timed action.
        /// </summary>
        /// <param name="state">The object state.</param>
        private void Callback(object state)
        {
            this.action();
        }
    }
}