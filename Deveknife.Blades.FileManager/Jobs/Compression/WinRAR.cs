// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinRAR.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>31.07.2015 16:43</date>
// <summary>
//   Defines the WinRAR type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs.Compression
{
    using System.Diagnostics;
    using System.Text;

    using Deveknife.Api;

    using Castle.Core.Logging;

    /// <summary>
    /// Rar executable stub.
    /// </summary>
    public class WinRar
    {
        private readonly ILogger logger;

        private readonly string winrarPath;

        private StringBuilder errsb;

        private StringBuilder outsb;

        /// <summary>
        /// Initializes a new instance of the <see cref="WinRar"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="winrarPath">The path to the winrar executable.</param>
        public WinRar(ILogger logger, string winrarPath)
        {
            // Guard.NotNull(() => logger, logger);
            this.logger = logger;
            Guard.NotNullOrEmpty(() => winrarPath, winrarPath);
            this.winrarPath = winrarPath;
        }

        /// <summary>
        /// Removes the specified input file.
        /// </summary>
        /// <param name="inputFile">The input file.</param>
        /// <param name="encoderParameters">The encoder parameters.</param>
        /// <param name="outFileFilter">The out file filter.</param>
        /// <returns>the start Process or null when set to debug, now.</returns>
        [CanBeNull]
        public Process Remove(
            string inputFile, 
            IWinrarParameters encoderParameters, 
            ITextFilter outFileFilter)
        {
            const bool Redirect = true;

            this.errsb = new StringBuilder();
            this.outsb = new StringBuilder();

            /*var processArgs = string.Format(
                "-y -i \"{0}\" {3} {1} {2} \"{4}\"", 
                "this.inFile.FullName", 
                "videoOptions", 
                "-an -f rawvideo", 
                "mapping", 
                "NUL");*/

            // E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform>"C:\Program Files\WinRAR\WinRAR.exe" a "E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform\Bla.rar"
            // "E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform\*" -r -ep1
            var processArgs = string.Format(
                "a \"{0}\" {1} {2} {3} \"{4}\"", 
                @"E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform\97429 Modern School Uniform.rar", 
                "-r", 
                "-ep1", 
                string.Empty, 
                @"E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform" + "\\*");
            const string WorkingDirectory = @"E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform";
            const bool Execute = false;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (Execute)
            {
                var process = this.StartProcess(Redirect, processArgs, WorkingDirectory);
                return process;
            }
            else
            {
                if (this.logger != null)
                {
                    this.logger.Info("{WinRAR} add " + "\"" + this.winrarPath + "\"" + " " + processArgs);
                }
            }

            return null;
        }

        private void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            var proc = sender as Process;
            if (proc == null)
            {
                return;
            }

            this.errsb.Append(dataReceivedEventArgs.Data);
            if (this.logger != null)
            {
                this.logger.Error(dataReceivedEventArgs.Data);
            }
        }

        private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            var proc = sender as Process;
            if (proc == null)
            {
                return;
            }

            this.outsb.Append(dataReceivedEventArgs.Data);
            if (this.logger != null)
            {
                this.logger.Info(dataReceivedEventArgs.Data);
            }
        }

        private Process StartProcess(bool redirect, string processArgs, string workingDirectory)
        {
            // It is possible to perform WinRAR commands from the command line. Common command line syntax is described below:
            // WinRAR <command> -<switch1> -<switchN> <archive> <files...> <@listfiles...> <path_to_extract\> 
            ProcessStartInfo si;
            if (redirect)
            {
                si = new ProcessStartInfo(this.winrarPath, processArgs)
                         {
                             UseShellExecute = false, 
                             RedirectStandardOutput = true, 
                             RedirectStandardError = true, 
                             CreateNoWindow = true
                         };
            }
            else
            {
                si = new ProcessStartInfo(this.winrarPath, processArgs);
            }

            si.WorkingDirectory = workingDirectory;
            if (this.logger != null)
            {
                this.logger.Info("\"" + this.winrarPath + "\"" + " " + processArgs);
            }

            var process = new Process { StartInfo = si };
            process.Start();
            process.PriorityClass = ProcessPriorityClass.Idle;

            if (redirect)
            {
                process.ErrorDataReceived += this.ProcessOnErrorDataReceived;
                process.OutputDataReceived += this.ProcessOnOutputDataReceived;
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }

            return process;
        }
    }
}