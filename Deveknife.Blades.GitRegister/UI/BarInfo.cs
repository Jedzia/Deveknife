// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarInfo.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>21.10.2016 20:16</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.GitRegister.UI
{
    using System.Drawing;

    using DevExpress.XtraBars;

    /// <summary>
    /// Helper to accumulate data and aid in the creation of BarButtonItem/BarCheckItem's.
    /// </summary>
    public class BarInfo
    {
        private readonly string caption;

        private readonly bool check;

        private readonly bool beginItemGroup;

        private readonly int groupIndex;

        private readonly ItemClickEventHandler handler;

        private readonly Image image;

        private readonly BarInfo[] info;

        private readonly bool isCheckItem;

        private BarItem item;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarInfo"/> class.
        /// </summary>
        /// <param name="caption">The caption of the item.</param>
        /// <param name="handler">The (item clicked) handler.</param>
        /// <param name="image">The image of the button.</param>
        /// <param name="isCheckItem">if set to <c>true</c> the button becomes a check button.</param>
        /// <param name="check">The initial value of the check button.if set to <c>true</c> the button is checked.</param>
        /// <param name="beginItemGroup">if set to <c>true</c> then begin a new visual item group.</param>
        /// <param name="info">The children of this instance.</param>
        /// <param name="groupIndex">Group-Index of the children.</param>
        // ReSharper disable once TooManyDependencies
        public BarInfo(
            string caption,
            ItemClickEventHandler handler,
            Image image,
            bool isCheckItem,
            bool check,
            bool beginItemGroup,
            [CanBeNull] BarInfo[] info = null,
            int groupIndex = -1)
        {
            this.caption = caption;
            this.handler = handler;
            this.image = image;
            this.isCheckItem = isCheckItem;
            this.check = check;
            this.beginItemGroup = beginItemGroup;
            this.info = info;
            this.groupIndex = groupIndex;
        }

        /// <summary>
        /// Gets the bar item of this instance.
        /// </summary>
        /// <value>The bar item.</value>
        public BarItem BarItem
        {
            get
            {
                return this.item;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance starts a group.
        /// </summary>
        /// <value><b>true</b>, if the current instance starts a group, otherwise, <b>false</b></value>
        public bool BeginItemGroup
        {
            get
            {
                return this.beginItemGroup;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BarInfo"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get
            {
                return this.item.Enabled;
            }

            set
            {
                this.item.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BarInfo"/> is pushed/checked.
        /// </summary>
        /// <value><c>true</c> if pushed; otherwise, <c>false</c>.</value>
        public bool Pushed
        {
            get
            {
                var barCheckItem = this.item as BarCheckItem;
                if (barCheckItem == null)
                {
                    return false;
                }

                return barCheckItem.Checked;
            }

            set
            {
                var barCheckItem = this.item as BarCheckItem;
                if (barCheckItem != null)
                {
                    barCheckItem.Checked = value;
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="T:DevExpress.Xpf.Bars.BarItem"/> from the current instance data.
        /// </summary>
        /// <param name="manager">The bar manager in use.</param>
        /// <returns>a new BarItem associated with the data of this instance.</returns>
        /// <remarks>The group-index of the BarItem is set to -1.</remarks>
        public BarItem CreateItem(BarManager manager)
        {
            return this.CreateItem(manager, -1);
        }

        /// <summary>
        /// Creates a <see cref="T:DevExpress.Xpf.Bars.BarItem"/> from the current instance data.
        /// </summary>
        /// <param name="manager">The bar manager in use.</param>
        /// <param name="itemGroupIndex">Index of the group to create.</param>
        /// <returns>a new BarItem associated with the data of this instance.</returns>
        public BarItem CreateItem(BarManager manager, int itemGroupIndex)
        {
            if (this.isCheckItem)
            {
                this.item = new BarCheckItem(manager, this.check) { Caption = this.caption };
                if (itemGroupIndex != -1)
                {
                    ((BarCheckItem)this.item).GroupIndex = itemGroupIndex;
                }
            }
            else
            {
                this.item = new BarButtonItem(manager, this.caption);
            }

            if (this.info != null)
            {
                var barButtonItem = this.item as BarButtonItem;
                if (barButtonItem != null)
                {
                    barButtonItem.ButtonStyle = BarButtonStyle.DropDown;
                    var menu = new PopupMenu(manager);
                    foreach (var barInfo in this.info)
                    {
                        menu.ItemLinks.Add(barInfo.CreateItem(manager, this.groupIndex));
                    }

                    barButtonItem.DropDownControl = menu;
                }
            }

            this.item.ItemClick += this.handler;
            this.item.Glyph = this.image;
            this.item.Hint = this.caption;
            return this.item;
        }
    }
}