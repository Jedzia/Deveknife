// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListingDialogForm.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>04.03.2014 09:46</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Deveknife.Api;

    using DevExpress.XtraEditors;

    public partial class ListingDialogForm : XtraForm, IListingDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListingDialogForm" /> class.
        /// </summary>
        public ListingDialogForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListingDialogForm" /> class.
        /// </summary>
        /// <param name="topic">The topic.</param>
        public ListingDialogForm(string topic)
            : this()
        {
            this.memoEdit1.Text = topic;
        }

        public DialogResult ShowDialog(IEnumerable<string> items)
        {
            this.AddListItems(items);
            return this.ShowDialog();
        }

        public DialogResult ShowDialog(IWin32Window owner, IEnumerable<string> items)
        {
            this.AddListItems(items);
            return this.ShowDialog(owner);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.lbItems.Items.Clear();
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void AddListItems(IEnumerable<string> items)
        {
            foreach (var item in items)
            {
                this.lbItems.Items.Add(item);
            }
        }

        private void EitDialogForm_Load(object sender, EventArgs e)
        {
        }
    }
}