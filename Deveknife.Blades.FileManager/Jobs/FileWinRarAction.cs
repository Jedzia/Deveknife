// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileWinRarAction.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>31.07.2015 16:44</date>
// <summary>
//   Class Compare
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs
{
    using System;
    using System.IO;

    using Deveknife.Api;
    using Deveknife.Blades.FileManager.Jobs.Compression;
    using Deveknife.Blades.Utils.Filters;

    /// <summary>
    /// Class Compare
    /// </summary>
    public class FileWinRarAction : Job
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileWinRarAction"/> class.
        /// </summary>
        public FileWinRarAction()
            : base("FileWinRarAction")
        {
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return "FileWinRarAction";
        }

        /// <summary>
        /// Runs the job with the specified parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns><c>true</c> if completed without errors, <c>false</c> otherwise</returns>
        /// <exception cref="ArgumentException">Run needs a named parameter.</exception>
        protected internal override bool Run(JobParameters parameters)
        {
            if (!parameters.ContainsKey(Traverse.MainParameterId))
            {
                var msg = string.Format("Run needs a parameter named {0}", Traverse.MainParameterId);
                throw new ArgumentException(msg, "parameters");
            }

            var path = parameters[Traverse.MainParameterId];
            var directoryInfo = new DirectoryInfo(path);
            if (directoryInfo.Exists)
            {
                // var directories = directoryInfo.GetDirectories().Select(info => info.Name).ToList();
                // var files = directoryInfo.GetFiles().Select(info => info.Name).ToList();
                // var all = directoryInfo.GetFileSystemInfos().Select(info => info.Name).ToList();
                // var allStr = all.Aggregate((s, s1) => s + ", " + s1);
                var winrarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
                var rarRunner = new WinRar(this.Logger, winrarPath);

                // string path = @"E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform";
                IWinrarParameters winrarParameters = new WinrarParameters();
                ITextFilter filter = new DefaultTextFilter();
                var message = "Running FileWinRarAction on '" + path + "'.";
                this.LogDebug(message);

                // var process = rarRunner.Remove(path, winrarParameters, filter);
                rarRunner.Remove(path, winrarParameters, filter);
            }

            return true;
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
        /// the <see cref="Run" /> method after doing the async stuff.</remarks>
        protected override DeferredJobResult RunAsync(DeferredJobResult jobResult, JobParameters parameters, bool sync)
        {
            if (!parameters.ContainsKey(Traverse.MainParameterId))
            {
                var msg = string.Format("Run needs a parameter named {0}", Traverse.MainParameterId);
                throw new ArgumentException(msg, "parameters");
            }

            var rarPath = parameters[Traverse.MainParameterId];
            var message = "Deferred FileWinRarAction on '" + rarPath + "'.";
            this.LogInfo(message);

            // jobResult.Add((Job)this.MemberwiseClone(), parameters);
            jobResult.Add(this, parameters);
            if (sync)
            {
                jobResult.Success &= this.Run(parameters);
            }

            return jobResult;
        }
    }
}