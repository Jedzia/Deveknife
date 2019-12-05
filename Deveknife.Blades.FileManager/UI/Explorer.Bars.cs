// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Explorer.Bars.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 10:18</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.UI
{
    using System;
    using System.Collections;

    using DevExpress.XtraBars;

    /// <summary>
    /// An explorer-like control for directory navigation.
    /// </summary>
    public partial class TreeListExplorer
    {
        [NotNull]
        private readonly ArrayList barInfos = new ArrayList();

        private Bar bar;

        /// <summary>
        /// Gets the name of the bar.
        /// </summary>
        /// <value>The name of the bar.</value>
        protected string BarName
        {
            get
            {
                return "Explorer";
            }
        }

        /// <summary>
        /// Gets the bar manager.
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
                foreach(Bar bar2 in this.Manager.Bars)
                {
                    if(bar2.BarName == this.BarName)
                    {
                        return bar2;
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
            if(this.barInfos.Count <= index)
            {
                return false;
            }

            return ((BarInfo)this.barInfos[index]).Pushed;
        }

        /// <summary>
        /// Initializes the menu bar.
        /// </summary>
        protected void InitBarInfo()
        {
            this.barInfos.Add(
                new BarInfo("Show Logical Drivers", this.ShowLogicalDriversClick, this.imageList3.Images[0], true, true, false));
            this.barInfos.Add(new BarInfo("Assign Check", this.AssignCheckClick, this.imageList3.Images[1], true, true, false));
            this.barInfos.Add(
                new BarInfo("Even And Odd Style Enabled", this.EvenOddStyleClick, this.imageList3.Images[2], true, true, false));
            this.barInfos.Add(new BarInfo("Show Footer", this.ShowFooterClick, this.imageList3.Images[3], true, false, false));
            this.barInfos.Add(new BarInfo("Show Preview", this.ShowPreviewClick, this.imageList3.Images[6], true, false, false));
            this.barInfos.Add(new BarInfo("Show Grid", this.ShowGridClick, this.imageList3.Images[7], true, false, false));
            this.barInfos.Add(new BarInfo("Alpha Blending", this.AlphaBlendingClick, this.imageList3.Images[8], false, false, false));
            this.barInfos.Add(new BarInfo("Checked Items List", this.ListClick, this.imageList3.Images[4], false, false, true));
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
        /// Sets the bar item the enabled state.
        /// </summary>
        /// <param name="index">The index of the bar item.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        protected void SetBarItemEnabled(int index, bool enabled)
        {
            this.InitBars();
            if(this.barInfos.Count > index)
            {
                ((BarInfo)this.barInfos[index]).Enabled = enabled;
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

        private void CreateBar()
        {
            this.bar = new Bar(this.Manager) { BarName = this.Text = this.BarName, DockStyle = BarDockStyle.Top };

            foreach(BarInfo info in this.barInfos)
            {
                var item = info.CreateItem(this.Manager);
                this.bar.AddItem(item).BeginGroup = info.BeginItemGroup;
            }
        }

        private void HideBar()
        {
            if((this.BarName != string.Empty) && (this.Manager != null))
            {
                this.Bar.Visible = false;
            }
        }

        private void InitBars()
        {
            if(this.barInfos.Count == 0)
            {
                this.InitBarInfo();
            }

            if((this.Manager != null) && (this.Bar == null))
            {
                this.CreateBar();
            }
        }

        private void ShowBar()
        {
            if((this.BarName != string.Empty) && (this.Manager != null))
            {
                this.InitBars();
                this.Bar.Visible = true;
            }
        }
    }
}