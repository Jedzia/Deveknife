// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SymbolicLink.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>29.11.2016 00:35</date>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable InconsistentNaming
namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    using Microsoft.Win32.SafeHandles;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
         Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
         Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation",
         Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1303:ConstFieldNamesMustBeginWithUpperCaseLetter",
         Justification = "Reviewed. Suppression is OK here.")]
    internal static class SymbolicLink
    {
        internal const uint symLinkTag = 0xA000000C;

        private const uint fileFlagsForOpenReparsePointAndBackupSemantics = 0x02200000;

        private const uint genericReadAccess = 0x80000000;

        private const int ioctlCommandGetReparsePoint = 0x000900A8;

        private const uint openExisting = 0x3;

        private const uint pathNotAReparsePointError = 0x80071126;

        private const uint shareModeAll = 0x7; // Read, Write, Delete

        private const int targetIsADirectory = 1;

        private const int targetIsAFile = 0;

        /// <exception cref="IOException">Condition.</exception>
        public static void CreateDirectoryLink(string linkPath, string targetPath)
        {
            if(!SymbolicLink.CreateSymbolicLink(linkPath, targetPath, SymbolicLink.targetIsADirectory) || (Marshal.GetLastWin32Error() != 0))
            {
                try
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }
                catch(COMException exception)
                {
                    throw new IOException(exception.Message, exception);
                }
            }
        }

        public static void CreateFileLink(string linkPath, string targetPath)
        {
            if(!SymbolicLink.CreateSymbolicLink(linkPath, targetPath, SymbolicLink.targetIsAFile))
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
        }

        public static bool Exists(string path)
        {
            if(!Directory.Exists(path) && !File.Exists(path))
            {
                return false;
            }

            var target = SymbolicLink.GetTarget(path);
            return target != null;
        }

        [CanBeNull]
        public static string GetTarget(string path)
        {
            SymbolicLinkReparseData reparseDataBuffer;

            var reparseDataBufferResult = SymbolicLink.GetReparseData(path);
            if(!reparseDataBufferResult.IsValid)
            {
                return null;
            }

            reparseDataBuffer = reparseDataBufferResult.ReparseData;
            if(reparseDataBuffer.ReparseTag != SymbolicLink.symLinkTag)
            {
                return null;
            }

            var target = Encoding.Unicode.GetString(
                reparseDataBuffer.PathBuffer,
                reparseDataBuffer.PrintNameOffset,
                reparseDataBuffer.PrintNameLength);

            return target;
        }

        internal static SymbolicLinkReparseDataResult GetReparseData(string path)
        {
            SymbolicLinkReparseData reparseDataBuffer;
            using(var fileHandle = SymbolicLink.GetFileHandle(path))
            {
                if(fileHandle.IsInvalid)
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }

                var outBufferSize = Marshal.SizeOf(typeof(SymbolicLinkReparseData));
                var outBuffer = IntPtr.Zero;
                try
                {
                    outBuffer = Marshal.AllocHGlobal(outBufferSize);
                    int bytesReturned;
                    var success = SymbolicLink.DeviceIoControl(
                        fileHandle.DangerousGetHandle(),
                        SymbolicLink.ioctlCommandGetReparsePoint,
                        IntPtr.Zero,
                        0,
                        outBuffer,
                        outBufferSize,
                        out bytesReturned,
                        IntPtr.Zero);

                    fileHandle.Close();

                    if(!success)
                    {
                        if((uint)Marshal.GetHRForLastWin32Error() == SymbolicLink.pathNotAReparsePointError)
                        {
                            return new SymbolicLinkReparseDataResult();
                        }

                        Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    }

                    reparseDataBuffer = (SymbolicLinkReparseData)Marshal.PtrToStructure(outBuffer, typeof(SymbolicLinkReparseData));
                }
                finally
                {
                    Marshal.FreeHGlobal(outBuffer);
                }
            }

            return new SymbolicLinkReparseDataResult(reparseDataBuffer);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        // ReSharper disable once TooManyArguments
        private static extern SafeFileHandle CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, int dwFlags);

        // ReSharper disable once TooManyArguments
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DeviceIoControl(
            IntPtr hDevice,
            uint dwIoControlCode,
            IntPtr lpInBuffer,
            int nInBufferSize,
            IntPtr lpOutBuffer,
            int nOutBufferSize,
            out int lpBytesReturned,
            IntPtr lpOverlapped);

        private static SafeFileHandle GetFileHandle(string path)
        {
            return SymbolicLink.CreateFile(
                path,
                SymbolicLink.genericReadAccess,
                SymbolicLink.shareModeAll,
                IntPtr.Zero,
                SymbolicLink.openExisting,
                SymbolicLink.fileFlagsForOpenReparsePointAndBackupSemantics,
                IntPtr.Zero);
        }
    }
}