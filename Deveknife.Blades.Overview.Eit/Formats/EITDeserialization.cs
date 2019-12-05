//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EITDeserialization.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>09.12.2013 09:20</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Eit.Formats
{
    using System;

    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    internal static class EITDeserialization
    {
        internal static object GetString(byte[] streamData, int Start, int Anzahl)
        {
            var str = "";
            var num2 = (Start + Anzahl) - 1;
            for (var i = Start; i <= num2; i++)
            {
                if (streamData[i] == 13)
                {
                    str = str + "\r\n";
                }
                else if (streamData[i] < 0x20)
                {
                    str = String.Format("{0} ", str);
                }
                else if (streamData[i] == 0x8a)
                {
                    str = str + "\r\n";
                }
                else
                {
                    str = str + Conversions.ToString(Strings.Chr(streamData[i]));
                }
            }
            return EITStringHelper.STrim(str);
        }

        internal static string GetTime(byte[] streamData, object StartByte)
        {
            var expression = Conversion.Hex(streamData[Conversions.ToInteger(StartByte)]);
            var str3 = Conversion.Hex(streamData[Conversions.ToInteger(Operators.AddObject(StartByte, 1))]);
            var str4 = Conversion.Hex(streamData[Conversions.ToInteger(Operators.AddObject(StartByte, 2))]);
            if (Strings.Len(expression) == 1)
            {
                expression = String.Format((string)"0{0}", (object)expression);
            }
            if (Strings.Len(str3) == 1)
            {
                str3 = String.Format((string)"0{0}", (object)str3);
            }
            if (Strings.Len(str4) == 1)
            {
                str4 = String.Format((string)"0{0}", (object)str4);
            }
            return String.Format("{0}:{1}:{2}", expression, str3, str4);
        }

        internal static string GetPID(byte[] streamData, int start)
        {
            var str2 = Conversion.Hex(streamData[start]);
            var str3 = Conversion.Hex(streamData[start + 1]);
            if (str2.Length == 1)
            {
                str2 = String.Format((string)"0{0}", (object)str2);
            }
            if (str3.Length == 1)
            {
                str3 = String.Format((string)"0{0}", (object)str3);
            }
            return (str2 + str3);
        }

        public static string GetDescExtended(byte[] streamData, int Zeiger, string evDesc)
        {
            var description =
                Conversions.ToString(
                    Operators.ConcatenateObject(
                        evDesc,
                        GetString(streamData, Zeiger + 8, streamData[Zeiger + 7])));
            return EITStringHelper.STrim(description);
        }
    }
}