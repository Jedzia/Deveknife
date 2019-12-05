// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDirectoryInfo.cs" company="EvePanix">
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
    using System;
    using System.IO;

    /// <summary>
    /// Wrapper for <see cref="T:System.IO.DirectoryInfo"/> class.
    /// </summary>
    [CLSCompliant(false)]
    public interface IDirectoryInfo
    {
        /// <summary>
        /// Gets <see cref="T:System.IO.DirectoryInfo"/> object.
        /// </summary>
        DirectoryInfo DirectoryInfo { get; }

        /// <summary>
        /// Gets a value indicating whether the directory exists.
        /// </summary>
        /// <returns>
        /// true if the directory exists; otherwise, false.
        /// </returns>
        bool Exists { get; }

        /// <summary>
        /// Gets the string representing the extension part of the file.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Gets the full path of the directory or file.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the name of this <see cref="T:System.IO.DirectoryInfo"/> instance. 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the parent directory of a specified subdirectory. 
        /// </summary>
        IDirectoryInfo Parent { get; }

        /// <summary>
        /// Gets a <see cref="T:SystemInterface.IO.IDirectoryInfoWrap"/> object representing the root of a path. 
        /// </summary>
        IDirectoryInfo Root { get; }

        /// <summary>
        /// Returns the subdirectories of the current directory.
        /// </summary>
        /// <returns>An array of <see cref="T:SystemInterface.IO.IDirectoryInfoWrap"/> objects. </returns>
        IDirectoryInfo[] GetDirectories();
    }
}