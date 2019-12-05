// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridExplorer.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 10:35</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.UI
{
    using System;
    using System.Drawing;

    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Columns;
    using DevExpress.XtraGrid.Views.Base;

    /// <summary>
    /// UserControl with Grid as File-Explorer.
    /// </summary>
    public partial class GridExplorer : XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridExplorer"/> class.
        /// </summary>
        public GridExplorer()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// <para>
        /// Gets or sets the grid control's data source.
        /// </para>
        /// </summary>
        /// <value>
        /// An object representing the grid control's data source.
        /// </value>
        public object DataSource
        {
            get
            {
                return this.gridControl1.DataSource;
            }

            set
            {
                this.gridControl1.DataSource = value;
            }
        }

        private void AddFilesColumns(ColumnView cv)
        {
            this.CreateGridColumn(cv, "FileName", "Name", 0);
            this.CreateGridColumn(cv, "Length", "Length", 1);
            this.CreateGridColumn(cv, "CreationTime", "CreationTime", 2);
            this.CreateGridColumn(cv, "LastWriteTime", "LastWriteTime", 3);
            this.CreateGridColumn(cv, "LastAccessTime", "LastAccessTime", 4);
            this.CreateGridColumn(cv, "FileAttributes", "Attributes", 5);
        }

        /// <summary>
        /// Handles the ButtonClick event of the buttonEdit1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ButtonPressedEventArgs"/> instance containing the event data.</param>
        private void ButtonEdit1ButtonClick(object sender, ButtonPressedEventArgs e)
        {
        }

        private void CreateFormatConditions()
        {
            var appDef1 = new AppearanceDefault(
                              Color.OrangeRed,
                              Color.Empty,
                              new Font(AppearanceObject.DefaultFont, FontStyle.Bold | FontStyle.Italic));
            var appDef2 = new AppearanceDefault(
                              Color.White,
                              Color.CornflowerBlue,
                              Color.Empty,
                              Color.SteelBlue,
                              new Font(AppearanceObject.DefaultFont, FontStyle.Bold));
            var sfcFilesCount1 = new StyleFormatCondition(
                                     FormatConditionEnum.NotEqual,
                                     null,
                                     appDef1,
                                     0,
                                     0,
                                     this.gridView1.Columns["FilesCount"],
                                     false);
            var sfcFilesCount2 = new StyleFormatCondition(
                                     FormatConditionEnum.NotEqual,
                                     null,
                                     appDef1,
                                     0,
                                     0,
                                     this.gridView2.Columns["FilesCount"],
                                     false);
            var sfcDirCount1 = new StyleFormatCondition(
                                   FormatConditionEnum.NotEqual,
                                   null,
                                   appDef2,
                                   0,
                                   0,
                                   this.gridView1.Columns["ChildDirCount"],
                                   false);
            var sfcDirCount2 = new StyleFormatCondition(
                                   FormatConditionEnum.NotEqual,
                                   null,
                                   appDef2,
                                   0,
                                   0,
                                   this.gridView2.Columns["ChildDirCount"],
                                   false);
            this.gridView1.FormatConditions.AddRange(new[] { sfcFilesCount1, sfcDirCount1 });
            this.gridView2.FormatConditions.AddRange(new[] { sfcFilesCount2, sfcDirCount2 });
        }

        private GridColumn CreateGridColumn(ColumnView cv, string caption, string field, int visibleindex)
        {
            var gc = cv.Columns.Add();
            gc.Caption = caption;
            gc.FieldName = field;
            gc.VisibleIndex = visibleindex;
            gc.OptionsColumn.AllowEdit = false;
            if(gc.VisibleIndex == 0)
            {
                gc.SummaryItem.SummaryType = SummaryItemType.Count;
            }

            if(field.IndexOf("Time", StringComparison.Ordinal) > 0)
            {
                gc.DisplayFormat.FormatType = FormatType.DateTime;
                gc.DisplayFormat.FormatString = "g";
            }

            return gc;
        }

        /// <summary>
        /// Handles the Load event of the GridExplorer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GridExplorerLoad(object sender, EventArgs e)
        {
            this.AddFilesColumns(this.gridView1);
            this.AddFilesColumns(this.gridView2);
            this.AddFilesColumns(this.winExplorerView1);
        }
    }
}