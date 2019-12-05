// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Job.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 10:04</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Castle.Core.Logging;

    /// <summary>
    /// Represents a Job runnable by the user.
    /// </summary>
    public abstract class Job
    {
        // private static readonly WeakReferenceDictionary<Guid, Job> GlobalJobList = new WeakReferenceDictionary<Guid, Job>();
        private static readonly Dictionary<Guid, Job> GlobalJobList = new Dictionary<Guid, Job>();

        private readonly Guid key = Guid.NewGuid();

        private Guid parentJob;

        /// <summary>
        /// Initializes a new instance of the <see cref="Job"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Job(string name)
        {
            this.Name = name;
            GlobalJobList.Add(this.Key, this);
            this.ChildrenJobs = new List<Job>();
        }

        /// <summary>
        /// Gets the children jobs.
        /// </summary>
        /// <value>The children jobs.</value>
        public List<Job> ChildrenJobs { get; private set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public Bitmap Icon { get; set; }

        /// <summary>
        /// Gets or sets the index of the image.
        /// </summary>
        /// <value>The index of the image.</value>
        public int ImageIndex { get; set; }

        /// <summary>
        /// Gets the unique key.
        /// </summary>
        /// <value>The unique key.</value>
        public Guid Key
        {
            get
            {
                return this.key;
            }
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the parent job.
        /// </summary>
        /// <value>The parent job.</value>
        public Guid ParentJob
        {
            get
            {
                return this.parentJob;
            }

            set
            {
                if(this.parentJob == value)
                {
                    return;
                }

                var found = GlobalJobList.ContainsKey(value);
                if(found)
                {
                    var pjob = GlobalJobList[value];
                    pjob.ChildrenJobs.Add(this);
                }

                this.parentJob = value;
            }
        }

        /// <summary>
        /// Executes the job with the specified parameters in asynchronous mode.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the job results for asynchronous execution.</returns>
        public DeferredJobResult Async(JobParameters parameters)
        {
            return this.RunAsync(new DeferredJobResult(), parameters, false);
        }

        /// <summary>
        /// Executes the job with the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if completed without errors, <c>false</c> otherwise</returns>
        public bool Execute(JobParameters parameters)
        {
            var jobResult = new DeferredJobResult();
            var result = this.RunAsync(jobResult, parameters, true).Success;
            return result;
        }

        /// <summary>
        /// Runs the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if this instance can Success execution the execution queue; otherwise, <c>false</c>.</returns>
        protected internal virtual bool Run(JobParameters parameters)
        {
            return true;
        }

        /// <summary>
        /// Calls the specified jobs.
        /// </summary>
        /// <param name="jobResult">The job result.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="childrenJobs">The children jobs.</param>
        /// <param name="sync">if set to <c>true</c> run synchronous.</param>
        /// <returns>the job results for asynchronous execution.</returns>
        protected static DeferredJobResult CallSpecifiedJobsAsync(
            DeferredJobResult jobResult,
            JobParameters parameters,
            List<Job> childrenJobs,
            bool sync)
        {
            var success = jobResult.Success;
            foreach(var job in childrenJobs)
            {
                var result = job.RunAsync(jobResult, parameters, sync);
                var executeSuccess = result.Success;
                if(job.CanCancelExecution(parameters) && !executeSuccess)
                {
                    result.Success = false;
                    return result;
                }

                success &= executeSuccess;
            }

            jobResult.Success = success;
            return jobResult;
        }

        /// <summary>
        /// Determines whether this instance can Success the execution queue.
        /// </summary>
        /// <param name="parameters">The parameters of the job.</param>
        /// <returns><c>true</c> if this instance can Success execution the execution queue; otherwise, <c>false</c>.</returns>
        protected virtual bool CanCancelExecution(JobParameters parameters)
        {
            // Todo: true execution path is not tested yet.
            return false;
        }

        /// <summary>
        /// Log as debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void LogDebug(string message)
        {
            if(this.Logger == null)
            {
                return;
            }

            this.LogInternal(message, "Job", this.Logger.Debug);
        }

        /// <summary>
        /// Log as error message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void LogError(string message)
        {
            if(this.Logger == null)
            {
                return;
            }

            this.LogInternal(message, "Job", this.Logger.Error);
        }

        /// <summary>
        /// Log as info message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void LogInfo(string message)
        {
            if(this.Logger == null)
            {
                return;
            }

            this.LogInternal(message, "Job", this.Logger.Info);
        }

        /// <summary>
        /// Runs the job with the specified parameters in asynchronous mode.
        /// </summary>
        /// <param name="jobResult">The job result ingoing object.</param>
        /// <param name="parameters">The parameters of the job.</param>
        /// <param name="sync">if set to <c>true</c> run synchronous.</param>
        /// <remarks>Running this method in synchronous mode with the <paramref name="sync"/> switch set, simply executes 
        /// the <see cref="Run"/> method after doing the async stuff.</remarks>
        /// <returns>the job results outgoing object for asynchronous execution.</returns>
        protected abstract DeferredJobResult RunAsync(DeferredJobResult jobResult, JobParameters parameters, bool sync);

        /// <summary>
        /// Log Messages.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="topicName">Name of the topic.</param>
        /// <param name="logAction">The log action.</param>
        private void LogInternal(string message, string topicName, Action<string> logAction)
        {
            var msg = string.Format("[{2}:{0}] {1}", this.GetType().Name, message, topicName);
            // ReSharper disable once EventExceptionNotDocumented
            logAction(msg);
            if(DateTime.Now.Second % 3 == 0)
            {
                Application.DoEvents();
            }
        }
    }
}