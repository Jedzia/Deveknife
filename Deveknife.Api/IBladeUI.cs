//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IBladeUI.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 12:43</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    /// <summary>
    /// Marks an <c>IBlade</c> User-Interface class.
    /// </summary>
    public interface IBladeUI
    {
        /// <summary>
        /// Gets the name of the <see cref="IBlade" />.
        /// </summary>
        /// <value>The name.</value>
        string BladeName { get; }

        /// <summary>
        /// Gets the corresponding blade for this User Interface.
        /// </summary>
        /// <value>The attached blade.</value>
        IBlade Blade { get; }
    }
}