// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileQueueProcessor.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>10.04.2014 03:21</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System.Collections.Generic;

    public class FileQueueProcessor
    {
        // implement an event system with listeners that can attach to the
        // progress of the queue.

        public void Copy(string path)
        {
            // always queue it up, longrunning operation.
        }

        public void CopyFiles(IEnumerable<string> files)
        {
        }

        public void Delete(string path)
        {
            // all local files can run on its own delete queue/thread.
        }

        public void DeleteFiles(IEnumerable<string> files)
        {
        }

        public void Move(string path)
        {
            // check if source == dest drive, then do a fast move
            // else queue it up
        }

        public void MoveFiles(IEnumerable<string> files)
        {
        }
    }
}