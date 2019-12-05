// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArchivePlannerUiGridMenu.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 09:22</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.ArchivePlanner
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using DevExpress.Utils.Menu;
    using DevExpress.XtraGrid.Views.Grid;

    /// <summary>
    /// Represents the Context-Menu for the ArchivePlannerUi´s gridview.
    /// </summary>
    /// <typeparam name="T">Underlying type of the Gridview´s data.</typeparam>
    internal class ArchivePlannerUiGridMenu<T>
        where T : class
    {
        private readonly Dictionary<string, ArchivePlannerUiGridMenuItem<T>> actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchivePlannerUiGridMenu{T}" /> class.
        /// </summary>
        /// <param name="gridview">The bound gridview.</param>
        public ArchivePlannerUiGridMenu(GridView gridview)
        {
            this.actions = new Dictionary<string, ArchivePlannerUiGridMenuItem<T>>();
            gridview.PopupMenuShowing += this.GridView1PopupMenuShowing;
        }

        /// <summary>
        /// Adds the specified action to the grid's context menu.
        /// </summary>
        /// <param name="menuCaption">The menu caption.</param>
        /// <param name="action">The action to add.</param>
        /// <param name="image">The image of the action.</param>
        public void AddAction(string menuCaption, Action<IEnumerable<T>, string> action, Image image = null)
        {
            this.actions.Add(menuCaption, new ArchivePlannerUiGridMenuItem<T>(menuCaption, action, image));
        }

        private DXMenuItem CreateCopyMenuItem(string caption, GridView view, Image image = null, EventHandler eventHandler = null)
        {
            if (eventHandler == null)
            {
                eventHandler = this.OnHandleGridMenuItemClick;
            }

            var item = new DXMenuItem(caption, eventHandler, image) { Tag = view };
            return item;
        }

        /// <summary>
        /// Handles the PopupMenuShowing event of the GridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PopupMenuShowingEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the <see cref="E:Handle_GridMenuItemClick" /> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
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

            ArchivePlannerUiGridMenuItem<T> action;
            var success = this.actions.TryGetValue(menuItem.Caption, out action);
            if (success)
            {
                action.Action(rows, menuItem.Caption);
            }
        }
    }
}