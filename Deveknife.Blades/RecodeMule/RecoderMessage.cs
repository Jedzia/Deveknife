//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RecoderMessage.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>03.12.2013 00:52</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule
{
    using System;

    /// <summary>
    /// Handles <see cref="Recoder" /> Message Events.
    /// </summary>
    public class RecoderMessage : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecoderMessage" />
        /// class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RecoderMessage(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; private set; }
    }
}