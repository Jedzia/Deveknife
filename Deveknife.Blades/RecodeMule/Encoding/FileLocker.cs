// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileLocker.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>12.05.2013 11:56</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.RecodeMule.Encoding
{
    using System.IO;

    /// <summary>
    /// Implements a simple file locking/processed mechanism with ".done" files.
    /// </summary>
    internal class FileLocker
    {
        /// <summary>
        /// The extension for the lock file.
        /// </summary>
        private const string LockExtension = ".done";

        /// <summary>
        /// The filename of the lock file.
        /// </summary>
        private readonly string filename;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLocker" /> class.
        /// </summary>
        /// <param name="filename">The filename of the lock file.</param>
        public FileLocker(string filename)
        {
            this.filename = filename;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="LockFilename" /> file exists.
        /// </summary>
        /// <value><c>true</c> if exists; otherwise, <c>false</c>.</value>
        public bool Exists
        {
            get
            {
                return File.Exists(this.LockFilename);
            }
        }

        /// <summary>
        /// Gets the lock filename.
        /// </summary>
        /// <value>The lock filename.</value>
        public string LockFilename
        {
            get
            {
                // return Path.Combine(this.filename, LockExtension);
                return this.filename + LockExtension;
            }
        }

        /// <summary>
        /// Tests this instances lock file. If not present, the lockfile is created.
        /// </summary>
        /// <returns><c>true</c> if the lock file exists, <c>false</c> otherwise</returns>
        public bool Create()
        {
            var exists = File.Exists(this.LockFilename);
            if (!exists)
            {
                var stream = File.Create(this.LockFilename);
                stream.WriteByte(54);
                stream.Close();
            }

            return exists;
        }
    }
}