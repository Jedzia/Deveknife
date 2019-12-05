// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryInfoWrap.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>31.07.2015 23:09</date>
// <summary>
//   Wrapper for <see cref="T:System.IO.DirectoryInfo" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Util
{
    using System.IO;
    using System.Security;

    /// <summary>
    /// Wrapper for <see cref="T:System.IO.DirectoryInfo"/> class.
    /// </summary>
    public class DirectoryInfoWrap : IDirectoryInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryInfoWrap"/> class on the specified path.
        /// </summary>
        /// <param name="directoryInfo">A <see cref="T:System.IO.DirectoryInfo" /> object.</param>
        public DirectoryInfoWrap(DirectoryInfo directoryInfo)
        {
            this.DirectoryInfo = directoryInfo;
        }

        /// <inheritdoc />
        public DirectoryInfo DirectoryInfo { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the directory exists.
        /// </summary>
        /// <returns>
        /// true if the directory exists; otherwise, false.
        /// </returns>
        public bool Exists
        {
            get
            {
                return this.DirectoryInfo.Exists;
            }
        }

        /// <inheritdoc />
        public string Extension
        {
            get
            {
                return this.DirectoryInfo.Extension;
            }
        }

        /// <inheritdoc />
        public string FullName
        {
            get
            {
                return this.DirectoryInfo.FullName;
            }
        }

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return this.DirectoryInfo.Name;
            }
        }

        /// <inheritdoc />
        public IDirectoryInfo Parent
        {
            get
            {
                return new DirectoryInfoWrap(this.DirectoryInfo.Parent);
            }
        }

        /// <inheritdoc />
        public IDirectoryInfo Root
        {
            get
            {
                return new DirectoryInfoWrap(this.DirectoryInfo.Root);
            }
        }

        /// <inheritdoc />
        public IDirectoryInfo[] GetDirectories()
        {
            var directoryInfos = this.DirectoryInfo.GetDirectories();
            return DirectoryInfoWrap.ConvertDirectoryInfoArrayIntoIDirectoryInfoWrapArray(directoryInfos);
        }

        private static IDirectoryInfo[] ConvertDirectoryInfoArrayIntoIDirectoryInfoWrapArray(
            DirectoryInfo[] directoryInfos)
        {
            var directoryInfoWraps = new IDirectoryInfo[directoryInfos.Length];
            for (var i = 0; i < directoryInfos.Length; i++)
            {
                directoryInfoWraps[i] = new DirectoryInfoWrap(directoryInfos[i]);
            }

            return directoryInfoWraps;
        }
    }
}