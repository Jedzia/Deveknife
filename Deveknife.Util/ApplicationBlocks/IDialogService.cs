// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDialogService.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.11.2016 16:05</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife
{
    /// <summary>
    /// Provides application wide dialog services.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Gets the folder browser dialog wrapper.
        /// </summary>
        /// <returns>the FolderBrowser Dialog wrapper.</returns>
        IFolderBrowserDialog CreateFolderBrowserDialog();
    }

    /// <summary>
    /// Implements a  folder browser dialog.
    /// </summary>
    public interface IFolderBrowserDialog
    {
        /// <summary>
        /// Presents a common dialog box that allows the user to specify options for 
        /// selecting a folder.
        /// </summary>
        /// <returns>the location of the selected folder or null if canceled.</returns>
        string PromptFolderBrowserDialog();
    }
}