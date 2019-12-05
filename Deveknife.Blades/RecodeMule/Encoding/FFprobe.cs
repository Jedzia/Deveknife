//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FFprobe.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 12:14</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule.Encoding
{
    using System.Diagnostics;
    using System.IO;

    using Deveknife.Api;

    using Castle.Core.Logging;

    /// <summary>
    /// Encapsulates a call to the external program 'ffprobe'.
    /// </summary>
    public class FFprobe : IEncoderProbe
    {
        /// <summary>
        /// The action used for logging.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The path to ffmpeg.
        /// </summary>
        private readonly string proberPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FFprobe" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="ffprobePath">The full path to the ffprobe executable.</param>
        public FFprobe(ILogger logger, string ffprobePath)
        {
            Guard.NotNull(() => logger, logger);
            this.logger = logger;
            Guard.NotNullOrEmpty(() => ffprobePath, ffprobePath);
            this.proberPath = ffprobePath;
        }

        /// <summary>
        /// Gets the stream mapping.
        /// </summary>
        /// <value>
        /// The stream mapping.
        /// </value>
        public string StreamMapping { get; private set; }

        /// <summary>
        /// Run the Encode action of this instance.
        /// </summary>
        /// <param name="file">The file to encode.</param>
        /// <returns>
        /// the started process encoding the video.
        /// </returns>
        public void Detect(string file)
        {
            var inFile = new FileInfo(file);

            if (!inFile.Exists)
            {
                var message2 = string.Format("Input file '{0}' does not exist", inFile.FullName);
                this.logger.Info(message2);

                return;
            }

            var processArgs = string.Format("-v quiet  -show_streams  -print_format xml \"{0}\"", inFile.FullName);
            var si = new ProcessStartInfo(this.proberPath, processArgs)
                         {
                             UseShellExecute = false,
                             RedirectStandardOutput = true,
                             RedirectStandardError = true,
                             CreateNoWindow = true
                         };
            this.logger.Info("\"" + this.proberPath + "\"" + " " + processArgs);

            var process = new Process { StartInfo = si };
            process.Start();

            var stdout = process.StandardOutput.ReadToEnd();
            var stderr = process.StandardError.ReadToEnd();
            this.logger.Warn(stderr);

            var parser = new FFprobeParser(stdout);
            this.StreamMapping = parser.ParseMapping();
            this.VideoDataInfo = parser.ParseVideoData();
            process.WaitForExit();
        }

        public VideoDataInfo VideoDataInfo { get; private set; }

        /// <summary>
        /// Probes the specified file, only for logging.
        /// </summary>
        /// <param name="filename">The file name.</param>
        public void ProbeLog(string filename)
        {
            var processArgs = string.Format("\"{0}\"", filename);
            var si = new ProcessStartInfo(this.proberPath, processArgs)
                         {
                             UseShellExecute = false,
                             RedirectStandardOutput = true,
                             RedirectStandardError = true,
                             CreateNoWindow = true
                         };
            this.logger.Info("\"" + this.proberPath + "\"" + " " + processArgs);

            var process = new Process { StartInfo = si };
            process.OutputDataReceived += this.ProcessOutputDataReceived;
            process.ErrorDataReceived += this.ProcessOutputDataReceived;
            //process.EnableRaisingEvents = true;
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            process.WaitForExit();
        }

        /// <summary>
        /// Handles the OutputDataReceived event of the process control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.Diagnostics.DataReceivedEventArgs" /> instance
        /// containing the event data.
        /// </param>
        private void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // var process = sender as Process;
            this.logger.Info(e.Data);
        }
    }
}