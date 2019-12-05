//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IBladeFactory.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 12:54</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides access to registered <see cref="IBlade" /> classes.
    /// </summary>
    public interface IBladeFactory
    {
        /// <summary>
        /// Creates all <see cref="IBlade" /> instances.
        /// </summary>
        /// <returns>
        /// the created IBlade's.
        /// </returns>
        IEnumerable<IBlade> CreateAll();
    }
}