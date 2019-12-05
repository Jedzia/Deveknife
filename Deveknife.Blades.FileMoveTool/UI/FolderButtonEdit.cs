// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FolderButtonEdit.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.11.2016 15:56</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileMoveTool.UI
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Drawing;

    /// <summary>
    /// Represents a TextBox with a Button that fires up a folder browser.
    /// </summary>
    public class FolderButtonEdit : ButtonEdit
    {
        /// <summary>
        /// Gets or sets the dialog service.
        /// </summary>
        /// <value>The dialog service.</value>
        public IDialogService DialogService { get; set; }

        /// <summary>
        /// Called when [click button].
        /// </summary>
        /// <param name="buttonInfo">The button information.</param>
        protected override void OnClickButton(EditorButtonObjectInfoArgs buttonInfo)
        {
            ////MessageBox.Show("Folder Button Pressed");
            if(this.DialogService == null)
            {
                return;
            }

            var folder = this.DialogService.CreateFolderBrowserDialog().PromptFolderBrowserDialog();
            this.EditValue = folder;
        }
    }
}