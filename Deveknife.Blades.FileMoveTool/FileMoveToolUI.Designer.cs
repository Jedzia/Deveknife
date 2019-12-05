namespace Deveknife.Blades.FileMoveTool
{
    using Deveknife.Blades.FileMoveTool.UI;

    partial class FileMoveToolUI
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
            this.btnCheck = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeListExplorer1 = new Deveknife.Blades.FileMoveTool.UI.TreeListExplorer();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.buttonEditFolderB = new Deveknife.Blades.FileMoveTool.UI.FolderButtonEdit();
            this.buttonEditFolderA = new Deveknife.Blades.FileMoveTool.UI.FolderButtonEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbFileMoveToolLog = new System.Windows.Forms.TextBox();
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
            this.eITFormatDisplayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFolderB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFolderA.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.cnt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eITFormatDisplayBindingSource)).BeginInit();
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
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.Location = new System.Drawing.Point(12, 613);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(150, 38);
            this.btnCheck.TabIndex = 1;
            this.btnCheck.Text = "Check";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.BtnCheckClick);
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
            this.splitContainer1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1MinSize = 850;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.btnTestFilter);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveLayout);
            this.splitContainer1.Panel2.Controls.Add(this.lbEitFiles);
            this.splitContainer1.Panel2.Controls.Add(this.btnFetchFtp);
            this.splitContainer1.Panel2.Controls.Add(this.btnCheck);
            this.splitContainer1.Panel2MinSize = 194;
            this.splitContainer1.Size = new System.Drawing.Size(1068, 654);
            this.splitContainer1.SplitterDistance = 850;
            this.splitContainer1.TabIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(850, 387);
            this.panelControl1.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeListExplorer1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(846, 383);
            this.splitContainerControl1.SplitterPosition = 225;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeListExplorer1
            // 
            this.treeListExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListExplorer1.Location = new System.Drawing.Point(0, 0);
            this.treeListExplorer1.Name = "treeListExplorer1";
            this.treeListExplorer1.Size = new System.Drawing.Size(225, 383);
            this.treeListExplorer1.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.groupControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(616, 230);
            this.panelControl2.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.buttonEditFolderB);
            this.groupControl1.Controls.Add(this.buttonEditFolderA);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(2, 2);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(612, 128);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // buttonEditFolderB
            // 
            this.buttonEditFolderB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditFolderB.DialogService = null;
            this.buttonEditFolderB.Location = new System.Drawing.Point(3, 49);
            this.buttonEditFolderB.Name = "buttonEditFolderB";
            this.buttonEditFolderB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditFolderB.Size = new System.Drawing.Size(606, 20);
            this.buttonEditFolderB.TabIndex = 1;
            // 
            // buttonEditFolderA
            // 
            this.buttonEditFolderA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditFolderA.DialogService = null;
            this.buttonEditFolderA.Location = new System.Drawing.Point(3, 23);
            this.buttonEditFolderA.Name = "buttonEditFolderA";
            this.buttonEditFolderA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEditFolderA.Size = new System.Drawing.Size(606, 20);
            this.buttonEditFolderA.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pbProgress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 387);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(850, 267);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbFileMoveToolLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 244);
            this.panel2.TabIndex = 2;
            // 
            // tbFileMoveToolLog
            // 
            this.tbFileMoveToolLog.ContextMenuStrip = this.cnt;
            this.tbFileMoveToolLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFileMoveToolLog.Font = new System.Drawing.Font("Inconsolata", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFileMoveToolLog.Location = new System.Drawing.Point(0, 0);
            this.tbFileMoveToolLog.Multiline = true;
            this.tbFileMoveToolLog.Name = "tbFileMoveToolLog";
            this.tbFileMoveToolLog.ReadOnly = true;
            this.tbFileMoveToolLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbFileMoveToolLog.Size = new System.Drawing.Size(850, 244);
            this.tbFileMoveToolLog.TabIndex = 0;
            this.tbFileMoveToolLog.Text = "This is a sample 666 Text";
            this.tbFileMoveToolLog.WordWrap = false;
            this.tbFileMoveToolLog.TextChanged += new System.EventHandler(this.TbFileMoveToolLogTextChanged);
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
            this.mnuCntClearText.Click += new System.EventHandler(this.MenuContextClearTextClick);
            // 
            // pbProgress
            // 
            this.pbProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbProgress.Location = new System.Drawing.Point(0, 244);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(850, 23);
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
            this.btnFetchFtp.Location = new System.Drawing.Point(12, 558);
            this.btnFetchFtp.Name = "btnFetchFtp";
            this.btnFetchFtp.Size = new System.Drawing.Size(150, 38);
            this.btnFetchFtp.TabIndex = 1;
            this.btnFetchFtp.Text = "Test SymLink";
            this.btnFetchFtp.UseVisualStyleBackColor = false;
            this.btnFetchFtp.Click += new System.EventHandler(this.BtnFetchFtpClick);
            // 
            // eITFormatDisplayBindingSource
            // 
            this.eITFormatDisplayBindingSource.DataSource = typeof(Deveknife.Blades.FileMoveTool.FileMoveToolUI.Dummy);
            // 
            // FileMoveToolUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FileMoveToolUI";
            this.Size = new System.Drawing.Size(1068, 654);
            this.Load += new System.EventHandler(this.FileMoveToolUILoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFolderB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEditFolderA.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.cnt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eITFormatDisplayBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbEitFiles;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSaveLayout;
        private System.Windows.Forms.Button btnFetchFtp;
        private System.Windows.Forms.BindingSource eITFormatDisplayBindingSource;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ProgressBarControl pbProgress;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbFileMoveToolLog;
        private System.Windows.Forms.Button btnFetchFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnComparerClear;
        private System.Windows.Forms.Button btnComparerSet;
        private System.Windows.Forms.Button btnTestFilter;
        private System.Windows.Forms.CheckBox cbSoundex;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCleanupDone;
        private System.Windows.Forms.ContextMenuStrip cnt;
        private System.Windows.Forms.ToolStripMenuItem mnuCntClearText;
        private System.Windows.Forms.Button btnFix;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private TreeListExplorer treeListExplorer1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private FolderButtonEdit buttonEditFolderB;
        private FolderButtonEdit buttonEditFolderA;
    }
}
