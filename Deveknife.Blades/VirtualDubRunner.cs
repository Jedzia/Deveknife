using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deveknife.Blades
{
    using System.Runtime.Remoting.Messaging;
    using System.Windows.Forms;

    using Deveknife.Api;

    public class VirtualDubRunner : IBlade
    {
        public VirtualDubRunner()
        {
            // MessageBox.Show("VirtualDubRunner created.");
        }

        public IHost Host { get; set; }

        public string Name
        {
            get
            {
                return "VirtualDub Runner";
            }
        }

        public UserControl CreateControl()
        {
            return new VirtualDubUI();
        }
    }
}
