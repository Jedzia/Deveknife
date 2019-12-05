// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEitListProcessor.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>24.02.2014 14:58</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System.Collections.Generic;

    /// <summary>
    /// Handles the work on EITFormatDisplay types.
    /// </summary>
    internal interface IEitListProcessor
    {
        /// <summary>
        /// Process the contents of the specified list of EITFormatDisplay's.
        /// </summary>
        /// <param name="eits">The list with EITFormatDisplay items to process.</param>
        void Apply(IEnumerable<EITFormatDisplay> eits);
    }
}