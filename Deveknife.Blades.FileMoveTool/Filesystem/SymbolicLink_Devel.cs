// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SymbolicLink_Devel.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>29.11.2016 00:48</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.InteropServices;

    /// <summary>
    /// SymbolicLink Development testing class. Todo: remove when finished.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation",
         Justification = "Reviewed. Suppression is OK here.")]

    // see http://stackoverflow.com/questions/1485155/check-if-a-file-is-real-or-a-symbolic-link and http://troyparsons.com/blog/2012/03/symbolic-links-in-c-sharp/
    // ReSharper disable InternalMembersMustHaveComments
    // ReSharper disable once InconsistentNaming
    internal class SymbolicLink_Devel
    {
        /// <summary>
        /// Enum SymbolicLink
        /// </summary>
        internal enum SymbolicLink
        {
            /// <summary>
            /// The file
            /// </summary>
            File = 0,

            /// <summary>
            /// The directory
            /// </summary>
            Directory = 1
        }

        /// <summary>
        /// Creates the link.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void CreateLink(string[] args)
        {
            string symbolicLink = @"c:\bar.txt";
            string fileName = @"c:\temp\foo.txt";

            using(var writer = File.CreateText(fileName))
            {
                writer.WriteLine("Hello World");
            }

            SymbolicLink_Devel.CreateSymbolicLink(symbolicLink, fileName, SymbolicLink.File);
        }

        [DllImport("kernel32.dll")]
        private static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLink dwFlags);
    }
}