// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileStarter.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 05:04</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.BladeModuleTemplate.Util
{
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// An executable starter.
    /// </summary>
    public class FileStarter
    {
        /// <summary>
        /// Runs the Executable with the specified full file system path.
        /// </summary>
        /// <param name="fullpath">The full path to the executable.</param>
        /// <exception cref="System.ComponentModel.Win32Exception">An error occurred when opening the associated file. </exception>
        /// <exception cref="FileNotFoundException">The PATH environment variable has a string containing quotes.</exception>
        public static void Execute(string fullpath)
        {
            Process.Start(fullpath);
        }
    }
}