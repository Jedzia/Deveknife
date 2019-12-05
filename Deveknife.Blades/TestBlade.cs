// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestBlade.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>06.02.2013 14:50</date>
// <summary>
//   Defines the TestBlade type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades
{
    using System.Windows.Forms;

    using Deveknife.Api;

    public class TestBlade : IBlade
    {
        public TestBlade()
        {
            // MessageBox.Show("TestBlade created.");
        }

        public string Name
        {
            get
            {
                return "Only for Testing";
            }
        }

        public IHost Host { get; set; }

        public UserControl CreateControl()
        {
            return new VirtualDubUI();
        }
    }
}