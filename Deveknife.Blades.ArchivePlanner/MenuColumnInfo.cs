// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuColumnInfo.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 09:25</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.ArchivePlanner
{
    using DevExpress.XtraGrid.Columns;

    /// <summary>
    /// Template Class MenuColumnInfo.
    /// </summary>
    internal class MenuColumnInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuColumnInfo"/> class.
        /// </summary>
        /// <param name="column">The column.</param>
        public MenuColumnInfo([NotNull] GridColumn column)
        {
            this.Column = Guard.NotNull(() => column, column);
        }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>The column.</value>
        public GridColumn Column { get; set; }
    }
}