// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BladeSettings.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>26.02.2014 00:23</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Api
{
    using System;
    using System.Drawing;

    public class BladeSettings : Attribute
    {
        public Image Icon { get; set; }
    }
}