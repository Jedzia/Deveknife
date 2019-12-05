//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IHost.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>27.12.2013 20:07</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    using System;

    /// <summary>
    /// Enum WorkItemAction
    /// </summary>
    public enum WorkItemAction
    {
        /// <summary>
        /// A workitem is added to the list.
        /// </summary>
        Enqueued,
        
        /// <summary>
        /// A workitem is removed from the list.
        /// </summary>
        Dequeued,
        
        /// <summary>
        /// A workitem is cancelled.
        /// </summary>
        Cancelled,
        
        /// <summary>
        /// A workitem has successful finished its job.
        /// </summary>
        Successful,
        
        /// <summary>
        /// A workitem has unsuccessful ended.
        /// </summary>
        UnSuccessful,
        
        /// <summary>
        /// A workitem reached the state of processing.
        /// </summary>
        Inprogress
    }

    /// <summary>
    /// Provides access to the host API.
    /// </summary>
    public interface IHost
    {
        event EventHandler<StatusData> StatusChanged;

        void DisplayStatus(StatusData data);

        ISettingsProvider GetSettingsProvider();

        /// <summary>
        /// Notifies the host of items in work.
        /// </summary>
        /// <param name="sender">The sending blade. Todo: this should be better the blade ... UI-less.</param>
        /// <param name="item">The work item.</param>
        /// <param name="action">The notify-action.</param>
        void NotifyWorkItem(IBlade sender, string item, WorkItemAction action);
    }
}