//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MenuColumnInfo.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.02.2014 17:26</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    using DevExpress.XtraGrid.Columns;

    internal class MenuColumnInfo
    {
        public GridColumn Column;

        public MenuColumnInfo(GridColumn column)
        {
            this.Column = column;
        }
    }

    /*internal class EITFormatDisplayList : List<EITFormatDisplay>
    {
    }*/
}