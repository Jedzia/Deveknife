// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Explorer.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 10:21</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.BladeModuleTemplate.UI
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Security;
    using System.Windows.Forms;

    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Nodes;

    using Guard = Deveknife.Guard;

    /// <summary>
    /// A sample control, a TreeList Explorer.
    /// </summary>
    // ReSharper disable once ClassTooBig
    public partial class TreeListExplorer : XtraUserControl
    {
        private Bar bar;

        /// <summary>
        /// The bar infos of this instance.
        /// </summary>
        [NotNull]
        private ArrayList barInfos = new ArrayList();

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeListExplorer"/> class.
        /// </summary>
        public TreeListExplorer()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            // this.LookAndFeel.StyleChanged += new EventHandler(this.LookAndFeel_StyleChanged);
            this.colSize.StyleName = "Style1";
            this.InitRoot();
            this.xtraTreeListBlending1.Enabled = true;
        }

        /// <summary>
        /// Gets the export control tree list.
        /// </summary>
        /// <value>The export control.</value>
        public TreeList ExportControl
        {
            get
            {
                return this.treeList1;
            }
        }

        /// <summary>
        /// Gets or sets the bar infos of this instance.
        /// </summary>
        /// <value>The bar infos.</value>
        protected ArrayList BarInfos
        {
            get
            {
                return this.barInfos;
            }

            set
            {
                this.barInfos = value;
            }
        }

        /// <summary>
        /// Gets the name of the bar.
        /// </summary>
        /// <value>The name of the bar.</value>
        protected string BarName
        {
            get
            {
                return "Insert a CONTROL NAME here.";
            }
        }

        /// <summary>
        /// Gets the manager of this instance.
        /// </summary>
        /// <value>The manager.</value>
        protected BarManager Manager
        {
            get
            {
                return this.barManager1;
            }
        }

        private Bar Bar
        {
            get
            {
                foreach(Bar barItem in this.Manager.Bars)
                {
                    if(barItem.BarName == this.BarName)
                    {
                        return barItem;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Called when the control hides.
        /// </summary>
        protected virtual void DoHide()
        {
        }

        /// <summary>
        /// Called when the control is showing.
        /// </summary>
        protected virtual void DoShow()
        {
        }

        /// <summary>
        /// Gets the bar item pushed.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns><c>true</c> if the item with the specified index was pushed or checked, <c>false</c> otherwise.</returns>
        protected bool GetBarItemPushed(int index)
        {
            this.InitBars();
            if(this.BarInfos.Count <= index)
            {
                return false;
            }

            return ((BarInfo)this.BarInfos[index]).Pushed;
        }

        /// <summary>
        /// Initializes the menu bar.
        /// </summary>
        protected void InitBarInfo()
        {
            this.BarInfos.Add(
                new BarInfo("Show Logical Drivers", this.ShowLogicalDriversClick, this.imageList3.Images[0], true, false, false));
            this.BarInfos.Add(new BarInfo("Assign Check", this.AssignCheckClick, this.imageList3.Images[1], true, true, false));
            this.BarInfos.Add(
                new BarInfo("Even And Odd Style Enabled", this.EvenOddStyleClick, this.imageList3.Images[2], true, true, false));
            this.BarInfos.Add(new BarInfo("Show Footer", this.ShowFooterClick, this.imageList3.Images[3], true, false, false));
            this.BarInfos.Add(new BarInfo("Show Preview", this.ShowPreviewClick, this.imageList3.Images[6], true, false, false));
            this.BarInfos.Add(new BarInfo("Show Grid", this.ShowGridClick, this.imageList3.Images[7], true, false, false));
            this.BarInfos.Add(new BarInfo("Alpha Blending", this.AlphaBlendingClick, this.imageList3.Images[8], false, false, false));
            this.BarInfos.Add(new BarInfo("Checked Items List", this.ListClick, this.imageList3.Images[4], false, false, true));
        }

        /// <summary>
        /// Handles the <see cref="E:VisibleChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnVisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible)
            {
                this.ShowBar();
                this.DoShow();
            }
            else
            {
                this.DoHide();
                this.HideBar();
            }
        }

        /// <summary>
        /// Sets the bar item enabled.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        protected void SetBarItemEnabled(int index, bool enabled)
        {
            this.InitBars();
            if(this.BarInfos.Count > index)
            {
                ((BarInfo)this.BarInfos[index]).Enabled = enabled;
            }
        }

        private void AlphaBlendingClick(object sender, ItemClickEventArgs e)
        {
            this.xtraTreeListBlending1.ShowDialog();
        }

        private void AssignCheckClick(object sender, ItemClickEventArgs e)
        {
            if(this.GetBarItemPushed(1))
            {
                this.bar1.ItemLinks[1].Visible = true;
                this.treeList1.StateImageList = this.imageList2;
            }
            else
            {
                this.bar1.ItemLinks[1].Visible = false;
                this.treeList1.StateImageList = null;
            }

            this.SetBarItemEnabled(7, this.GetBarItemPushed(1));
        }

        private void CheckedItemsInfo([CanBeNull] TreeListNode pnode, ref string s)
        {
            var nodes = pnode != null ? pnode.Nodes : this.treeList1.Nodes;
            foreach(TreeListNode node in nodes)
            {
                var nodeChecked = (CheckState)node.GetValue("Check");
                if(nodeChecked != CheckState.Unchecked)
                {
                    if((nodeChecked == CheckState.Checked) && (node.GetDisplayText("Type") == "File"))
                    {
                        s += node.GetDisplayText("FullName") + "\r\n";
                    }

                    this.CheckedItemsInfo(node, ref s);
                }
            }
        }

        private string CheckedItemsInfoMain()
        {
            string s = string.Empty;
            this.CheckedItemsInfo(null, ref s);
            if(s == string.Empty)
            {
                s = "No checked files...";
            }

            return s;
        }

        private void CreateBar()
        {
            this.bar = new Bar(this.Manager) { BarName = this.Text = this.BarName, DockStyle = BarDockStyle.Top };

            foreach(BarInfo info in this.BarInfos)
            {
                var item = info.CreateItem(this.Manager);
                this.bar.AddItem(item).BeginGroup = info.BeginItemGroup;
            }
        }

        private void EvenOddStyleClick(object sender, ItemClickEventArgs e)
        {
            this.treeList1.OptionsView.EnableAppearanceEvenRow = this.GetBarItemPushed(2);
        }

        /// <summary>
        /// Determines whether the specified path has files present.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns><c>true</c> if the specified path has files; otherwise, <c>false</c>.</returns>
        private bool HasFiles(string path)
        {
            string[] root;
            try
            {
                root = Directory.GetFiles(path);
            }
            catch(IOException)
            {
                return false;
            }
            catch(UnauthorizedAccessException)
            {
                return false;
            }

            if(root.Length > 0)
            {
                return true;
            }

            try
            {
                root = Directory.GetDirectories(path);
                if(root.Length > 0)
                {
                    return true;
                }
            }
            catch(UnauthorizedAccessException)
            {
            }
            catch(IOException)
            {
                // TODO: Handle the IOException 
            }

            return false;
        }

        /// <summary>
        /// Hides the bar.
        /// </summary>
        private void HideBar()
        {
            if((this.BarName != string.Empty) && (this.Manager != null))
            {
                this.Bar.Visible = false;
            }
        }

        /// <summary>
        /// Initializes the bars.
        /// </summary>
        private void InitBars()
        {
            if(this.BarInfos.Count == 0)
            {
                this.InitBarInfo();
            }

            if((this.Manager != null) && (this.Bar == null))
            {
                this.CreateBar();
            }
        }

        /// <summary>
        /// Initializes the drives.
        /// </summary>
        private void InitDrives()
        {
            this.treeList1.BeginUnboundLoad();
            try
            {
                string[] root = Directory.GetLogicalDrives();

                foreach(string s in root)
                {
                    var node = this.treeList1.AppendNode(new object[] { s, s, "Logical Driver", null, null, CheckState.Unchecked }, null);
                    node.HasChildren = true;
                    node.Tag = true;
                }
            }
            catch(IOException)
            {
            }
            catch(UnauthorizedAccessException)
            {
            }

            this.treeList1.EndUnboundLoad();
        }

        /// <summary>
        /// Initializes the TreeListNode files recursively from a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="parentNode">The root node to start from. Set to <see langword="null"/> if it is the tree's root node.</param>
        /// <param name="check">The check state of new nodes.</param>
        private void InitFiles([NotNull] string path, [CanBeNull] TreeListNode parentNode, CheckState check)
        {
            Guard.NotNullOrEmpty(() => path, path);
            try
            {
                string[] root = Directory.GetFiles(path);
                foreach(string s in root)
                {
                    var fi = new FileInfo(s);
                    var node = this.treeList1.AppendNode(new object[] { s, fi.Name, "File", fi.Length, fi.Attributes, check }, parentNode);
                    node.HasChildren = false;
                }
            }
            catch(IOException)
            {
            }
            catch(UnauthorizedAccessException)
            {
                // TODO: Handle the UnauthorizedAccessException 
            }
            catch(SecurityException)
            {
                // TODO: Handle the SecurityException 
            }
        }

        /// <summary>
        /// Initializes the TreeListNode files recursively from a path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="parentNode">The root node to start from. Set to <see langword="null"/> if it is the tree's root node.</param>
        /// <param name="check">The check state of new nodes.</param>
        private void InitFolders([NotNull] string path, [CanBeNull] TreeListNode parentNode, CheckState check)
        {
            Guard.NotNullOrEmpty(() => path, path);
            this.treeList1.BeginUnboundLoad();
            try
            {
                string[] root = Directory.GetDirectories(path);
                foreach(string s in root)
                {
                    try
                    {
                        var di = new DirectoryInfo(s);
                        var node = this.treeList1.AppendNode(new object[] { s, di.Name, "Folder", null, di.Attributes, check }, parentNode);
                        node.HasChildren = this.HasFiles(s);
                        if(node.HasChildren)
                        {
                            node.Tag = true;
                        }
                    }
                    catch(SecurityException)
                    {
                        // TODO: Handle the SecurityException 
                    }
                }
            }
            catch(UnauthorizedAccessException)
            {
                // TODO: Handle the UnauthorizedAccessException 
            }
            catch(IOException)
            {
                // TODO: Handle the IOException 
            }

            this.InitFiles(path, parentNode, check);
            this.treeList1.EndUnboundLoad();
        }

        private void InitRoot()
        {
            if(!this.GetBarItemPushed(0))
            {
                try
                {
                    this.InitFolders(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), null, CheckState.Unchecked);
                }
                catch(UnauthorizedAccessException)
                {
                }
                catch(PathTooLongException)
                {
                    // TODO: Handle the PathTooLongException 
                }
            }
            else
            {
                this.InitDrives();
            }
        }

        /// <summary>
        /// Lists the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception">The specified control is a top-level control, or a circular control reference would result if this control were added to the control collection.</exception>
        private void ListClick(object sender, ItemClickEventArgs e)
        {
            var f = new XtraForm();
            var tb = new TextBox
                     {
                         Multiline = true,
                         Dock = DockStyle.Fill,
                         ScrollBars = ScrollBars.Vertical,
                         Text = this.CheckedItemsInfoMain(),
                         SelectionLength = 0
                     };

            f.Controls.Add(tb);
            f.Text = "CheckedItems Info";
            f.StartPosition = FormStartPosition.Manual;
            f.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            f.Location = ControlUtils.CalcLocation(Control.MousePosition, Control.MousePosition, f.Size);
            f.ShowDialog();
        }

        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for(int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i]["Check"] = check;
                this.SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        private void SetCheckedNode(TreeListNode node)
        {
            var check = (CheckState)node.GetValue("Check");
            if((check == CheckState.Indeterminate) || (check == CheckState.Unchecked))
            {
                check = CheckState.Checked;
            }
            else
            {
                check = CheckState.Unchecked;
            }

            this.treeList1.FocusedNode = node;
            this.treeList1.BeginUpdate();
            node["Check"] = check;
            this.StatusBarDisplayText(this.treeList1.FocusedNode);
            this.SetCheckedChildNodes(node, check);
            this.SetCheckedParentNodes(node, check);
            this.treeList1.EndUpdate();
        }

        // ReSharper disable once FlagArgument
        private void SetCheckedParentNodes([NotNull] TreeListNode node, CheckState check)
        {
            if(node.ParentNode == null)
            {
                return;
            }

            bool b = false;
            for(var i = 0; i < node.ParentNode.Nodes.Count; i++)
            {
                if(!check.Equals(node.ParentNode.Nodes[i]["Check"]))
                {
                    b = true;
                    break;
                }
            }

            node.ParentNode["Check"] = b ? CheckState.Indeterminate : check;
            this.SetCheckedParentNodes(node.ParentNode, check);
        }

        private void ShowBar()
        {
            if((this.BarName != string.Empty) && (this.Manager != null))
            {
                this.InitBars();
                this.Bar.Visible = true;
            }
        }

        private void ShowFooterClick(object sender, ItemClickEventArgs e)
        {
            this.treeList1.OptionsView.ShowSummaryFooter = this.GetBarItemPushed(3);
        }

        private void ShowGridClick(object sender, ItemClickEventArgs e)
        {
            this.treeList1.OptionsView.ShowHorzLines = this.treeList1.OptionsView.ShowVertLines = this.GetBarItemPushed(5);
        }

        private void ShowLogicalDriversClick(object sender, ItemClickEventArgs e)
        {
            this.treeList1.ClearNodes();
            this.InitRoot();
        }

        private void ShowPreviewClick(object sender, ItemClickEventArgs e)
        {
            this.treeList1.OptionsView.ShowPreview = this.GetBarItemPushed(4);
        }

        private void StatusBarDisplayText([CanBeNull] TreeListNode node)
        {
            if(node != null)
            {
                this.barStaticItem1.Caption = node.GetDisplayText("FullName");
                this.barStaticItem2.Caption = node.GetDisplayText("Check");
            }
            else
            {
                this.barStaticItem1.Caption = this.barStaticItem2.Caption = string.Empty;
            }
        }

        /// <summary>
        /// Handles the BeforeExpand event of the treeList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BeforeExpandEventArgs"/> instance containing the event data.</param>
        private void TreeList1BeforeExpand(object sender, BeforeExpandEventArgs e)
        {
            if(e.Node.Tag != null)
            {
                var currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                this.InitFolders(e.Node.GetDisplayText("FullName"), e.Node, (CheckState)e.Node.GetValue("Check"));
                e.Node.Tag = null;
                Cursor.Current = currentCursor;
            }
        }

        /// <summary>
        /// Handles the CompareNodeValues event of the treeList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CompareNodeValuesEventArgs"/> instance containing the event data.</param>
        private void TreeList1CompareNodeValues(object sender, CompareNodeValuesEventArgs e)
        {
            string type1 = e.Node1.GetDisplayText("Type");
            string type2 = e.Node2.GetDisplayText("Type");
            if(type1 != type2)
            {
                if(type1 == "Folder")
                {
                    e.Result = (e.SortOrder == SortOrder.Ascending) ? -1 : 1;
                }
                else
                {
                    e.Result = (e.SortOrder == SortOrder.Ascending) ? 1 : -1;
                }
            }
        }

        /// <summary>
        /// Handles the FocusedNodeChanged event of the treeList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FocusedNodeChangedEventArgs"/> instance containing the event data.</param>
        private void TreeList1FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            this.StatusBarDisplayText(e.Node);
        }

        /// <summary>
        /// Handles the GetPreviewText event of the treeList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GetPreviewTextEventArgs"/> instance containing the event data.</param>
        private void TreeList1GetPreviewText(object sender, GetPreviewTextEventArgs e)
        {
            e.PreviewText = e.Node.GetDisplayText("FullName");
        }

        /// <summary>
        /// Handles the GetSelectImage event of the treeList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GetSelectImageEventArgs"/> instance containing the event data.</param>
        private void TreeList1GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            if(e.Node.GetDisplayText("Type") == "Folder")
            {
                e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
            }
            else if(e.Node.GetDisplayText("Type") == "File")
            {
                e.NodeImageIndex = 2;
            }
            else
            {
                e.NodeImageIndex = 3;
            }
        }

        /// <summary>
        /// Handles the GetStateImage event of the treeList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GetStateImageEventArgs"/> instance containing the event data.</param>
        private void TreeList1GetStateImage(object sender, GetStateImageEventArgs e)
        {
            var check = (CheckState)e.Node.GetValue("Check");
            if(check == CheckState.Unchecked)
            {
                e.NodeImageIndex = 0;
            }
            else if(check == CheckState.Checked)
            {
                e.NodeImageIndex = 1;
            }
            else
            {
                e.NodeImageIndex = 2;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the treeList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void TreeList1KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Space)
            {
                this.SetCheckedNode(this.treeList1.FocusedNode);
            }

            if(e.KeyData != Keys.Enter)
            {
                return;
            }

            if(this.treeList1.FocusedNode.GetDisplayText("Type") == "File")
            {
                try
                {
                    var process = new Process
                                  {
                                      StartInfo =
                                      {
                                          FileName = this.treeList1.FocusedNode.GetDisplayText("FullName"),
                                          Verb = "Open",
                                          WindowStyle = ProcessWindowStyle.Normal
                                      }
                                  };
                    process.Start();
                }
                catch(InvalidOperationException)
                {
                }
                catch(Win32Exception)
                {
                    // TODO: Handle the Win32Exception 
                }
            }
            else if(this.treeList1.FocusedNode.HasChildren)
            {
                this.treeList1.FocusedNode.Expanded = !this.treeList1.FocusedNode.Expanded;
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the treeList1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void TreeList1MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                var hitInfo = this.treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if(hitInfo.HitInfoType == HitInfoType.StateImage)
                {
                    this.SetCheckedNode(hitInfo.Node);
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the TreeListExplorer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        [UsedImplicitly]
        private void TreeListExplorerLoad(object sender, EventArgs e)
        {
            // base.OnLoad(e);
            this.VisibleChanged += this.OnVisibleChanged;
        }
    }
}