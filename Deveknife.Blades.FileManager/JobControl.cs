﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobControl.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 10:54</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using Deveknife.Blades.FileManager.Images;
    using Deveknife.Blades.FileManager.Jobs;

    using Castle.Core.Logging;

    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Base;
    using DevExpress.XtraTreeList;

    /// <summary>
    /// Class JobControl
    /// </summary>
    public partial class JobControl : XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobControl"/> class.
        /// </summary>
        public JobControl()
        {
            this.InitializeComponent();

            // This line of code is generated by Data Source Configuration Wizard
            // trlJobs.DataSource = new System.Collections.Generic.List<Job>();
            // This line of code is generated by Data Source Configuration Wizard
            // this.trlJobs.DataSource = new List<JobList>();
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the job tree.
        /// </summary>
        /// <value>The job tree.</value>
        protected List<Job> JobTree { get; set; }

        private IEnumerable<DeferredJobResult.JobClosure> JobClosures { get; set; }

        /// <summary>
        /// Executes the jobs.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool ExecuteJobs(JobParameters parameters)
        {
            var result = true;
            foreach(var job in this.JobTree)
            {
                result &= job.Execute(parameters);
            }

            return result;
        }

        /// <summary>
        /// Executes the jobs.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public DeferredJobResult ExecuteJobsAsync(JobParameters parameters)
        {
            var result = new DeferredJobResult();
            foreach(var job in this.JobTree)
            {
                var deferredJobResult = job.Async(parameters);

                // jobClosureBindingSource.DataSource = deferredJobResult.Jobs;
                foreach(var resultJob in deferredJobResult.Jobs)
                {
                    result.Add(resultJob.Job, resultJob.Parameters);

                    // clbJobs.Items.Add(resultJob);
                }

                result.Success &= deferredJobResult.Success;
            }

            this.JobClosures = result.Jobs;
            this.jobClosureBindingSource.DataSource = this.JobClosures;
            return result;
        }

        /// <summary>
        /// Handles the Click event of the btnExecuteJobs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnExecuteJobsClick(object sender, EventArgs e)
        {
            foreach(var source in this.JobClosures.Where((closure, i) => closure.Enabled))
            {
                this.Logger.Info("Running job '" + source.Parameters + "'.");
                source.Run();
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the comboBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ComboBox1SelectionChangeCommitted(object sender, EventArgs e)
        {
            var sel = this.comboBox1.SelectedItem as string;
            if(sel == null)
            {
                return;
            }

            var view = this.gridControl1.ViewCollection.FirstOrDefault(baseView => baseView.Name == sel);
            if(view == null)
            {
                return;
            }

            this.gridControl1.MainView = view;
        }

        /// <summary>
        /// Handles the Load event of the JobControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void JobControlLoad(object sender, EventArgs e)
        {
            foreach(BaseView view in this.gridControl1.ViewCollection)
            {
                this.comboBox1.Items.Add(view.Name);
            }

            var logger = this.Logger;

            // ILogger logger = null;
            var rootJob = new RootJob { Logger = logger, ImageIndex = 0, Icon = ImageResource.Globe };
            var traverseJob = new Traverse(3)
                              {
                                  ParentJob = rootJob.Key,
                                  Logger = logger,
                                  ImageIndex = 1,
                                  Icon = ImageResource.camera_mount
                              };
            var compareJob = new Compare { ParentJob = traverseJob.Key, Logger = logger, ImageIndex = 2, Icon = ImageResource.camera_mount };
            var fileActionJob = new FileWinRarAction
                                {
                                    ParentJob = compareJob.Key,
                                    Logger = logger,
                                    ImageIndex = 3,
                                    Icon = ImageResource.camera_mount
                                };

            var jobs = new List<Job> { rootJob, traverseJob, compareJob, fileActionJob };
            this.trlJobs.DataSource = jobs;

            var running = new List<Job> { traverseJob, compareJob, fileActionJob };
            this.JobTree = new List<Job> { rootJob };
            this.gridControl1.DataSource = running;

            /*foreach (var job in jobs)
            {
                this.trlJobs.Nodes.Add(job);
            }*/
        }

        /// <summary>
        /// Handles the Click event of the simpleButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SimpleButton1Click(object sender, EventArgs e)
        {
            var view = this.gridControl1.MainView as ColumnView;
            if(view == null)
            {
                return;
            }

            var row = view.GetFocusedRow();
        }

        /// <summary>
        /// Handles the CustomDrawNodeImages event of the TreeListJobs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CustomDrawNodeImagesEventArgs"/> instance containing the event data.</param>
        private void TreeListJobsCustomDrawNodeImages(object sender, CustomDrawNodeImagesEventArgs e)
        {
            // trlJobs.AddFilter();
            {
                // if (!e.Node.Equals(trlJobs.FocusedNode))
                try
                {
                    // var node = e.Node.GetValue(colName.AbsoluteIndex);
                    var node = this.trlJobs.GetDataRecordByNode(e.Node) as Job;
                    if((node == null) || (node.Icon == null))
                    {
                        return;
                    }

                    var image = new Bitmap(node.Icon, 32, 32);

                    // var image = new Bitmap(ImageResource.Globe, 32, 32);
                    // var image = ImageCollection.GetImageListImage(this.trlJobs.SelectImageList, e.SelectImageIndex);
                    // image = BitmapCreator.CreateBitmapWithResolutionLimit(image, Color.Crimson);
                    int y = (e.SelectRect.Top + (e.SelectRect.Height - image.Height)) / 2;
                    ControlPaint.DrawImageDisabled(e.Graphics, image, e.SelectRect.X, y, Color.Black);
                    e.Handled = true;
                }
                catch(Exception)
                {
                }
            }
        }

        /// <summary>
        /// Handles the GetStateImage event of the TreeListJobs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GetStateImageEventArgs"/> instance containing the event data.</param>
        private void TreeListJobsGetStateImage(object sender, GetStateImageEventArgs e)
        {
            // e.NodeImageIndex = 1;
        }
    }
}