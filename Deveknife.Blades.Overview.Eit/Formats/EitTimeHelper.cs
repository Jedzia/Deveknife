//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EitTimeHelper.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>09.12.2013 09:17</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Eit.Formats
{
    using System;

    using Microsoft.VisualBasic;

    internal static class EitTimeHelper
    {
        public static string ParseTime(byte T1, byte T2, byte T3, byte T4, byte T5)
        {
            var expression = Conversion.Hex(T3);
            var str = Conversion.Hex(T4);
            var str4 = Conversion.Hex(T5);
            if (Strings.Len(expression) == 1)
            {
                expression = String.Format((string)"0{0}", (object)expression);
            }
            if (Strings.Len(str) == 1)
            {
                str = String.Format((string)"0{0}", (object)str);
            }
            if (Strings.Len(str4) == 1)
            {
                str4 = String.Format((string)"0{0}", (object)str4);
            }
            var num = (T1 * 0x100) + T2;
            var str3 = Strings.Left(DateAndTime.DateAdd("d", num, "17.11.1858").ToString(), 10);
            return String.Format("{0} {1}:{2}:{3}", new object[] { str3, expression, str, str4 });
        }

        public static int ParseToDVBTime(string MJD)
        {
            var time = new DateTime(0x742, 11, 0x11);
            if (MJD != null)
            {
                var num = (int)DateAndTime.DateDiff("d", time, MJD, FirstDayOfWeek.Sunday, FirstWeekOfYear.Jan1);
                if (num < 0)
                {
                    num = 0;
                }
                return num;
            }
            return 0;
        }
    }
}