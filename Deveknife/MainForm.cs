// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="EvePanix">
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

    using Deveknife.Api;

    using Castle.Core.Logging;

    /// <summary>
    /// Main Application Window.
    /// </summary>
    public partial class MainForm : Form
    {
        // this is Castle.Core.Logging.ILogger, not log4net.Core.ILogger

        // private Entities ent;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="bladeFactory">The blade factory.</param>
        /// <param name="bladeToolFactory">The blade tool factory.</param>
        public MainForm(ILogger logger, IBladeFactory bladeFactory, IBladeToolFactory bladeToolFactory)
            : this()
        {
            Guard.NotNull(() => logger, logger);
            this.Logger = logger;
            Guard.NotNull(() => bladeFactory, bladeFactory);
            this.BladeFactory = bladeFactory;
            Guard.NotNull(() => bladeToolFactory, bladeToolFactory);
            this.BladeToolFactory = bladeToolFactory;
        }

        // public IVegetablesRepository VegetablesRepository { get; set; }
        // public IOrdersRepository OrdersRepository { get; set; }
        // public IUnitOfWorkFactory UnitOfWorkFactory { get; set; }
        // public IUnitOfWork UnitOfWork { get; set; }

        /*public MainForm(IVegetablesRepository vegetablesRepository, IOrdersRepository ordersRepository) : this()
        {
            this.VegetablesRepository = vegetablesRepository;
            this.OrdersRepository = ordersRepository;
        }*/

        /// <summary>
        /// Prevents a default instance of the <see cref="MainForm"/> class from being created.
        /// </summary>
        private MainForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the blade factory.
        /// </summary>
        /// <value>
        /// The blade factory.
        /// </value>
        public IBladeFactory BladeFactory { get; private set; }

        /// <summary>
        /// Gets the blade tool factory.
        /// </summary>
        /// <value>The blade tool factory.</value>
        public IBladeToolFactory BladeToolFactory { get; private set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; private set; }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void Button1Click(object sender, EventArgs e)
        {
            this.InitBlades();

            // MessageBox.Show("Bla");
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void Button2Click(object sender, EventArgs e)
        {
            var uc = new TestUserControl();
            uc.AllowDrop = true;
            var ntb = new TabPage("New Page");
            ntb.Controls.Add(uc);
            this.tabControl1.TabPages.Add(ntb);
            ntb.AllowDrop = true;
            ////ntb.DragEnter += this.Nob_DragEnter;
            ////uc.DragEnter += this.Nob_DragEnter;
        }

        /// <summary>
        /// TODO The form 1_ form closing.
        /// </summary>
        /// <param name="sender">TODO The sender.</param>
        /// <param name="e">TODO The e.</param>
        private void Form1FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (this.ent != null)
            {
                this.ent.SaveChanges(); 
            }*/
            //// var entfact = UnitOfWorkFactory;
            // var workunit = entfact.Create();
            // workunit.Save();

            // var workunit = UnitOfWork;
            //// workunit.Save();

            this.Logger.Info("------------------------------------------------------------------------------------");
            this.Logger.Info("                               Exiting Application.");
            this.Logger.Info("------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// Occurs when the form is loaded.
        /// </summary>
        /// <param name="sender">The <paramref name="sender" /> .</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void Form1Load(object sender, EventArgs e)
        {
            this.Logger.Info("------------------------------------------------------------------------------------");
            this.Logger.Info("                               Starting up.");
            this.Logger.Info("------------------------------------------------------------------------------------");

            this.textBox1.Text = "Mo3633oo";
            this.WindowState = FormWindowState.Maximized;
            this.InitBlades();
        }

        /// <summary>
        /// Initializes the blades.
        /// </summary>
        private void InitBlades()
        {
            try
            {
                var blades = this.BladeFactory.CreateAll();
                TabPage lasttab = null;
                foreach (var blade in blades)
                {
                    // var ntb = new TabPage(blade.GetType().Name);
                    var ntb = new TabPage(blade.Name);
                    ntb.AllowDrop = true;
                    var userControl = blade.CreateControl();
                    userControl.Dock = DockStyle.Fill;
                    ntb.Controls.Add(userControl);
                    this.tabControl1.TabPages.Add(ntb);
                    lasttab = ntb;
                }

                if (lasttab != null)
                {
                    this.tabControl1.SelectTab(lasttab);
                    
                    // ToDo: remove fixed index after testing !!!
                    this.tabControl1.SelectedIndex = 9;
                }
            }
            catch (Exception ex)
            {
                this.Logger.Info(ex.ToString());
                throw;
            }
        }
    }
}