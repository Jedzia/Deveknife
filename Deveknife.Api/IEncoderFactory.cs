//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="IEncoderFactory.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.12.2013 18:21</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    public interface IEncoderFactory
    {
        IEncoder GetByName(string name);
    }
}