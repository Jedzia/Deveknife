// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Horst.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>07.11.2015 23:16</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife
{
    using System;
    using System.IO;

    using Deveknife.Api;

    /// <summary>
    /// Main Application Interface Object.
    /// </summary>
    public class Horst : IHost
    {
        private readonly MainForm mainForm;

        private ISettingsProvider settingsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="Horst" /> class.
        /// </summary>
        /// <param name="mainForm">The main form.</param>
        /// <param name="settingsProvider">The settings provider.</param>
        public Horst(MainForm mainForm, ISettingsProvider settingsProvider)
        {
            this.mainForm = Guard.NotNull(() => mainForm, mainForm);
            this.settingsProvider = Guard.NotNull(() => settingsProvider, settingsProvider);
        }

        /// <summary>
        /// Occurs when [status changed].
        /// </summary>
        public event EventHandler<StatusData> StatusChanged;

        /// <summary>
        /// Displays the status.
        /// </summary>
        /// <param name="data">The data.</param>
        public void DisplayStatus(StatusData data)
        {
            if (data.HasProgress)
            {
                this.mainForm.IvReq(
                    delegate
                        {
                            this.mainForm.pbAll.Maximum = data.MaxProgress;
                            if (data.Progress > data.MaxProgress)
                            {
                                this.mainForm.pbAll.Maximum = data.Progress;
                            }

                            this.mainForm.pbAll.Value = data.Progress;
                        });
            }

            this.OnStatusChanged(data);
        }

        ISettingsProvider IHost.GetSettingsProvider()
        {
            return this.settingsProvider;
        }

        /// <summary>
        /// Notifies the host of items in work.
        /// </summary>
        /// <param name="sender">The sending blade. ToDo: this should be better the blade ... UI-less.</param>
        /// <param name="item">The work item.</param>
        /// <param name="action">The notify-action.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">action is null.</exception>
        public void NotifyWorkItem(IBlade sender, string item, WorkItemAction action)
        {
            switch (action)
            {
                // Todo: doublettes throw. detect here or in the recoder impl?
                // hmmm ... both. recoder should not have to edentical items and this here should be protected
                // and spit out an log entry, if doublettes occur.
                case WorkItemAction.Enqueued:
                    try
                    {
                        this.mainForm.IvReq(
                            delegate
                                {
                                    var nodeText = item;
                                    if (item.Length > 40)
                                    {
                                        nodeText = Path.GetFileNameWithoutExtension(item);
                                    }

                                    this.mainForm.treeView1.Nodes.Add(item, nodeText);
                                });
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format(
                            "NotifyWorkItem: '" + "' WorkItemAction.Enqueued failed with:{0}{1}",
                            Environment.NewLine,
                            ex);
                        this.mainForm.Logger.Error(message);
                    }

                    break;
                case WorkItemAction.Dequeued:
                case WorkItemAction.Cancelled:
                case WorkItemAction.Successful:
                case WorkItemAction.UnSuccessful:
                    try
                    {
                        this.mainForm.IvReq(delegate { this.mainForm.treeView1.Nodes.RemoveByKey(item); });
                    }
                    catch (Exception ex)
                    {
                        var message =
                            string.Format(
                                "NotifyWorkItem: '" + "' WorkItemAction.Dequeued-UnSuccessful failed with:{0}{1}",
                                Environment.NewLine,
                                ex);
                        this.mainForm.Logger.Error(message);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException("action");
            }
        }

        /// <summary>
        /// Called when [status changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnStatusChanged(StatusData e)
        {
            var handler = this.StatusChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}