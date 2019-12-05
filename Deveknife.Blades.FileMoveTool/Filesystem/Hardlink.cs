// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Hardlink.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>29.11.2016 00:21</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    using System.Text;

    internal static class Hardlink
    {
        internal const uint hardLinkTag = 0xA0000003;

        [CanBeNull]
        public static string GetTarget(string path)
        {
            var reparseDataBufferResult = SymbolicLink.GetReparseData(path);
            if(!reparseDataBufferResult.IsValid)
            {
                return null;
            }

            var reparseDataBuffer = reparseDataBufferResult.ReparseData;
            if (reparseDataBuffer.ReparseTag != Hardlink.hardLinkTag)
            {
                return null;
            }

            string target = Encoding.Unicode.GetString(
                reparseDataBuffer.PathBuffer,
                reparseDataBuffer.PrintNameOffset,
                reparseDataBuffer.PrintNameLength);

            return target;
        }
    }
}