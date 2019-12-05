//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IEncoderParameters.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.12.2013 19:03</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    public interface IEncoderParameters
    {
        /// <summary>
        /// Gets the audio bit rate.
        /// </summary>
        /// <value>
        /// The audio bit rate.
        /// </value>
        ushort AudioBitRate { get; }

        /// <summary>
        /// Gets the audio options.
        /// </summary>
        /// <value>
        /// The audio options.
        /// </value>
        string AudioOptions { get; }

        /// <summary>
        /// Gets a value indicating whether the Encoder should overwrite output
        /// files.
        /// </summary>
        /// <remarks>
        /// Use only for development.
        /// </remarks>
        /// <value>
        /// <c>true</c> if overwrite; otherwise, <c>false</c> .
        /// </value>
        bool DevelOverwrite { get; set; }

        /// <summary>
        /// Gets or sets the preset.
        /// </summary>
        /// <value>
        /// The preset.
        /// </value>
        string Preset { get; set; }

        /// <summary>
        /// Gets a value indicating whether the Encoder should perform a two
        /// pass encoding.
        /// </summary>
        /// <value>
        /// <c>true</c> if two pass encoding; otherwise, <c>false</c> .
        /// </value>
        bool TwoPass { get; set; }

        /// <summary>
        /// Gets the video bit rate.
        /// </summary>
        /// <value>
        /// The video bit rate.
        /// </value>
        ushort VideoBitRate { get; }

        /// <summary>
        /// Gets the frame rate.
        /// </summary>
        /// <value>The frame rate.</value>
        float FrameRate { get; }

        /// <summary>
        /// Gets the video options.
        /// </summary>
        /// <value>
        /// The video options.
        /// </value>
        string VideoOptions { get; }
    }
}