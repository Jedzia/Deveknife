// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Explorer.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>26.07.2015 20:16</date>
// <summary>
//   An explorer-like control for directory navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.UI
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Windows.Forms;

    using Castle.Core.Logging;

    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Nodes;

    using Guard = Deveknife.Guard;

    /// <summary>
    /// An explorer-like control for directory navigation.
    /// </summary>
    public partial class TreeListExplorer : XtraUserControl
    {
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
        /// Gets the export control.
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
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets the selected directory.
        /// </summary>
        /// <value>The selected directory.</value>
        [Description("SelectedDirectory")]
        [Category("SelectedDirectory Category")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string SelectedDirectory { get; private set; }

        /// <summary>
        /// Jumps to the specified path.
        /// </summary>
        /// <param name="path">The directory.</param>
        public void JumpToPath(string path)
        {
            // int columnID1 = treeList1.Columns[colName.Caption].AbsoluteIndex;
            // int columnID1a = treeList1.Columns.IndexOf(colName);
            var p = new FileInfo(path);
            var d = new DirectoryInfo(p.DirectoryName);
            if (d.Exists)
            {
                var root = d.Root;
                var rootNode = this.treeList1.FindNodeByFieldValue("Name", root.FullName);
                rootNode.Selected = true;
                rootNode.Expanded = true;
                var dirparts = d.FullName.Split('\\');
                var curnode = rootNode;
                for (var i = 1; i < dirparts.Length; i++)
                {
                    var curdirpart = dirparts[i];

                    // var y = curnode.Nodes.Select(node => node.GetValue(colName.AbsoluteIndex));
                    curnode = this.FindNodeByName(curnode, curdirpart);
                    if (curnode == null)
                    {
                        return;
                    }

                    curnode.Selected = true;
                    curnode.Expanded = true;
                }
            }

            if (!p.Exists)
            {
            }
        }

        private void CheckedItemsInfo(TreeListNode pnode, ref string s)
        {
            var nodes = pnode != null ? pnode.Nodes : this.treeList1.Nodes;
            foreach (TreeListNode node in nodes)
            {
                var nodeChecked = (CheckState)node.GetValue("Check");
                if (nodeChecked != CheckState.Unchecked)
                {
                    if (nodeChecked == CheckState.Checked && node.GetDisplayText("Type") == "File")
                    {
                        s += node.GetDisplayText("FullName") + "\r\n";
                    }

                    this.CheckedItemsInfo(node, ref s);
                }
            }
        }

        private string CheckedItemsInfoMain()
        {
            var s = string.Empty;
            this.CheckedItemsInfo(null, ref s);
            if (s == string.Empty)
            {
                s = "No checked files...";
            }

            return s;
        }

        private void EvenOddStyleClick(object sender, ItemClickEventArgs e)
        {
            this.treeList1.OptionsView.EnableAppearanceEvenRow = this.GetBarItemPushed(2);
        }

        private TreeListNode FindNodeByName(TreeListNode curnode, string name)
        {
            return curnode.Nodes.FirstOrDefault(
                node =>
                    {
                        var value = node.GetValue(this.colName.AbsoluteIndex) as string;
                        return value == name;
                    });
        }

        private bool HasFiles(string path)
        {
            string[] root;
            try
            {
                root = Directory.GetFiles(path);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                this.Log(unauthorizedAccessException.Message);
                return false;
            }

            if (root.Length > 0)
            {
                return true;
            }

            root = Directory.GetDirectories(path);
            if (root.Length > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Initializes the drives.
        /// </summary>
        private void InitDrives()
        {
            this.treeList1.BeginUnboundLoad();
            try
            {
                var root = Directory.GetLogicalDrives();

                foreach (var s in root)
                {
                    var node =
                        this.treeList1.AppendNode(
                            new object[] { s, s, "Logical Driver", null, null, CheckState.Unchecked }, 
                            null);
                    node.HasChildren = true;
                    node.Tag = true;
                }
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
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
                var root = Directory.GetFiles(path);
                foreach (var s in root)
                {
                    var fi = new FileInfo(s);
                    var node =
                        this.treeList1.AppendNode(
                            new object[] { s, fi.Name, "File", fi.Length, fi.Attributes, check }, 
                            parentNode);
                    node.HasChildren = false;
                }

                this.OnFilesAvailable(new FilesEventArgs(root));
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
                // TODO: Handle the UnauthorizedAccessException 
            }
            catch (SecurityException)
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
                var root = Directory.GetDirectories(path);
                foreach (var s in root)
                {
                    try
                    {
                        var di = new DirectoryInfo(s);
                        var node =
                            this.treeList1.AppendNode(
                                new object[] { s, di.Name, "Folder", null, di.Attributes, check }, 
                                parentNode);
                        node.HasChildren = this.HasFiles(s);
                        if (node.HasChildren)
                        {
                            node.Tag = true;
                        }
                    }
                    catch (SecurityException)
                    {
                        // TODO: Handle the SecurityException 
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                // TODO: Handle the UnauthorizedAccessException 
            }
            catch (IOException)
            {
                // TODO: Handle the IOException 
            }

            this.InitFiles(path, parentNode, check);
            this.treeList1.EndUnboundLoad();
        }

        private void InitRoot()
        {
            if (!this.GetBarItemPushed(0))
            {
                try
                {
                    this.InitFolders(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()), null, CheckState.Unchecked);
                }
                catch (UnauthorizedAccessException)
                {
                }
                catch (PathTooLongException)
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
            f.Location = ControlUtils.CalcLocation(MousePosition, MousePosition, f.Size);
            f.ShowDialog();
        }

        private void Log(string message)
        {
            var logger = this.Logger;
            if (logger != null)
            {
                logger.Info(message);
            }
        }

        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (var i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i]["Check"] = check;
                this.SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        private void SetCheckedNode(TreeListNode node)
        {
            var check = (CheckState)node.GetValue("Check");
            if (check == CheckState.Indeterminate || check == CheckState.Unchecked)
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
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                var b = false;
                for (var i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    if (!check.Equals(node.ParentNode.Nodes[i]["Check"]))
                    {
                        b = true;
                        break;
                    }
                }

                node.ParentNode["Check"] = b ? CheckState.Indeterminate : check;
                this.SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        private void ShowFooterClick(object sender, ItemClickEventArgs e)
        {
            this.treeList1.OptionsView.ShowSummaryFooter = this.GetBarItemPushed(3);
        }

        private void ShowGridClick(object sender, ItemClickEventArgs e)
        {
            this.treeList1.OptionsView.ShowHorzLines =
                this.treeList1.OptionsView.ShowVertLines = this.GetBarItemPushed(5);
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
            if (node != null)
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
            if (e.Node.Tag != null)
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
            var type1 = e.Node1.GetDisplayText("Type");
            var type2 = e.Node2.GetDisplayText("Type");
            if (type1 != type2)
            {
                if (type1 == "Folder")
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
            var path = e.Node.GetValue(this.colFullName.AbsoluteIndex) as string;
            this.SelectedDirectory = path;
            if(path != null)
            {
                this.OnDirectoryChanged(new DirectoryChangedEventArgs(path));
            }
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
            if (e.Node.GetDisplayText("Type") == "Folder")
            {
                e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
            }
            else if (e.Node.GetDisplayText("Type") == "File")
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
            if (check == CheckState.Unchecked)
            {
                e.NodeImageIndex = 0;
            }
            else if (check == CheckState.Checked)
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
            if (e.KeyData == Keys.Space)
            {
                this.SetCheckedNode(this.treeList1.FocusedNode);
            }

            if(e.KeyData != Keys.Enter)
            {
                return;
            }

            if (this.treeList1.FocusedNode.GetDisplayText("Type") == "File")
            {
                try
                {
                    var process = new Process();
                    process.StartInfo.FileName = this.treeList1.FocusedNode.GetDisplayText("FullName");
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch (InvalidOperationException)
                {
                }
                catch (Win32Exception)
                {
                    // TODO: Handle the Win32Exception 
                }
            }
            else if (this.treeList1.FocusedNode.HasChildren)
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
            if (e.Button == MouseButtons.Left)
            {
                var hitInfo = this.treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if (hitInfo.HitInfoType == HitInfoType.StateImage)
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
        private void TreeListExplorerLoad(object sender, EventArgs e)
        {
            // base.OnLoad(e);
            this.VisibleChanged += this.OnVisibleChanged;
        }
    }
}