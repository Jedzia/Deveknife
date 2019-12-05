// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileIOService.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.11.2016 15:09</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife
{
    using System.IO;

    using SystemInterface.IO;

    /// <summary>
    /// Main File IO Service Wrapper.
    /// </summary>
    internal class FileIOService : IOService
    {
        private readonly IFile fileWrap;

        private readonly IWrappedIOServiceFactory wrappedIOServiceFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileIOService" /> class.
        /// </summary>
        /// <param name="fileWrap">The file wrap.</param>
        /// <param name="wrappedIOServiceFactory">The i wrapped io services.</param>
        public FileIOService(IFile fileWrap, IWrappedIOServiceFactory wrappedIOServiceFactory)
        {
            this.fileWrap = Guard.NotNull(() => fileWrap, fileWrap);
            this.wrappedIOServiceFactory = Guard.NotNull(() => wrappedIOServiceFactory, wrappedIOServiceFactory);
        }

        /// <summary>
        /// Gets the wrapped I/O service factory.
        /// </summary>
        /// <returns>the I/O service factory.</returns>
        public IWrappedIOServiceFactory GetWrappedIOServiceFactory()
        {
            return this.wrappedIOServiceFactory;
        }

        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>A stream to the selected file or <see langword="null" /> if the operation wasn't successful.</returns>
        public IStream OpenFile(string path)
        {
            return this.fileWrap.Open(path, FileMode.Open);
        }

        /// <summary>
        /// Creates or overwrites a file in the specified path.
        /// </summary>
        /// <param name="path">The path and name of the file to create.</param>
        /// <returns>A IFileStreamWrap that provides read/write access to the file specified in path.</returns>
        public IFileStream SaveFile(string path)
        {
            return this.fileWrap.Create(path);
        }

        /// <summary>
        /// Send file to recycle bin.  Display dialog, display warning if files are too big to fit (FOF_WANTNUKEWARNING)
        /// </summary>
        /// <param name="path">Location of directory or file to recycle</param>
        /// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
        public bool SendToRecycleBin(string path)
        {
            return FileOperationAPIWrapper.Send(path);
        }
    }
}