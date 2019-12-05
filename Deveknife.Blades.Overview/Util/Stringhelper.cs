//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Stringhelper.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.02.2014 18:22</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Util
{
    using System;

    public static class Stringhelper
    {
        public static string TruncateLongString(this string str, int maxLength)
        {
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }

        public static string TruncateLongString(this string str, int maxLength, int startIndex)
        {
            return str.Substring(startIndex, Math.Min(str.Length - startIndex, maxLength));
        }


        public static string TruncateLongStringFromTheBack(this string str, int maxLength)
        {
            // this is the end.
            // 1234567890123456
            //         |
            // len = 16 ... the last 8

            var startIndex = Math.Max(0, str.Length - maxLength);
            var result = str.Substring(startIndex, str.Length - startIndex);
            return result;
        }
    }
}