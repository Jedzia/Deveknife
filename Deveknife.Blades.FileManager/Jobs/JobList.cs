// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobList.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>31.07.2015 11:46</date>
// <summary>
//   Defines the JobList type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides information to a list of Jobs.
    /// </summary>
    public class JobList : List<Job>
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public Guid Key { get; set; }

        /// <summary>
        /// Gets or sets the parent job.
        /// </summary>
        /// <value>The parent job.</value>
        public Guid ParentJob { get; set; }
    }
}