//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FFprobeParser.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>27.12.2013 17:58</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule.Encoding
{
    using NCalc;

    using global::RecodeMule.Lib.Utils;

    public class VideoDataInfo
    {
        public double AvgFramerate { get; set; }

        public float Duration { get; set; }

        public int TotalFrames
        {
            get
            {
                return (int)(this.Duration * this.AvgFramerate);
            }
        }
    }

    /// <summary>
    /// ParseMapping outputs from the ffmpeg probe utility.
    /// </summary>
    internal class FFprobeParser
    {
        private readonly ffprobeType probedData;

        /// <summary>
        /// Initializes a new instance of the <see cref="FFprobeParser" />
        /// class.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        public FFprobeParser(string xmlData)
        {
            this.probedData = ffprobeType.Deserialize(xmlData);
        }

        /// <summary>
        /// Parses the specified ffprobe XML data for possible stream mappings.
        /// </summary>
        /// <param name="xmlData">The XML data.</param>
        /// <returns>
        /// a command-line argument for ffmpeg that use all possible stream
        /// mappings.
        /// </returns>
        public string ParseMapping()
        {
            var result = string.Empty;
            foreach (var stream in this.probedData.streams)
            {
                var cln = stream.codec_name;
                var cindex = stream.index;
                var isWantedStream = cln != "dvbsub" && cln != "dvb_teletext";
                if (!string.IsNullOrWhiteSpace(cln) && isWantedStream)
                {
                    result += string.Format("-map 0:{0} ", cindex);
                }
            }

            return result;
        }

        public VideoDataInfo ParseVideoData()
        {
            foreach (var streamType in this.probedData.streams)
            {
                if (streamType.codec_type == "video")
                {
                    if (streamType.durationSpecified)
                    {
                        var vd = new VideoDataInfo();
                        var avgFramerateString = streamType.avg_frame_rate;
                        var avgFramerateExpr = new Expression(avgFramerateString);
                        vd.AvgFramerate = (double)avgFramerateExpr.Evaluate();
                        vd.Duration = streamType.duration;
                        return vd;
                    }
                }
            }
            return null;
        }
    }
}