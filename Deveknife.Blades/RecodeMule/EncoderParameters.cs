//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EncoderParameters.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.12.2013 18:53</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule.Encoding
{
    using Deveknife.Api;

    /// <summary>
    /// Represents Parameters for the Video Encoder.
    /// </summary>
    public class EncoderParameters : IEncoderParameters
    {
        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="EncoderParameters" />
        ///   </para>
        ///   <para>class.</para>
        /// </summary>
        /// <param name="videoBitRate">The video bit rate.</param>
        /// <param name="audioBitRate">The audio bit rate.</param>
        public EncoderParameters(ushort videoBitRate, ushort audioBitRate)
        {
            this.VideoBitRate = videoBitRate;
            this.AudioBitRate = audioBitRate;
            //this.FrameRate = 25;
            //this.Passes = 1;
        }

        /// <summary>
        /// Gets the audio bit rate.
        /// </summary>
        /// <value>
        /// The audio bit rate.
        /// </value>
        public ushort AudioBitRate { get; private set; }

        /// <summary>
        /// Gets the audio options.
        /// </summary>
        /// <value>The audio options.</value>
        public string AudioOptions { get; set; }

        /// <summary>
        /// Gets or sets the preset.
        /// </summary>
        /// <value>The preset.</value>
        public string Preset { get; set; }
        
        //public ushort Passes { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether the <see cref="Encoder" /> should
        /// overwrite output files.
        /// </summary>
        /// <remarks>
        /// Use only for development.
        /// </remarks>
        /// <value>
        /// <c>true</c> if overwrite; otherwise, <c>false</c> .
        /// </value>
        public bool DevelOverwrite { get; set; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Encoder" /> should
        /// perform a two pass encoding.
        /// </summary>
        /// <value>
        /// <c>true</c> if two pass encoding; otherwise, <c>false</c> .
        /// </value>
        public bool TwoPass { get; set; }

        /// <summary>
        /// Gets the video bit rate.
        /// </summary>
        /// <value>
        /// The video bit rate.
        /// </value>
        public ushort VideoBitRate { get; private set; }

        /// <summary>
        /// Gets the frame rate.
        /// </summary>
        /// <value>The frame rate.</value>
        public float FrameRate { get; set; }

        /// <summary>
        /// Gets the video options.
        /// </summary>
        /// <value>The video options.</value>
        public string VideoOptions { get; set; }
    }
}