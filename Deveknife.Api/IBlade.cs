// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBlade.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>26.02.2014 00:23</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Api
{
    using System.Windows.Forms;

    /// <summary>
    /// Represents a loadable tool with GUI.
    /// </summary>
    public interface IBlade
    {
        /*/// <summary>
        /// Gets or sets the host API object.
        /// </summary>
        /// <value>The host.</value>
        IHost Host { get; set; }*/

        /// <summary>
        /// Gets the name of the <see cref="IBlade"/>.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Creates the User-Interface control.
        /// </summary>
        /// <returns>a UserControl with the User-Interface of the <see cref="IBlade"/>.</returns>
        UserControl CreateControl();
    }
}