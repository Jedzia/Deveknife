//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EITConversions.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>09.12.2013 09:36</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Eit.Formats
{
    internal static class EITConversions
    {
        public static int ToBcd(int dec)
        {
            if (dec > 100)
            {
            }
            return (((dec / 10) * 0x10) + (dec % 10));
        }
    }
}