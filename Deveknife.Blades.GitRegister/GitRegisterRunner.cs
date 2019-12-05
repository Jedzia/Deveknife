// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitRegisterRunner.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 09:01</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.GitRegister
{
    using Deveknife.Api;

    using Castle.Core.Logging;

    /// <summary>
    /// Blade definition and startup initialization.
    /// </summary>
    public class GitRegisterRunner : BladeRunner, IBlade
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GitRegisterRunner"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="ui">The UI.</param>
        public GitRegisterRunner(IHost host, GitRegisterUI ui)
            : base(host, ui)
        {
            ui.Blade = this;
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /*
        /// <summary>
        /// Creates the User-Interface control.
        /// </summary>
        /// <returns>a UserControl with the User-Interface of the <see cref="IBlade" />.</returns>
        public UserControl CreateControl()
        {
            return this.ui;
        }
*/
    }
}