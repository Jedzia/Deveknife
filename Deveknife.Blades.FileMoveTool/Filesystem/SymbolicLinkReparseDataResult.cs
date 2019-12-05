// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SymbolicLinkReparseDataResult.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>29.11.2016 00:22</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    /// <summary>
    /// Represents the result while trying to retrieve a file system link.
    /// </summary>
    internal class SymbolicLinkReparseDataResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolicLinkReparseDataResult"/> class with valid data.
        /// </summary>
        /// <param name="reparseData">The reparse data.</param>
        /// <param name="isValid">if set to <c>true</c> the entry is valid.</param>
        public SymbolicLinkReparseDataResult(SymbolicLinkReparseData reparseData, bool isValid = true)
        {
            this.ReparseData = reparseData;
            this.IsValid = isValid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolicLinkReparseDataResult"/> class, marked as invalid entry.
        /// </summary>
        public SymbolicLinkReparseDataResult()
        {
            this.IsValid = false;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid { get; private set; }

        /// <summary>
        /// Gets the reparse data.
        /// </summary>
        /// <value>The reparse data.</value>
        public SymbolicLinkReparseData ReparseData { get; private set; }
    }
}