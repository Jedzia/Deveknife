﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitDisplayItem.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2019, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>12.12.2019 11:49</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.GitRegister
{
    /// <summary>
    /// Struct Deveknife.Blades.GitRegister.GitDisplayItem
    /// </summary>
    public class GitDisplayItem
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string Remote { get; set; }

        public bool Selected { get; set; }

        public GitItem ToGitItem()
        {
            var result = new GitItem { Name = this.Name, Path = this.Path, Remote = this.Remote };
            return result;
        }
    }
}