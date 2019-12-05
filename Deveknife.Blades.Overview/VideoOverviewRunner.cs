// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VideoOverviewRunner.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>25.02.2014 19:40</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using Deveknife.Api;

    using Castle.Core.Logging;

    public class VideoOverviewRunner : BladeRunner, IBlade
    {
        /// <summary>
        /// The User Interface
        /// </summary>
        public VideoOverviewRunner(IHost host, VideoOverviewUI ui)
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