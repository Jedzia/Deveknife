//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FileStarter.cs" company="EvePanix">Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>29.09.2014 13:04</date>
//  --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview.Util
{
    using System.Diagnostics;

    public class FileStarter
    {
        public static void Execute(string fullpath)
        {
            Process.Start(fullpath);
        }
    }
}