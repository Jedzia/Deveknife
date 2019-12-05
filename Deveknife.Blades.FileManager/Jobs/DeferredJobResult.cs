// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeferredJobResult.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>31.07.2015 15:32</date>
// <summary>
//   Job results for asynchronous execution.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs
{
    using System.Collections.Generic;

    /// <summary>
    /// Results for a <see cref="Job"/> when running asynchronously.
    /// </summary>
    public class DeferredJobResult
    {
        private readonly List<JobClosure> jobClosures = new List<JobClosure>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DeferredJobResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> the job signals success.</param>
        public DeferredJobResult(bool success = true)
        {
            this.Success = success;
        }

        /// <summary>
        /// Gets the jobs.
        /// </summary>
        /// <value>The jobs.</value>
        public IEnumerable<JobClosure> Jobs
        {
            get
            {
                return this.jobClosures;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DeferredJobResult"/> execution was a success.
        /// </summary>
        /// <value><c>true</c> if execution was successfull; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

        /// <summary>
        /// Adds the specified job as a closure.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <param name="parameters">The parameters for the job.</param>
        // ReSharper disable once MethodNameNotMeaningful
        public void Add(Job job, JobParameters parameters)
        {
            this.jobClosures.Add(new JobClosure(job, parameters));
        }

        /// <summary>
        /// Execute the jobs enumerated by this instance.
        /// </summary>
        /// <returns><c>true</c> if completed without errors, <c>false</c> otherwise</returns>
        public bool Execute()
        {
            var result = true;
            foreach (var jobClosure in this.Jobs)
            {
                result &= jobClosure.Run();
            }

            return result;
        }

        /// <summary>
        /// Holds information to a job with its corresponding parameters.
        /// </summary>
        public class JobClosure
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="JobClosure"/> class.
            /// </summary>
            /// <param name="job">The job.</param>
            /// <param name="parameters">The parameters.</param>
            public JobClosure(Job job, JobParameters parameters)
            {
                Guard.NotNull(() => job, job);
                this.Job = job;
                Guard.NotNull(() => parameters, parameters);
                this.Parameters = parameters;
                this.Enabled = true;
            }

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="JobClosure"/> is enabled.
            /// </summary>
            /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
            public bool Enabled { get; set; }

            /// <summary>
            /// Gets the job.
            /// </summary>
            /// <value>The job.</value>
            public Job Job { get; private set; }

            /// <summary>
            /// Gets the parameters.
            /// </summary>
            /// <value>The parameters.</value>
            public JobParameters Parameters { get; private set; }

            /// <summary>
            /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
            /// </returns>
            public override string ToString()
            {
                return this.Parameters.ToString();
            }

            /// <summary>
            /// Runs the job enclosed by this instance.
            /// </summary>
            /// <returns><c>true</c> if completed without errors, <c>false</c> otherwise</returns>
            // ReSharper disable once MethodNameNotMeaningful
            public bool Run()
            {
                return this.Job.Run(this.Parameters);
            }
        }
    }
}