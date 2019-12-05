// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>07.11.2015 23:20</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife
{
    using System;
    using System.Windows.Forms;

    using Castle.Facilities.Logging;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    /// <summary>
    /// The startup bootstrapper.
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// TODO The container.
        /// </summary>
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper" /> class.
        /// </summary>
        public Bootstrapper()
        {
            try
            {
                this.container = new WindsorContainer();
                this.container.Kernel.Resolver.AddSubResolver(new CollectionResolver(this.container.Kernel, false));
                this.container.AddFacility<TypedFactoryFacility>();
                this.container.AddFacility<LoggingFacility>(f => f.UseNLog());

                //// Types.FromAssemblyInThisApplication() ?
                ////this.container.Install(FromAssembly.Containing<Bootstrapper>());
                ////this.container.Install(FromAssembly.Named("Deveknife.Blades.Overview"));
                ////this.container.Install(FromAssembly.InThisApplication());

                ////this.container.Install(FromAssembly.InDirectory(new AssemblyFilter(Path.GetDirectoryName(Application.ExecutablePath))));
                this.container.Install(FromAssembly.InDirectory(new AssemblyFilter(string.Empty, "Deveknife*")));

                ////var eee = container.Resolve<IBladeTest>();
                ////var encoderFactory = container.Resolve<IEncoderFactory>();
                ////var encoder = encoderFactory.GetByName("Deveknife.Blades.RecodeMule.Encoding.FFmpeg");
                ////var encoder = encoderFactory.GetByName("Mencoder");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Gets the main application object.
        /// </summary>
        /// <returns>
        /// the Windows Form <see cref="MainForm" /> .
        /// </returns>
        public MainForm Run()
        {
            return this.container.Resolve<MainForm>();
        }
    }
}