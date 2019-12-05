//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FilesEventArgs.cs" company="EvePanix">Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>17.07.2015 14:28</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.FileManager.UI
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides data for the <see cref="E:ClassName.Event"/> event.
    /// </summary>
    [Serializable]
    public class FilesEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilesEventArgs"/> class.
        /// </summary>
        /// <param name="files">The files.</param>
        public FilesEventArgs(IEnumerable<string> files)
        {
            this.Files = files;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <value>The files.</value>
        public IEnumerable<string> Files { get; private set; }
    }
}