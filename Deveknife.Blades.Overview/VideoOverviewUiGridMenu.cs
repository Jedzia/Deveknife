// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VideoOverviewUiGridMenu.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>21.02.2014 19:36</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using DevExpress.Utils.Menu;
    using DevExpress.XtraGrid.Views.Grid;

    /// <summary>
    /// Represents the Context-Menu for the VideoOverviewUi's gridview.
    /// </summary>
    /// <typeparam name="T">Underlying type of the Gridview's data.</typeparam>
    internal class VideoOverviewUiGridMenu<T>
        where T : class
    {
        private readonly Dictionary<string, VideoOverviewUiGridMenuItem<T>> actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoOverviewUiGridMenu{T}" /> class.
        /// </summary>
        /// <param name="gridview">The bound gridview.</param>
        public VideoOverviewUiGridMenu(GridView gridview)
        {
            this.actions = new Dictionary<string, VideoOverviewUiGridMenuItem<T>>();
            gridview.PopupMenuShowing += this.GridView1PopupMenuShowing;
        }

        /// <summary>
        /// Adds the action.
        /// </summary>
        /// <param name="menuCaption">The menu caption.</param>
        /// <param name="action">The action.</param>
        /// <param name="image">The image.</param>
        public void AddAction(string menuCaption, Action<IEnumerable<T>, string> action, Image image = null)
        {
            this.actions.Add(menuCaption, new VideoOverviewUiGridMenuItem<T>(menuCaption, action, image));
        }

        private DXMenuItem CreateCopyMenuItem(
            string caption, 
            GridView view, 
            Image image = null, 
            EventHandler eventHandler = null)
        {
            if (eventHandler == null)
            {
                eventHandler = this.OnHandleGridMenuItemClick;
            }

            var item = new DXMenuItem(caption, eventHandler, image) { Tag = view };
            return item;
        }

        private void GridView1PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType != GridMenuType.Row)
            {
                return;
            }

            e.Menu.Items.Clear();
            foreach (var action in this.actions)
            {
                var menuItem = this.CreateCopyMenuItem(action.Value.Caption, e.Menu.View, action.Value.Image);
                e.Menu.Items.Add(menuItem);
            }
        }

        private void OnHandleGridMenuItemClick(object sender, EventArgs e)
        {
            var menuItem = sender as DXMenuItem;
            if (menuItem == null)
            {
                return;
            }

            var view = menuItem.Tag as GridView;
            if (view == null)
            {
                return;
            }

            if (view.SelectedRowsCount == 0)
            {
                return;
            }

            // Create an empty list.
            var rows = new List<T>();

            // Add the selected rows to the list.
            for (var i = 0; i < view.SelectedRowsCount; i++)
            {
                var selectedRow = view.GetSelectedRows()[i];
                if (selectedRow < 0)
                {
                    continue;
                }

                var dataRow = view.GetRow(selectedRow) as T;
                if (dataRow != null)
                {
                    rows.Add(dataRow);
                }
            }

            VideoOverviewUiGridMenuItem<T> action;
            var success = this.actions.TryGetValue(menuItem.Caption, out action);
            if (success)
            {
                action.Action(rows, menuItem.Caption);
            }
        }
    }
}