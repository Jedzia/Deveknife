//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Recycle.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>13.02.2014 10:49</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    using System;
    using System.Runtime.InteropServices;

    public static class Recycle
    {
        private const int FOF_ALLOWUNDO = 0x40;

        private const int FOF_NOCONFIRMATION = 0x0010;

        private const int FO_DELETE = 3;

        public static void DeleteFileOperation(string filePath)
        {
            var fileop = new SHFILEOPSTRUCT();
            fileop.hNameMappings = IntPtr.Zero;
            fileop.hwnd = IntPtr.Zero;
            fileop.lpszProgressTitle = "Bla Bla";

            fileop.wFunc = FO_DELETE;
            fileop.pFrom = filePath + '\0' + '\0';
            fileop.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
            var result = SHFileOperation(ref fileop);
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;

            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;

            public string pFrom;

            public string pTo;

            public short fFlags;

            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;

            public IntPtr hNameMappings;

            public string lpszProgressTitle;
        }
    }
}