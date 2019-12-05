// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>07.11.2015 23:17</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// TODO The program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // new Bootstrapper().Run();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Application.Run(new MainForm());
            Application.Run(new Bootstrapper().Run());
        }
    }
}