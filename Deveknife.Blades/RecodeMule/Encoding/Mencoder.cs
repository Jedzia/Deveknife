//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Mencoder.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.12.2013 13:21</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule.Encoding
{
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    using Deveknife.Api;
    using Deveknife.Blades.Utils.Filters;

    using Castle.Core.Logging;

    /// <summary>
    /// Implements an <see cref="Encoder" /> using mencoder.
    /// </summary>
    public class Mencoder : Encoder
    {
        /// <summary>
        /// The path to mencoder.
        /// </summary>
        private static string mencoderPath = @"D:\Program Files (x86)\mplayer\mencoder.exe";

        private readonly ILogger logger;

        /// <summary>
        /// The bitrate used for encoding.
        /// </summary>
        private string audioBitRate;

        /// <summary>
        /// The file to encode.
        /// </summary>
        private FileInfo inFile;

        /// <summary>
        /// The bitrate used for encoding.
        /// </summary>
        private string videoBitRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mencoder" /> class.
        /// </summary>
        /// <param name="logger">The logging facility.</param>
        public Mencoder(ILogger logger)
        {
            Guard.NotNull(() => logger, logger);
            this.logger = logger;
        }

        /// <summary>
        /// Gets or sets the path to mencoder.
        /// </summary>
        public static string MencoderPath
        {
            get
            {
                return mencoderPath;
            }

            set
            {
                mencoderPath = value;
            }
        }

        /// <summary>
        /// Gets the bitrate used for encoding.
        /// </summary>
        public string AudioBitRate
        {
            get
            {
                return this.audioBitRate;
            }
        }

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
                return ".avi";
            }
        }

        /// <summary>
        /// Gets the bitrate used for encoding.
        /// </summary>
        /// <value>
        /// The bitrate used for encoding.
        /// </value>
        public string VideoBitRate
        {
            get
            {
                return this.videoBitRate;
            }
        }

        /// <summary>
        /// Run the <see cref="Mencoder.Encode" /> action of this instance.
        /// </summary>
        /// <param name="file">The file to encode.</param>
        /// <param name="encoderParameters">The encoder parameters.</param>
        /// <param name="outFileFilter"></param>
        /// <returns>
        /// the started process encoding the video.
        /// </returns>
        public override Process Encode(string file, IEncoderParameters encoderParameters, ITextFilter outFileFilter)
        {
            var twoPass = false;
            this.inFile = new FileInfo(file);
            this.videoBitRate = encoderParameters.VideoBitRate.ToString(CultureInfo.InvariantCulture);
            this.audioBitRate = encoderParameters.AudioBitRate.ToString(CultureInfo.InvariantCulture);

            if (!this.inFile.Exists || this.inFile.Extension == ".avi")
            {
                var message2 = string.Format("Input file '{0}' does not exist", this.inFile.FullName);
                this.logger.Info(message2);
                return null;
            }

            var outFile = new FileInfo(Path.ChangeExtension(this.inFile.FullName, ".avi"));
            if (outFile.Exists)
            {
                var message2 = string.Format("Output file '{0}' already exist", outFile.FullName);
                this.logger.Info(message2);
                return null;
            }

            var message = string.Format(
                "Encoding '{0}' with bitrate V:{1}kbps/A:{2}kbps",
                this.inFile.Name,
                this.VideoBitRate,
                this.AudioBitRate);
            this.logger.Info(message);

            var outputFullname = outFile.FullName;
            //var aspectRatio = "-force-avi-aspect 1.12";
            var aspectRatio = "";
            var lavaspect = ":aspect=16/9";
            var lavvpass = "";
            if (twoPass)
            {
                lavvpass = ":vpass=1";
            }
            var videoOptions = string.Format(
                "-lavcopts vcodec=mpeg4:mbd=2:trell:threads=8{0}:vbitrate={1}{2}",
                lavaspect,
                this.VideoBitRate,
                lavvpass);
            var audioOptions = "-lameopts " + "cbr:br=" + this.AudioBitRate;
            var processArgs = "\"" + this.inFile.FullName + "\" " + "-ovc lavc " + videoOptions + " " + "-oac mp3lame "
                              + audioOptions + " " + aspectRatio + " " + "-o \"" + outputFullname + "\"";
            var si = new ProcessStartInfo(mencoderPath, processArgs);
            var process = new Process { StartInfo = si };
            process.Start();
            if (twoPass)
            {
                process.WaitForExit();

                lavvpass = ":vpass=2";
                videoOptions = string.Format(
                    "-lavcopts vcodec=mpeg4:mbd=2:trell:threads=8{0}:vbitrate={1}{2}",
                    lavaspect,
                    this.VideoBitRate,
                    lavvpass);
                processArgs = "\"" + this.inFile.FullName + "\" " + "-ovc lavc " + videoOptions + " " + "-oac mp3lame "
                              + audioOptions + " " + aspectRatio + " " + "-o \"" + outputFullname + "\"";
                si = new ProcessStartInfo(mencoderPath, processArgs);
                process = new Process { StartInfo = si };
                process.Start();
            }

            return process;
        }
    }
}