// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitRegisterUiGridMenuItem.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>21.02.2014 19:22</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.GitRegister
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// A menu item storage class for the <see cref="GitRegisterUiGridMenu{T}" /> class.
    /// </summary>
    /// <typeparam name="T">Underlying type of the Gridview's data to store.</typeparam>
    internal class GitRegisterUiGridMenuItem<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GitRegisterUiGridMenuItem{T}" /> class.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="action">The action.</param>
        /// <param name="image">The image.</param>
        public GitRegisterUiGridMenuItem(
            string caption,
            Action<IEnumerable<T>, string> action,
            Image image = null)
        {
            this.Caption = caption;
            this.Action = action;
            this.Image = image;
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        public Action<IEnumerable<T>, string> Action { get; private set; }

        /// <summary>
        /// Gets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string Caption { get; private set; }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image { get; private set; }
    }
}