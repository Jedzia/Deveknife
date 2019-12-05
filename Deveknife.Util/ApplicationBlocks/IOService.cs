// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOService.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.11.2016 14:39</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife
{
    using SystemInterface.IO;

    /// <summary>
    /// Implements File-IO capabilities. 
    /// </summary>
    public interface IOService
    {
        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>
        /// A stream to the selected file or <see langword="null"/> if the operation wasn't successful.
        /// </returns>
        IStream OpenFile(string path);

        //// Other similar untestable IO operations

        /// <summary>
        /// Creates or overwrites a file in the specified path.
        /// </summary>
        /// <param name="path">The path and name of the file to create.</param>
        /// <returns>A IFileStreamWrap that provides read/write access to the file specified in path.</returns>
        IFileStream SaveFile(string path);

        /// <summary>
        /// Send file to recycle bin.  Display dialog, display warning if files are too big to fit (FOF_WANTNUKEWARNING)
        /// </summary>
        /// <param name="path">Location of directory or file to recycle</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        bool SendToRecycleBin(string path);

        /// <summary>
        /// Gets the wrapped I/O service factory.
        /// </summary>
        /// <returns>the I/O service factory.</returns>
        IWrappedIOServiceFactory GetWrappedIOServiceFactory();
    }
}