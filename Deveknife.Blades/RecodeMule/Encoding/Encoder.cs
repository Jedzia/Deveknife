//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Encoder.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 10:29</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule.Encoding
{
    using System.Diagnostics;

    using Deveknife.Api;

    /// <summary>
    /// A Encoder, able to process videos.
    /// </summary>
    public abstract class Encoder : IEncoder
    {
        /// <summary>
        /// Gets the default extension for the encoder.
        /// </summary>
        /// <value>
        /// The default extension.
        /// </value>
        public abstract string DefaultExtension { get; }

        /// <summary>
        /// Run the <see cref="Encode" /> action of this instance.
        /// </summary>
        /// <param name="file">The file to encode.</param>
        /// <param name="encoderParameters">The encoder parameters.</param>
        /// <param name="outFileFilter">The output filename transformation filter.</param>
        /// <returns>the started process encoding the video.</returns>
        public abstract Process Encode(string file, IEncoderParameters encoderParameters, ITextFilter outFileFilter);
    }
}