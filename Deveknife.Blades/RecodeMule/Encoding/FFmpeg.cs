//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FFmpeg.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>27.12.2013 19:53</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule.Encoding
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Text;

    using Deveknife.Api;

    using Castle.Core.Logging;

    /// <summary>
    /// Implements an video encoder using ffmpeg.
    /// </summary>
    public class FFmpeg : Encoder
    {
        /// <summary>
        /// The path to ffmpeg.
        /// </summary>
        private readonly string ffmpegPath;

        private readonly IHost host;

        private readonly ILogger logger;

        private readonly FFprobe probe;

        private StringBuilder errsb;

        /// <summary>
        /// The file to encode.
        /// </summary>
        private FileInfo inFile;

        private StringBuilder outsb;

        private IEncoderParameters currentEncoderParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mencoder" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="ffmpegPath">The ffmpeg path.</param>
        /// <param name="probe">The ffprobe helper.</param>
        public FFmpeg(ILogger logger, string ffmpegPath, FFprobe probe, IHost host)
        {
            Guard.NotNull(() => logger, logger);
            this.logger = logger;
            Guard.NotNullOrEmpty(() => ffmpegPath, ffmpegPath);
            this.ffmpegPath = ffmpegPath;
            Guard.NotNull(() => probe, probe);
            this.probe = probe;
            Guard.NotNull(() => host, host);
            this.host = host;
        }

        /// <summary>
        /// Gets the bitrate used for encoding.
        /// </summary>
        public string AudioBitRate { get; private set; }

        /// <summary>
        /// Gets the default extension for the encoder.
        /// </summary>
        /// <value>
        /// The default extension.
        /// </value>
        public override string DefaultExtension
        {
            get
            {
                return ".mkv";
            }
        }

        /// <summary>
        /// Gets the bitrate used for encoding.
        /// </summary>
        /// <value>
        /// The bitrate used for encoding.
        /// </value>
        public string VideoBitRate { get; private set; }

        /// <summary>
        /// Run the <see cref="FFmpeg.Encode" /> action of this instance.
        /// </summary>
        /// <param name="inputFile">The file to encode.</param>
        /// <param name="encoderParameters">The encoder parameters.</param>
        /// <param name="outFileFilter">
        /// The output filename transformation filter.
        /// </param>
        /// <returns>
        /// the started process encoding the video.
        /// </returns>
        public override Process Encode(
            string inputFile, IEncoderParameters encoderParameters, ITextFilter outFileFilter)
        {
            const bool redirect = true;
            var twoPass = encoderParameters.TwoPass;
            this.inFile = new FileInfo(inputFile);
            this.VideoBitRate = encoderParameters.VideoBitRate.ToString(CultureInfo.InvariantCulture);
            this.AudioBitRate = encoderParameters.AudioBitRate.ToString(CultureInfo.InvariantCulture);

            // removed avi skipping for input file... problems ?
            if (!this.inFile.Exists /*|| this.inFile.Extension == ".avi"*/)
            {
                var message2 = string.Format("Input file '{0}' does not exist", this.inFile.FullName);
                this.logger.Info(message2);
                return null;
            }

            //var probe = new FFprobe(@"D:\Program Files\ffmpeg\bin\ffprobe.exe", null);
            this.probe.ProbeLog(this.inFile.FullName);

            // Apply path filter
            var outFile = outFileFilter.Apply(Path.ChangeExtension(this.inFile.FullName, this.DefaultExtension));
            var outFileInfo = new FileInfo(outFile);
            if ((!encoderParameters.DevelOverwrite) && outFileInfo.Exists)
            {
                var message2 = string.Format("Output file '{0}' already exist", outFileInfo.FullName);
                this.logger.Info(message2);
                return null;
            }

            var message = string.Format(
                "Encoding '{0}' with bitrate V:{1}kbps/A:{2}kbps",
                this.inFile.Name,
                this.VideoBitRate,
                this.AudioBitRate);
            this.logger.Info(message);

            var outputFullname = outFileInfo.FullName;

            this.errsb = new StringBuilder();
            this.outsb = new StringBuilder();
            this.currentEncoderParameters = encoderParameters;

            // var aspectRatio = "-force-avi-aspect 1.12";
            // var aspectRatio = "";
            // var lavaspect = ":aspect=16/9";
            var passArg = "";
            if (twoPass)
            {
                // ffmpeg -y -i input -c:v libx264 -preset medium -b:v 555k -pass 1 -an -f mp4 /dev/null && \
                // ffmpeg -i input -c:v libx264 -preset medium -b:v 555k -pass 2 -c:a libfdk_aac -b:a 128k output.mp4
                passArg = "-pass 1";
            }

            this.probe.Detect(this.inFile.FullName);
            var mapping = this.probe.StreamMapping;
            //var mapping = "-map 0:0 -map 0:1";

            var overwrite = "";
            if (encoderParameters.DevelOverwrite)
            {
                overwrite = "-y";
            }
            //var mapping = "-map 0:1 -map 0:2";
            //var preset = "fast";
            //var preset = "medium";
            //var preset = "veryslow";
            var preset = "slow";
            if (!string.IsNullOrWhiteSpace(encoderParameters.Preset))
            {
                preset = encoderParameters.Preset;
            }

            var videoOptionsFormat = "-c:v libx264 -preset {2} -b:v {0}k {1}";
            if (!string.IsNullOrWhiteSpace(encoderParameters.VideoOptions))
            {
                videoOptionsFormat = encoderParameters.VideoOptions;
            }
            
            var frameRate = "-r 25";
            if (encoderParameters.FrameRate > 0)
            {
                frameRate = "-r " + encoderParameters.FrameRate.ToString(CultureInfo.InvariantCulture);
            }

            var videoOptions = string.Format(
                videoOptionsFormat, this.VideoBitRate, passArg, preset, frameRate);
            this.logger.Info("videoOptions='" + videoOptions + "'");
            //var audioOptions = string.Format("-c:a libvo_aacenc -b:a {0}k", this.AudioBitRate);
            var audioOptionsFormat = "-strict -2 -c:a aac -b:a {0}k";
            if (!string.IsNullOrWhiteSpace(encoderParameters.AudioOptions))
            {
                audioOptionsFormat = encoderParameters.AudioOptions;
            }

            var audioOptions = string.Format(audioOptionsFormat, this.AudioBitRate);
            this.logger.Info("audioOptions='" + audioOptions + "'");

            // ffmpeg -i input -c:v libx264 -preset medium -b:v 555k -pass 2 -c:a libfdk_aac -b:a 128k output.mp4
            // libvo_aacenc
            var processArgs = string.Format(
                "{5} -i \"{0}\" {3} {1} {2} \"{4}\"",
                this.inFile.FullName,
                videoOptions,
                audioOptions,
                mapping,
                outputFullname,
                overwrite);
            if (twoPass)
            {
                // -f rawvideo or mp4 ?
                // -y overwrite
                processArgs = string.Format(
                    "-y -i \"{0}\" {3} {1} {2} \"{4}\"",
                    this.inFile.FullName,
                    videoOptions,
                    "-an -f rawvideo",
                    mapping,
                    "NUL");
            }

            var process = this.StartProcess(redirect, processArgs);

            if (!twoPass)
            {
                return process;
            }

            //var stdout = process.StandardOutput.ReadToEnd();
            //var stderr = process.StandardError.ReadToEnd();
            //this.logger.Warn(stderr);
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                return process;
            }

            passArg = "-pass 2";
            videoOptions = string.Format(
                videoOptionsFormat, this.VideoBitRate, passArg, preset, frameRate);
            this.logger.Info("videoOptions='" + videoOptions + "'");
            this.logger.Info("audioOptions='" + audioOptions + "'");

            processArgs = string.Format(
                "{5} -i \"{0}\" {3} {1} {2} \"{4}\"",
                this.inFile.FullName,
                videoOptions,
                audioOptions,
                mapping,
                outputFullname,
                overwrite);
            process = this.StartProcess(redirect, processArgs);
            return process;
        }

        private void ProcessOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            var proc = sender as Process;
            if (proc == null)
            {
                return;
            }
            if (dataReceivedEventArgs.Data == null)
            {
                return;
            }

            var lines = dataReceivedEventArgs.Data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line.ToLower().Contains("error"))
                {
                    this.logger.Error(line);
                }
                else
                {
                    this.logger.Debug(line);
                }

                if (!line.ToLower().StartsWith("frame="))
                {
                    continue;
                }

                var tmpData = line.Split(new[] { "frame=", " ", "fps=", "q=", "size=", "time=", "bitrate=" }, StringSplitOptions.RemoveEmptyEntries);
                var statusData = new StatusData(line);
                if (tmpData.Length > 0)
                {
                    int curFrame;
                    var success = int.TryParse(tmpData[0], out curFrame);
                    if (success)
                    {
                        var videoDataInfo = this.probe.VideoDataInfo;
                        if (videoDataInfo != null)
                        {
                            var totalFrames = videoDataInfo.TotalFrames;
                            if (this.currentEncoderParameters.FrameRate > 0)
                            {
                                var frameFactor = this.currentEncoderParameters.FrameRate / videoDataInfo.AvgFramerate;
                                totalFrames = (int) (totalFrames * frameFactor);
                            }
                            statusData.SetProgress(curFrame, totalFrames);
                        }
                    }
                }
                this.host.DisplayStatus(statusData);
            }
            this.errsb.Append(dataReceivedEventArgs.Data);
        }

        private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            var proc = sender as Process;
            if (proc == null)
            {
                return;
            }
            this.outsb.Append(dataReceivedEventArgs.Data);
            this.logger.Info(dataReceivedEventArgs.Data);
        }

        private Process StartProcess(bool redirect, string processArgs)
        {
            ProcessStartInfo si;
            if (redirect)
            {
                si = new ProcessStartInfo(this.ffmpegPath, processArgs)
                         {
                             UseShellExecute = false,
                             RedirectStandardOutput = true,
                             RedirectStandardError = true,
                             CreateNoWindow = true,
                         };
            }
            else
            {
                si = new ProcessStartInfo(this.ffmpegPath, processArgs);
            }

            this.logger.Info("\"" + this.ffmpegPath + "\"" + " " + processArgs);
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