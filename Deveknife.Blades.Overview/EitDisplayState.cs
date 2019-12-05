// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EitDisplayState.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>07.11.2015 22:46</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    /// <summary>
    /// Describes the state of an <see cref="EITFormatDisplay"/> entry.
    /// </summary>
    public enum EitDisplayState{
        /// <summary>
        /// The state is unknown or not initialized.
        /// </summary>
        Unknown,

        /// <summary>
        /// The state is normal state.
        /// </summary>
        Normal,

        /// <summary>
        /// The entry is not fixed.
        /// </summary>
        Unfixed,

        /// <summary>
        /// The entry is fixed.
        /// </summary>
        Fixed
    }
}