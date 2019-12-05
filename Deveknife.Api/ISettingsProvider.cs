// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingsProvider.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2019, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2019 17:21</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    public interface ISettingsProvider
    {
        string GetTextSetting(string name);
        string this[string name] { get; /*set;*/ }
    }

}