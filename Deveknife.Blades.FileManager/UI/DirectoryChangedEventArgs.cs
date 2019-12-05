// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryChangedEventArgs.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>24.07.2015 00:18</date>
// <summary>
//   Provides data for the <see cref="E:TreeListExplorer.DirectoryChanged" /> event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.UI
{
    using System;

    /// <summary>
    /// Provides data for the <see cref="E:TreeListExplorer.DirectoryChanged"/> event.
    /// </summary>
    [Serializable]
    public class DirectoryChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryChangedEventArgs"/> class.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public DirectoryChangedEventArgs(string directory)
        {
            this.Directory = directory;
        }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <value>The directory.</value>
        public string Directory { get; private set; }
    }
}