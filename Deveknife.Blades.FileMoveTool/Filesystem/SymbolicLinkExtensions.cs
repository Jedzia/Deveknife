// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SymbolicLinkExtensions.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>29.11.2016 16:25</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    using SystemInterface.IO;

    using Castle.Core.Logging;

    using Microsoft.Win32.SafeHandles;

    public static class SymbolicLinkExtensions
    {
        private const int CREATION_DISPOSITION_OPEN_EXISTING = 3;

        private const int FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;

        private const int FILE_SHARE_READ = 1;

        private const int FILE_SHARE_WRITE = 2;

        // http://msdn.microsoft.com/en-us/library/aa363858(VS.85).aspx
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter",
             Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation",
             Justification = "Reviewed. Suppression is OK here.")]
        [DllImport("kernel32.dll", EntryPoint = "CreateFileW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            int dwDesiredAccess,
            int dwShareMode,

            // ReSharper disable once InconsistentNaming
            IntPtr SecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        // http://msdn.microsoft.com/en-us/library/aa364962%28VS.85%29.aspx
        [DllImport("kernel32.dll", EntryPoint = "GetFinalPathNameByHandleW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetFinalPathNameByHandle(IntPtr handle, [In] [Out] StringBuilder path, int bufLen, int flags);

        /// <summary>
        /// Gets the absolute path of a symbolic link target.
        /// </summary>
        /// <param name="symlink">The symbolic link.</param>
        /// <returns>the absolute path where the symbolic link points to.
        /// </returns>
        /// <exception cref="Win32Exception">The file handle was invalid or GetFinalPathNameByHandle returned a size lesser 0.</exception>
        public static string GetSymbolicLinkTarget(this IFileInfo symlink)
        {
            return SymbolicLinkExtensions.GetSymbolicLinkTargetInternal(symlink.FullName);
        }

        /// <summary>
        /// Gets the absolute path of a symbolic link target.
        /// </summary>
        /// <param name="symlink">The symbolic link.</param>
        /// <returns>the absolute path where the symbolic link points to.</returns>
        /// <exception cref="Win32Exception">The file handle was invalid or GetFinalPathNameByHandle returned a size lesser 0.</exception>
        public static string GetSymbolicLinkTarget(this IDirectoryInfo symlink)
        {
            return SymbolicLinkExtensions.GetSymbolicLinkTargetInternal(symlink.FullName);
        }

        /// <summary>
        /// Determines whether the specified file info points to a valid symbolic link.
        /// </summary>
        /// <param name="fileInfo">The file information of the symbolic link.</param>
        /// <param name="logger">The logger used for exception posting.</param>
        /// <returns><c>true</c> if the symbolic link is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidSymlink(this IFileInfo fileInfo, ILogger logger = null)
        {
            var path = fileInfo.FullName;
            string symlink = null;
            try
            {
                symlink = SymbolicLink.GetTarget(path);
            }
            catch(COMException ex)
            {
                if(logger != null)
                {
                    var message = "Exception at '" + path + "'. " + ex;
                    logger.Error(message);
                    Application.DoEvents();
                }

                return false;
            }

            return symlink != null;
        }

        /// <summary>
        /// Gets the absolute path of a symbolic link target.
        /// </summary>
        /// <param name="symlinkFullName">The full path of the symbolic link.</param>
        /// <returns>the absolute path where the symbolic link points to.</returns>
        /// <exception cref="Win32Exception">The file handle was invalid or GetFinalPathNameByHandle returned a size lesser 0.</exception>
        private static string GetSymbolicLinkTargetInternal(string symlinkFullName)
        {
            using(
                var fileHandle = SymbolicLinkExtensions.CreateFile(
                    symlinkFullName,
                    0,
                    2,
                    IntPtr.Zero,
                    SymbolicLinkExtensions.CREATION_DISPOSITION_OPEN_EXISTING,
                    SymbolicLinkExtensions.FILE_FLAG_BACKUP_SEMANTICS,
                    IntPtr.Zero))
            {
                if(fileHandle.IsInvalid)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                var path = new StringBuilder(512);
                var size = SymbolicLinkExtensions.GetFinalPathNameByHandle(fileHandle.DangerousGetHandle(), path, path.Capacity, 0);
                if(size < 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                // The remarks section of GetFinalPathNameByHandle mentions the return being prefixed with "\\?\"
                // More information about "\\?\" here -> http://msdn.microsoft.com/en-us/library/aa365247(v=VS.85).aspx
                if((path[0] == '\\') && (path[1] == '\\') && (path[2] == '?') && (path[3] == '\\'))
                {
                    return path.ToString().Substring(4);
                }

                return path.ToString();
            }
        }
    }
}