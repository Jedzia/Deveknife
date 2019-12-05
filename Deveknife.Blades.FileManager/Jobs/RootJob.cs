// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RootJob.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>31.07.2015 16:50</date>
// <summary>
//   Class Compare
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs
{
    /// <summary>
    /// Class Compare
    /// </summary>
    public class RootJob : Job
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootJob"/> class.
        /// </summary>
        public RootJob()
            : base("Root")
        {
        }

        /// <summary>
        /// Runs the job with the specified parameters in asynchronous mode.
        /// </summary>
        /// <param name="jobResult">The job result ingoing object.</param>
        /// <param name="parameters">The parameters of the job.</param>
        /// <param name="sync">if set to <c>true</c> run synchronous.</param>
        /// <returns>the job results outgoing object for asynchronous execution.</returns>
        /// <remarks>Running this method in synchronous mode with the <paramref name="sync" /> switch set, simply executes
        /// the <see cref="Job.Run" /> method after doing the async stuff.</remarks>
        protected override DeferredJobResult RunAsync(DeferredJobResult jobResult, JobParameters parameters, bool sync)
        {
            return Job.CallSpecifiedJobsAsync(jobResult, parameters, this.ChildrenJobs, sync);
        }
    }
}