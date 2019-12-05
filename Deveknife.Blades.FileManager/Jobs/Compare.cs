// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Compare.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>31.07.2015 16:41</date>
// <summary>
//   Class Compare
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security;

    /// <summary>
    /// Class Compare
    /// </summary>
    public class Compare : Job
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Compare"/> class.
        /// </summary>
        public Compare()
            : base("Compare")
        {
        }

        /// <summary>
        /// Runs the job with the specified parameters in asynchronous mode.
        /// </summary>
        /// <param name="jobResult">The job result ingoing object.</param>
        /// <param name="parameters">The parameters of the job.</param>
        /// <param name="sync">if set to <c>true</c> run synchronous.</param>
        /// <returns>the job results outgoing object for asynchronous execution.</returns>
        /// <exception cref="ArgumentException">Run needs a parameter named <see cref="Traverse.MainParameterId"/>.</exception>
        /// <remarks>Running this method in synchronous mode with the <paramref name="sync" /> switch set, simply executes
        /// the <see cref="Job.Run" /> method after doing the async stuff.</remarks>
        protected override DeferredJobResult RunAsync(DeferredJobResult jobResult, JobParameters parameters, bool sync)
        {
            if (!parameters.ContainsKey(Traverse.MainParameterId))
            {
                var message = string.Format("Run needs a parameter named {0}", Traverse.MainParameterId);
                throw new ArgumentException(message, "parameters");
            }

            var path = parameters[Traverse.MainParameterId];
            DirectoryInfo directoryInfo;
            try
            {
                directoryInfo = new DirectoryInfo(path);
            }
            catch (SecurityException securityException)
            {
                this.PostException(jobResult, path, securityException, "Security Exception while reading directory");
                return jobResult;
            }

            if (!directoryInfo.Exists)
            {
                var message = string.Format(
                    "Parameter '{0}' points to a non existant directory.", 
                    Traverse.MainParameterId);
                throw new ArgumentException(message, "parameters");
            }

            List<string> directories;
            try
            {
                directories = directoryInfo.GetDirectories().Select(info => info.Name.ToLower()).ToList();
            }
            catch (SecurityException securityException)
            {
                this.PostException(
                    jobResult, 
                    path, 
                    securityException, 
                    "Security Exception while getting directories from");
                return jobResult;
            }
            catch (DirectoryNotFoundException directoryNotFoundException)
            {
                this.PostException(
                    jobResult, 
                    path, 
                    directoryNotFoundException, 
                    "Security Exception while getting directories from");
                return jobResult;
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                this.PostException(
                    jobResult, 
                    path, 
                    unauthorizedAccessException, 
                    "Security Exception while getting directories from");
                return jobResult;
            }

            if (directories.Contains("content") || directories.Contains("runtime"))
            {
                // var jobParameters = new JobParameters();
                // jobParameters.Add(MainParameterId, dirpath);
                var msg = "Compare Directory success on '" + path + "'.";
                this.LogInfo(msg);
                // success.Success &= CallSpecifiedJobsAsync(jobResult, parameters, this.ChildrenJobs, sync).Success;
                var foundSuccess = Job.CallSpecifiedJobsAsync(jobResult, parameters, this.ChildrenJobs, sync).Success;
                //jobResult.Success &= foundSuccess;
                jobResult.Success = false;
            }

            return jobResult;
        }

        private void PostException(DeferredJobResult jobResult, string path, Exception exception, string title)
        {
            jobResult.Success = false;
            var message = string.Format("{2} '{0}' {1}", path, exception.Message, title);
            this.LogError(message);
        }
    }
}