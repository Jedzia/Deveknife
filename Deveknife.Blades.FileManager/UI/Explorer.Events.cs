// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Explorer.Events.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 10:33</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.UI
{
    using System;
    using System.Threading;

    /// <summary>
    /// An explorer-like control for directory navigation.
    /// </summary>
    public partial class TreeListExplorer
    {
        /// <summary>
        /// Occurs when a directory is changed.
        /// </summary>
        public event EventHandler<DirectoryChangedEventArgs> DirectoryChanged;

        /// <summary>
        /// Occurs when a selection change makes files available.
        /// </summary>
        public event EventHandler<FilesEventArgs> FilesAvailable;

        /// <summary>
        /// Raises the <see cref="E:DirectoryChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="DirectoryChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="NullReferenceException">The address of DirectoryChanged is a null pointer. </exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        protected virtual void OnDirectoryChanged(DirectoryChangedEventArgs e)
        {
            var handler = Interlocked.CompareExchange(ref this.DirectoryChanged, null, null);
            if(handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:GridExplorer.FilesAvailable"/> event.
        /// </summary>
        /// <param name="e">The <see cref="FilesEventArgs"/> instance containing the event data.</param>
        /// <exception cref="NullReferenceException">The address of DirectoryChanged is a null pointer. </exception>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        protected virtual void OnFilesAvailable(FilesEventArgs e)
        {
            var handler = Interlocked.CompareExchange(ref this.FilesAvailable, null, null);
            if(handler != null)
            {
                handler(this, e);
            }
        }
    }
}