// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitDisplayItem.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2019, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2019 20:17</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.GitRegister
{
    /// <summary>
    /// Struct Deveknife.Blades.GitRegister.GitDisplayItem
    /// </summary>
    public struct GitDisplayItem
    {
        /*public bool Selected;

        public string Name;

        public string Path;

        public string Remote;*/

        public bool Selected { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Remote { get; set; }
    }
}