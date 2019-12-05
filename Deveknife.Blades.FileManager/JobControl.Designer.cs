namespace Deveknife.Blades.FileManager
{
    partial class JobControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JobControl));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.trlJobs = new DevExpress.XtraTreeList.TreeList();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.winExplorerView1 = new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnImageIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clbJobs = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.jobClosureBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.searchControl1 = new DevExpress.XtraEditors.SearchControl();
            this.btnExecuteJobs = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trlJobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.winExplorerView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbJobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobClosureBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.trlJobs);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.clbJobs);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1358, 377);
            this.splitContainerControl1.SplitterPosition = 227;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // trlJobs
            // 
            this.trlJobs.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colName});
            this.trlJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trlJobs.KeyFieldName = "Key";
            this.trlJobs.Location = new System.Drawing.Point(0, 0);
            this.trlJobs.Name = "trlJobs";
            this.trlJobs.OptionsBehavior.Editable = false;
            this.trlJobs.ParentFieldName = "ParentJob";
            this.trlJobs.SelectImageList = this.imageList1;
            this.trlJobs.Size = new System.Drawing.Size(227, 377);
            this.trlJobs.StateImageList = this.imageList1;
            this.trlJobs.TabIndex = 0;
            this.trlJobs.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.TreeListJobsGetStateImage);
            this.trlJobs.CustomDrawNodeImages += new DevExpress.XtraTreeList.CustomDrawNodeImagesEventHandler(this.TreeListJobsCustomDrawNodeImages);
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 145;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 145;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Globe.png");
            this.imageList1.Images.SetKeyName(1, "cam_mount.png");
            this.imageList1.Images.SetKeyName(2, "joystick.png");
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.winExplorerView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(513, 377);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.winExplorerView1,
            this.cardView1});
            // 
            // winExplorerView1
            // 
            this.winExplorerView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName,
            this.gridColumnImage});
            this.winExplorerView1.ColumnSet.DescriptionColumn = this.gridColumnName;
            this.winExplorerView1.ColumnSet.MediumImageIndexColumn = this.gridColumnImage;
            this.winExplorerView1.ColumnSet.SmallImageIndexColumn = this.gridColumnImage;
            this.winExplorerView1.ColumnSet.TextColumn = this.gridColumnName;
            this.winExplorerView1.GridControl = this.gridControl1;
            this.winExplorerView1.Images = this.imageList1;
            this.winExplorerView1.MediumImages = this.imageList1;
            this.winExplorerView1.Name = "winExplorerView1";
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 0;
            // 
            // gridColumnImage
            // 
            this.gridColumnImage.Caption = "gridColumn1";
            this.gridColumnImage.FieldName = "ImageIndex";
            this.gridColumnImage.Name = "gridColumnImage";
            this.gridColumnImage.Visible = true;
            this.gridColumnImage.VisibleIndex = 1;
            // 
            // cardView1
            // 
            this.cardView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumnImageIndex});
            this.cardView1.FocusedCardTopFieldIndex = 0;
            this.cardView1.GridControl = this.gridControl1;
            this.cardView1.Name = "cardView1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Name";
            this.gridColumn2.FieldName = "Name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumnImageIndex
            // 
            this.gridColumnImageIndex.Caption = "gridColumn1";
            this.gridColumnImageIndex.FieldName = "ImageIndex";
            this.gridColumnImageIndex.Name = "gridColumnImageIndex";
            this.gridColumnImageIndex.Visible = true;
            this.gridColumnImageIndex.VisibleIndex = 1;
            // 
            // clbJobs
            // 
            this.clbJobs.CheckMember = "Enabled";
            this.clbJobs.DataSource = this.jobClosureBindingSource;
            this.clbJobs.DisplayMember = "Parameters";
            this.clbJobs.Dock = System.Windows.Forms.DockStyle.Right;
            this.clbJobs.Location = new System.Drawing.Point(513, 0);
            this.clbJobs.Name = "clbJobs";
            this.clbJobs.Size = new System.Drawing.Size(613, 377);
            this.clbJobs.TabIndex = 1;
            this.clbJobs.ValueMember = "Job";
            // 
            // jobClosureBindingSource
            // 
            this.jobClosureBindingSource.DataSource = typeof(Deveknife.Blades.FileManager.Jobs.DeferredJobResult.JobClosure);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 92);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1362, 381);
            this.panelControl1.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.comboBox1);
            this.groupControl1.Controls.Add(this.searchControl1);
            this.groupControl1.Controls.Add(this.btnExecuteJobs);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1362, 92);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "groupControl1";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(418, 46);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(172, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.ComboBox1SelectionChangeCommitted);
            // 
            // searchControl1
            // 
            this.searchControl1.Location = new System.Drawing.Point(73, 46);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton(),
            new DevExpress.XtraEditors.Repository.SearchButton()});
            this.searchControl1.Size = new System.Drawing.Size(339, 20);
            this.searchControl1.TabIndex = 1;
            // 
            // btnExecuteJobs
            // 
            this.btnExecuteJobs.Location = new System.Drawing.Point(1224, 38);
            this.btnExecuteJobs.Name = "btnExecuteJobs";
            this.btnExecuteJobs.Size = new System.Drawing.Size(133, 29);
            this.btnExecuteJobs.TabIndex = 0;
            this.btnExecuteJobs.Text = "simpleButton1";
            this.btnExecuteJobs.Click += new System.EventHandler(this.BtnExecuteJobsClick);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(596, 41);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(133, 29);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.SimpleButton1Click);
            // 
            // JobControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "JobControl";
            this.Size = new System.Drawing.Size(1362, 473);
            this.Load += new System.EventHandler(this.JobControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trlJobs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.winExplorerView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbJobs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobClosureBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList trlJobs;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView winExplorerView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevExpress.XtraEditors.CheckedListBoxControl clbJobs;
        private System.Windows.Forms.BindingSource jobClosureBindingSource;
        private DevExpress.XtraEditors.SimpleButton btnExecuteJobs;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnImage;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnImageIndex;
    }
}
