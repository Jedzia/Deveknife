//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IEncoder.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 12:48</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    using System.Diagnostics;

    /// <summary>
    /// Provides encoding capability.
    /// </summary>
    public interface IEncoder
    {
        /// <summary>
        /// Gets the default extension for the encoder.
        /// </summary>
        /// <value>
        /// The default extension.
        /// </value>
        string DefaultExtension { get; }

        /// <summary>
        /// Run the <see cref="Encode" /> action of this instance.
        /// </summary>
        /// <param name="file">The file to encode.</param>
        /// <param name="encoderParameters">The encoder parameters.</param>
        /// <param name="outFileFilter">The output filename transformation filter.</param>
        /// <returns>the started process encoding the video.</returns>
        Process Encode(string file, IEncoderParameters encoderParameters, ITextFilter outFileFilter);

    }
}