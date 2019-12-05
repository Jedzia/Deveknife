//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EITStringHelper.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>09.12.2013 09:17</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Eit.Formats
{
    using Microsoft.VisualBasic;

    internal static class EITStringHelper
    {
        public static string STrim(string TestString)
        {
            if (Strings.Left(TestString, 1) == " ")
            {
                TestString = Strings.Right(TestString, TestString.Length - 1);
            }
            if (Strings.Left(TestString, 1) == "\x0005")
            {
                TestString = Strings.Right(TestString, TestString.Length - 1);
            }
            if (Strings.Right(TestString, 1) == "\x0005")
            {
                TestString = Strings.Left(TestString, TestString.Length - 1);
            }
            return TestString;
        }

    }
}