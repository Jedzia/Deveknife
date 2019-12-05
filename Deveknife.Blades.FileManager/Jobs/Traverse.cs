// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Traverse.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 10:05</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs
{
    using System;
    using System.IO;

    /// <summary>
    /// Class Compare
    /// </summary>
    public class Traverse : Job
    {
        /// <summary>
        /// The main parameter identifier.
        /// </summary>
        public const string MainParameterId = "Path";

        /// <summary>
        /// Initializes a new instance of the <see cref="Traverse" /> class.
        /// </summary>
        /// <param name="depth">The depth of directory iteration.</param>
        public Traverse(int depth = 0)
            : base("Traverse")
        {
            this.Depth = depth;
        }

        /// <summary>
        /// Gets the depth of directory iteration.
        /// </summary>
        /// <value>The depth.</value>
        public int Depth { get; private set; }

        /*
        /// <summary>
        /// Runs the job with the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if completed without errors, <c>false</c> otherwise</returns>
        /// <exception cref="ArgumentException">Run needs a named parameter.</exception>
        protected override bool Run(JobParameters parameters)
        {
            if (!parameters.ContainsKey(MainParameterId))
            {
                var message = string.Format("Run needs a parameter named {0}", MainParameterId);
                throw new ArgumentException(message, "parameters");
            }

            return this.IterateAsync(new DeferredJobResult(), parameters[MainParameterId], 0).Success;
        }
        */

        /// <summary>
        /// Runs the job with the specified parameters in asynchronous mode.
        /// </summary>
        /// <param name="jobResult">The job result ingoing object.</param>
        /// <param name="parameters">The parameters of the job.</param>
        /// <param name="sync">if set to <c>true</c> run synchronous.</param>
        /// <returns>the job results outgoing object for asynchronous execution.</returns>
        /// <exception cref="ArgumentException">Run needs a parameter named <see cref="MainParameterId"/></exception>
        /// <remarks>Running this method in synchronous mode with the <paramref name="sync" /> switch set, simply executes
        /// the <see cref="Job.Run" /> method after doing the async stuff.</remarks>
        protected override DeferredJobResult RunAsync(DeferredJobResult jobResult, JobParameters parameters, bool sync)
        {
            if(!parameters.ContainsKey(MainParameterId))
            {
                var message = string.Format("Run needs a parameter named {0}", MainParameterId);
                throw new ArgumentException(message, "parameters");
            }

            var path = parameters[MainParameterId];
            var success = this.IterateAsync(jobResult, path, 0, sync);
            return success;
        }

        private DeferredJobResult IterateAsync(DeferredJobResult jobResult, string path, int iterateDepth, bool sync)
        {
            var directoryInfo = new DirectoryInfo(path);
            if(!directoryInfo.Exists)
            {
                var message = string.Format("Parameter '{0}' points to a non existant directory.", MainParameterId);
                throw new ArgumentException(message, "path");
            }

            // Todo: Test this 
            bool success2;
            if(this.TraversePath(jobResult, sync, path, out success2))
            {
                var msg = "Traverse CONTINUE ROOT '" + path + "'.";
                this.LogDebug(msg);
                return jobResult;
            }

            var directories = directoryInfo.GetDirectories();
            foreach(var directory in directories)
            {
                var dirpath = directory.FullName;
                bool success;
                if(this.TraversePath(jobResult, sync, dirpath, out success))
                {
                    var msg = "Traverse CONTINUE ITER '" + dirpath + "'.";
                    this.LogDebug(msg);
                    continue;
                }

                jobResult.Success &= success;

                if(iterateDepth < this.Depth)
                {
                    var down = this.IterateAsync(jobResult, dirpath, iterateDepth + 1, sync);
                }
            }

            return jobResult;
        }

        private bool TraversePath(DeferredJobResult jobResult, bool sync, string dirpath, out bool success)
        {
            var msg = "Traversing '" + dirpath + "'.";
            this.LogDebug(msg);

            var jobParameters = new JobParameters();
            jobParameters.Add(MainParameterId, dirpath);
            success = Job.CallSpecifiedJobsAsync(jobResult, jobParameters, this.ChildrenJobs, sync).Success;
            if(!success)
            {
                msg = "Traverse CONTINUE TraversePath '" + dirpath + "'.";
                this.LogDebug(msg);
                jobResult.Success = true;
                return true;
            }

            return false;
        }
    }
}