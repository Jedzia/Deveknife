namespace Deveknife.Blades.Overview
{
    partial class VideoOverviewUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbEitFiles = new System.Windows.Forms.ListBox();
            this.btnFetch = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.eITFormatDisplayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIcon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colTag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFilename = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAudio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBeschreibung = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDauer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEventName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEventPicture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEventLanguage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEventType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVPid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVerschluesselung = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colZeit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colColor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTest = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsFixed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDisplayType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbOverviewLog = new System.Windows.Forms.TextBox();
            this.cnt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCntClearText = new System.Windows.Forms.ToolStripMenuItem();
            this.pbProgress = new DevExpress.XtraEditors.ProgressBarControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFix = new System.Windows.Forms.Button();
            this.btnCleanupDone = new System.Windows.Forms.Button();
            this.btnTestFilter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbSoundex = new System.Windows.Forms.CheckBox();
            this.btnComparerClear = new System.Windows.Forms.Button();
            this.btnComparerSet = new System.Windows.Forms.Button();
            this.btnFetchFolder = new System.Windows.Forms.Button();
            this.btnSaveLayout = new System.Windows.Forms.Button();
            this.btnFetchFtp = new System.Windows.Forms.Button();
            this.toolTipController1 = new Deveknife.Blades.Overview.WordcountingTooltipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eITFormatDisplayBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.cnt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbEitFiles
            // 
            this.lbEitFiles.FormattingEnabled = true;
            this.lbEitFiles.HorizontalScrollbar = true;
            this.lbEitFiles.Location = new System.Drawing.Point(2, 59);
            this.lbEitFiles.Name = "lbEitFiles";
            this.lbEitFiles.Size = new System.Drawing.Size(194, 121);
            this.lbEitFiles.TabIndex = 0;
            // 
            // btnFetch
            // 
            this.btnFetch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetch.Location = new System.Drawing.Point(12, 439);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(171, 38);
            this.btnFetch.TabIndex = 1;
            this.btnFetch.Text = "Fetch";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.BtnFetchClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridControl1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.btnTestFilter);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveLayout);
            this.splitContainer1.Panel2.Controls.Add(this.lbEitFiles);
            this.splitContainer1.Panel2.Controls.Add(this.btnFetchFtp);
            this.splitContainer1.Panel2.Controls.Add(this.btnFetch);
            this.splitContainer1.Panel2MinSize = 194;
            this.splitContainer1.Size = new System.Drawing.Size(640, 480);
            this.splitContainer1.SplitterDistance = 437;
            this.splitContainer1.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.eITFormatDisplayBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 125);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemImageComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(437, 355);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ToolTipController = this.toolTipController1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // eITFormatDisplayBindingSource
            // 
            this.eITFormatDisplayBindingSource.DataSource = typeof(Deveknife.Blades.Overview.EITFormatDisplay);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIcon,
            this.colTag,
            this.colFilename,
            this.colAudio,
            this.colBeschreibung,
            this.colDauer,
            this.colEventName,
            this.colFileSize,
            this.colEventPicture,
            this.colEventLanguage,
            this.colEventType,
            this.colVPid,
            this.colVerschluesselung,
            this.colZeit,
            this.colColor,
            this.colNote,
            this.colTest,
            this.colIsFixed,
            this.colDisplayType});
            this.gridView1.CustomizationFormBounds = new System.Drawing.Rectangle(265, 407, 216, 185);
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.GridView1RowStyle);
            this.gridView1.CustomFilterDialog += new DevExpress.XtraGrid.Views.Grid.CustomFilterDialogEventHandler(this.GridView1CustomFilterDialog);
            // 
            // colIcon
            // 
            this.colIcon.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colIcon.FieldName = "Icon";
            this.colIcon.Name = "colIcon";
            this.colIcon.OptionsColumn.AllowEdit = false;
            this.colIcon.OptionsColumn.AllowSize = false;
            this.colIcon.OptionsColumn.FixedWidth = true;
            this.colIcon.OptionsColumn.ReadOnly = true;
            this.colIcon.Width = 32;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.ReadOnly = true;
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // colTag
            // 
            this.colTag.FieldName = "Tag";
            this.colTag.Name = "colTag";
            this.colTag.Width = 34;
            // 
            // colFilename
            // 
            this.colFilename.FieldName = "Filename";
            this.colFilename.Name = "colFilename";
            this.colFilename.Visible = true;
            this.colFilename.VisibleIndex = 1;
            this.colFilename.Width = 40;
            // 
            // colAudio
            // 
            this.colAudio.FieldName = "Audio";
            this.colAudio.Name = "colAudio";
            // 
            // colBeschreibung
            // 
            this.colBeschreibung.FieldName = "Beschreibung";
            this.colBeschreibung.Name = "colBeschreibung";
            this.colBeschreibung.Visible = true;
            this.colBeschreibung.VisibleIndex = 5;
            this.colBeschreibung.Width = 59;
            // 
            // colDauer
            // 
            this.colDauer.FieldName = "Dauer";
            this.colDauer.Name = "colDauer";
            this.colDauer.OptionsColumn.AllowSize = false;
            this.colDauer.OptionsColumn.FixedWidth = true;
            this.colDauer.Visible = true;
            this.colDauer.VisibleIndex = 6;
            this.colDauer.Width = 54;
            // 
            // colEventName
            // 
            this.colEventName.FieldName = "EventName";
            this.colEventName.Name = "colEventName";
            this.colEventName.Visible = true;
            this.colEventName.VisibleIndex = 3;
            this.colEventName.Width = 29;
            // 
            // colFileSize
            // 
            this.colFileSize.FieldName = "FileSize";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.Width = 55;
            // 
            // colEventPicture
            // 
            this.colEventPicture.FieldName = "EventPicture";
            this.colEventPicture.Name = "colEventPicture";
            // 
            // colEventLanguage
            // 
            this.colEventLanguage.FieldName = "EventLanguage";
            this.colEventLanguage.Name = "colEventLanguage";
            // 
            // colEventType
            // 
            this.colEventType.FieldName = "EventType";
            this.colEventType.Name = "colEventType";
            this.colEventType.Visible = true;
            this.colEventType.VisibleIndex = 4;
            this.colEventType.Width = 29;
            // 
            // colVPid
            // 
            this.colVPid.FieldName = "VPid";
            this.colVPid.Name = "colVPid";
            // 
            // colVerschluesselung
            // 
            this.colVerschluesselung.FieldName = "Verschluesselung";
            this.colVerschluesselung.Name = "colVerschluesselung";
            // 
            // colZeit
            // 
            this.colZeit.FieldName = "Zeit";
            this.colZeit.Name = "colZeit";
            this.colZeit.OptionsColumn.AllowSize = false;
            this.colZeit.OptionsColumn.FixedWidth = true;
            this.colZeit.Visible = true;
            this.colZeit.VisibleIndex = 2;
            this.colZeit.Width = 120;
            // 
            // colColor
            // 
            this.colColor.Caption = "Color";
            this.colColor.FieldName = "Color";
            this.colColor.Name = "colColor";
            // 
            // colNote
            // 
            this.colNote.FieldName = "Note";
            this.colNote.Name = "colNote";
            this.colNote.OptionsColumn.FixedWidth = true;
            this.colNote.Visible = true;
            this.colNote.VisibleIndex = 7;
            this.colNote.Width = 64;
            // 
            // colTest
            // 
            this.colTest.FieldName = "Test";
            this.colTest.Name = "colTest";
            // 
            // colIsFixed
            // 
            this.colIsFixed.Caption = "Fixed";
            this.colIsFixed.FieldName = "IsFixed";
            this.colIsFixed.Name = "colIsFixed";
            this.colIsFixed.OptionsColumn.AllowSize = false;
            this.colIsFixed.OptionsColumn.FixedWidth = true;
            this.colIsFixed.Width = 36;
            // 
            // colDisplayType
            // 
            this.colDisplayType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colDisplayType.FieldName = "DisplayState";
            this.colDisplayType.Name = "colDisplayType";
            this.colDisplayType.OptionsColumn.FixedWidth = true;
            this.colDisplayType.Visible = true;
            this.colDisplayType.VisibleIndex = 0;
            this.colDisplayType.Width = 24;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.LargeImages = this.imageList1;
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imageList1;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pbProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(437, 125);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbOverviewLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(437, 102);
            this.panel2.TabIndex = 2;
            // 
            // tbOverviewLog
            // 
            this.tbOverviewLog.ContextMenuStrip = this.cnt;
            this.tbOverviewLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOverviewLog.Location = new System.Drawing.Point(0, 0);
            this.tbOverviewLog.Multiline = true;
            this.tbOverviewLog.Name = "tbOverviewLog";
            this.tbOverviewLog.ReadOnly = true;
            this.tbOverviewLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOverviewLog.Size = new System.Drawing.Size(437, 102);
            this.tbOverviewLog.TabIndex = 0;
            this.tbOverviewLog.TextChanged += new System.EventHandler(this.TbOverviewLogTextChanged);
            // 
            // cnt
            // 
            this.cnt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCntClearText});
            this.cnt.Name = "cnt";
            this.cnt.Size = new System.Drawing.Size(127, 26);
            // 
            // mnuCntClearText
            // 
            this.mnuCntClearText.Name = "mnuCntClearText";
            this.mnuCntClearText.Size = new System.Drawing.Size(126, 22);
            this.mnuCntClearText.Text = "Clear Text";
            this.mnuCntClearText.Click += new System.EventHandler(this.MnuCountClearTextClick);
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbProgress.Location = new System.Drawing.Point(0, 102);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(437, 23);
            this.pbProgress.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFix);
            this.groupBox2.Controls.Add(this.btnCleanupDone);
            this.groupBox2.Location = new System.Drawing.Point(2, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 53);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // btnFix
            // 
            this.btnFix.BackColor = System.Drawing.Color.GreenYellow;
            this.btnFix.Location = new System.Drawing.Point(72, 10);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(55, 31);
            this.btnFix.TabIndex = 0;
            this.btnFix.Text = "Fix";
            this.btnFix.UseVisualStyleBackColor = false;
            this.btnFix.Click += new System.EventHandler(this.BtnFixClick);
            // 
            // btnCleanupDone
            // 
            this.btnCleanupDone.BackColor = System.Drawing.SystemColors.Info;
            this.btnCleanupDone.Location = new System.Drawing.Point(133, 10);
            this.btnCleanupDone.Name = "btnCleanupDone";
            this.btnCleanupDone.Size = new System.Drawing.Size(55, 31);
            this.btnCleanupDone.TabIndex = 0;
            this.btnCleanupDone.Text = "Cleanup";
            this.btnCleanupDone.UseVisualStyleBackColor = false;
            this.btnCleanupDone.Click += new System.EventHandler(this.BtnCleanupDoneClick);
            // 
            // btnTestFilter
            // 
            this.btnTestFilter.Location = new System.Drawing.Point(9, 14);
            this.btnTestFilter.Name = "btnTestFilter";
            this.btnTestFilter.Size = new System.Drawing.Size(66, 36);
            this.btnTestFilter.TabIndex = 4;
            this.btnTestFilter.Text = "Test Filter";
            this.btnTestFilter.UseVisualStyleBackColor = true;
            this.btnTestFilter.Click += new System.EventHandler(this.BtnTestFilterClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbSoundex);
            this.groupBox1.Controls.Add(this.btnComparerClear);
            this.groupBox1.Controls.Add(this.btnComparerSet);
            this.groupBox1.Controls.Add(this.btnFetchFolder);
            this.groupBox1.Location = new System.Drawing.Point(3, 186);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 133);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Compare and Browse";
            // 
            // cbSoundex
            // 
            this.cbSoundex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSoundex.AutoSize = true;
            this.cbSoundex.Location = new System.Drawing.Point(6, 65);
            this.cbSoundex.Name = "cbSoundex";
            this.cbSoundex.Size = new System.Drawing.Size(68, 17);
            this.cbSoundex.TabIndex = 3;
            this.cbSoundex.Text = "Soundex";
            this.cbSoundex.UseVisualStyleBackColor = true;
            // 
            // btnComparerClear
            // 
            this.btnComparerClear.Location = new System.Drawing.Point(18, 20);
            this.btnComparerClear.Name = "btnComparerClear";
            this.btnComparerClear.Size = new System.Drawing.Size(65, 43);
            this.btnComparerClear.TabIndex = 2;
            this.btnComparerClear.Text = "Clear Comparers";
            this.btnComparerClear.UseVisualStyleBackColor = true;
            this.btnComparerClear.Click += new System.EventHandler(this.BtnComparerClearClick);
            // 
            // btnComparerSet
            // 
            this.btnComparerSet.Location = new System.Drawing.Point(118, 20);
            this.btnComparerSet.Name = "btnComparerSet";
            this.btnComparerSet.Size = new System.Drawing.Size(61, 43);
            this.btnComparerSet.TabIndex = 2;
            this.btnComparerSet.Text = "Set as Comparer";
            this.btnComparerSet.UseVisualStyleBackColor = true;
            this.btnComparerSet.Click += new System.EventHandler(this.BtnComparerSetClick);
            // 
            // btnFetchFolder
            // 
            this.btnFetchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetchFolder.BackColor = System.Drawing.Color.SpringGreen;
            this.btnFetchFolder.Location = new System.Drawing.Point(6, 86);
            this.btnFetchFolder.Name = "btnFetchFolder";
            this.btnFetchFolder.Size = new System.Drawing.Size(171, 38);
            this.btnFetchFolder.TabIndex = 1;
            this.btnFetchFolder.Text = "Fetch\r\nFolder";
            this.btnFetchFolder.UseVisualStyleBackColor = false;
            this.btnFetchFolder.Click += new System.EventHandler(this.BtnFetchFolderClick);
            // 
            // btnSaveLayout
            // 
            this.btnSaveLayout.Location = new System.Drawing.Point(85, 12);
            this.btnSaveLayout.Name = "btnSaveLayout";
            this.btnSaveLayout.Size = new System.Drawing.Size(98, 38);
            this.btnSaveLayout.TabIndex = 2;
            this.btnSaveLayout.Text = "Save Layout";
            this.btnSaveLayout.UseVisualStyleBackColor = true;
            this.btnSaveLayout.Click += new System.EventHandler(this.BtnSaveLayoutClick);
            // 
            // btnFetchFtp
            // 
            this.btnFetchFtp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetchFtp.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnFetchFtp.Location = new System.Drawing.Point(12, 384);
            this.btnFetchFtp.Name = "btnFetchFtp";
            this.btnFetchFtp.Size = new System.Drawing.Size(171, 38);
            this.btnFetchFtp.TabIndex = 1;
            this.btnFetchFtp.Text = "Fetch Ftp";
            this.btnFetchFtp.UseVisualStyleBackColor = false;
            this.btnFetchFtp.Click += new System.EventHandler(this.BtnFetchFtpClick);
            // 
            // toolTipController1
            // 
            this.toolTipController1.AutoPopDelay = 35000;
            // 
            // VideoOverviewUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "VideoOverviewUI";
            this.Size = new System.Drawing.Size(640, 480);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eITFormatDisplayBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.cnt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbEitFiles;
        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colTag;
        private DevExpress.XtraGrid.Columns.GridColumn colFilename;
        private DevExpress.XtraGrid.Columns.GridColumn colAudio;
        private DevExpress.XtraGrid.Columns.GridColumn colBeschreibung;
        private DevExpress.XtraGrid.Columns.GridColumn colDauer;
        private DevExpress.XtraGrid.Columns.GridColumn colEventName;
        private DevExpress.XtraGrid.Columns.GridColumn colFileSize;
        private DevExpress.XtraGrid.Columns.GridColumn colEventPicture;
        private DevExpress.XtraGrid.Columns.GridColumn colEventLanguage;
        private DevExpress.XtraGrid.Columns.GridColumn colEventType;
        private DevExpress.XtraGrid.Columns.GridColumn colVPid;
        private DevExpress.XtraGrid.Columns.GridColumn colVerschluesselung;
        private DevExpress.XtraGrid.Columns.GridColumn colZeit;
        private System.Windows.Forms.Button btnSaveLayout;
        private System.Windows.Forms.Button btnFetchFtp;
        private System.Windows.Forms.BindingSource eITFormatDisplayBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colColor;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ProgressBarControl pbProgress;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbOverviewLog;
        private System.Windows.Forms.Button btnFetchFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnComparerClear;
        private System.Windows.Forms.Button btnComparerSet;
        private DevExpress.XtraGrid.Columns.GridColumn colNote;
        private DevExpress.XtraGrid.Columns.GridColumn colIcon;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private System.Windows.Forms.Button btnTestFilter;
        private DevExpress.XtraGrid.Columns.GridColumn colTest;
        private System.Windows.Forms.CheckBox cbSoundex;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCleanupDone;
        private System.Windows.Forms.ContextMenuStrip cnt;
        private System.Windows.Forms.ToolStripMenuItem mnuCntClearText;
        private System.Windows.Forms.Button btnFix;
        private DevExpress.XtraGrid.Columns.GridColumn colIsFixed;
        private DevExpress.XtraGrid.Columns.GridColumn colDisplayType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.Utils.ToolTipController toolTipController1;
    }
}
