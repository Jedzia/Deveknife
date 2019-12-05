// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileQueueProcessor.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 09:23</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.BladeModuleTemplate
{
    using System.Collections.Generic;

    /// <summary>
    /// Example File Queue Processor.
    /// </summary>
    public class FileQueueProcessor
    {
        /// <summary>
        /// Copies the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Copy(string path)
        {
            // implement an event system with listeners that can attach to the
            // progress of the queue.

            // always queue it up, longrunning operation.
        }

        /// <summary>
        /// Copies the files.
        /// </summary>
        /// <param name="files">The files.</param>
        public void CopyFiles(IEnumerable<string> files)
        {
        }

        /// <summary>
        /// Deletes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Delete(string path)
        {
            // all local files can run on its own delete queue/thread.
        }

        /// <summary>
        /// Deletes the files.
        /// </summary>
        /// <param name="files">The files.</param>
        public void DeleteFiles(IEnumerable<string> files)
        {
        }

        /// <summary>
        /// Moves the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Move(string path)
        {
            // check if source == dest drive, then do a fast move
            // else queue it up
        }

        /// <summary>
        /// Moves the files.
        /// </summary>
        /// <param name="files">The files.</param>
        public void MoveFiles(IEnumerable<string> files)
        {
        }
    }
}