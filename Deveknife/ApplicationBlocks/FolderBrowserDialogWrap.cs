// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FolderBrowserDialogWrap.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.11.2016 15:37</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife
{
    using System.Windows.Forms;

    /// <summary>
    /// Implements a folder browser dialog.
    /// </summary>
    public class FolderBrowserDialogWrap : IFolderBrowserDialog
    {
        /// <summary>
        /// Presents a common dialog box that allows the user to specify options for
        /// selecting a folder.
        /// </summary>
        /// <returns>the location of the selected folder or null if canceled.</returns>
        public string PromptFolderBrowserDialog()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();

            return dialog.SelectedPath;
        }
    }
}