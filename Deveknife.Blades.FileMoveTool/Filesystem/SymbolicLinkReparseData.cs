// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SymbolicLinkReparseData.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>29.11.2016 00:43</date>
// --------------------------------------------------------------------------------------------------------------------
// ReSharper disable InternalMembersMustHaveComments
namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
         Justification = "Reviewed. Suppression is OK here.")]

    //// 
    //// Refer to http://msdn.microsoft.com/en-us/library/windows/hardware/ff552012%28v=vs.85%29.aspx
    //// 
    [StructLayout(LayoutKind.Sequential)]
    internal struct SymbolicLinkReparseData
    {
        // Not certain about this!
        private const int maxUnicodePathLength = 260 * 2;

        public uint ReparseTag;

        public ushort ReparseDataLength;

        public ushort Reserved;

        public ushort SubstituteNameOffset;

        public ushort SubstituteNameLength;

        public ushort PrintNameOffset;

        public ushort PrintNameLength;

        public uint Flags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SymbolicLinkReparseData.maxUnicodePathLength)]
        public byte[] PathBuffer;
    }
}