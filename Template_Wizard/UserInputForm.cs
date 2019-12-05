// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserInputForm.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 14:00</date>
// --------------------------------------------------------------------------------------------------------------------
namespace CustomWizard
{
    using System;
    using System.Windows.Forms;

    public partial class UserInputForm : Form
    {
        private string lastProjectName;

        public UserInputForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the text of the tbLastProjectName TextBox.
        /// </summary>
        /// <value>The text.</value>
        public string LastProjectNameText
        {
            get
            {
                return this.tbLastProjectName.Text;
            }

            set
            {
                this.tbLastProjectName.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the text of the textBox2 TextBox.
        /// </summary>
        /// <value>The text.</value>
        public string TextBox2Text
        {
            get
            {
                return this.textBox2.Text;
            }

            set
            {
                this.textBox2.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the text of the BuildDate TextBox.
        /// </summary>
        /// <value>The text.</value>
        public string TextBoxBuildDateText
        {
            get
            {
                return this.tbBuildDate.Text;
            }

            set
            {
                this.tbBuildDate.Text = value;
            }
        }

        /// <summary>
        /// Gets the last name of the project.
        /// </summary>
        /// <returns>the value of the LastProjectName user input.</returns>
        public string GetLastProjectName()
        {
            return this.lastProjectName;
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button1Click(object sender, EventArgs e)
        {
            this.lastProjectName = this.tbLastProjectName.Text;
            this.Dispose();
        }
    }
}