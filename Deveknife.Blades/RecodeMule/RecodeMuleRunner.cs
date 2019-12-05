// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecodeMuleRunner.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>25.02.2014 19:41</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.RecodeMule
{
    using System.Windows.Forms;

    using Deveknife.Api;

    using Castle.Core.Logging;

    /// <summary>
    /// Entry point for the RecodeMule <see cref="Recoder" /> IBlade.
    /// </summary>
    //[BladeSettings(Icon = Image.FromStream())]
    public class RecodeMuleRunner : IBlade
    {
        // Todo: use the BladeRunner implementation.

        /*/// <summary>
        /// </summary>
        /// <value>The host.</value>
        public IHost Host { get; private set; }*/
        private readonly IHost host;

        /// <summary>
        /// The User Interface
        /// </summary>
        private readonly RecoderUI ui;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecodeMuleRunner" /> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="ui">The User Interface.</param>
        public RecodeMuleRunner(IHost host, RecoderUI ui)
        {
            Guard.NotNull(() => host, host);
            this.host = host;
            Guard.NotNull(() => ui, ui);
            this.ui = ui;
            ui.Blade = this;
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets the name of the <see cref="IBlade" />.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                // return "Recode Mule";
                return this.ui.BladeName;
            }
        }

        /// <summary>
        /// Creates the User-Interface control.
        /// </summary>
        /// <returns>a UserControl with the User-Interface of the <see cref="IBlade" />.</returns>
        public UserControl CreateControl()
        {
            return this.ui;
        }
    }
}