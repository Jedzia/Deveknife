// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BladesInstaller.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>21.10.2016 20:05</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileMoveTool.Installers
{
    using Deveknife.Api;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// Installs the <see cref="Deveknife.Api.IBlade" /> and other tools from
    /// the 'Deveknife.Blades' Assembly.
    /// </summary>
    public class BladesInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the
        /// <see cref="Castle.Windsor.IWindsorContainer" /> .
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Types.FromAssemblyContaining<BladesInstaller>().BasedOn(typeof(IBladeComponentFactory)).LifestyleTransient());

            // container.Register(Types.FromAssemblyNamed("Deveknife.Blades.FileMoveTool").BasedOn(typeof(IBladeTool)).WithServiceAllInterfaces().LifestyleTransient());
            container.Register(Types.FromAssemblyContaining<BladesInstaller>().BasedOn(typeof(IBladeTool)).LifestyleTransient());
            container.Register(Types.FromAssemblyContaining<BladesInstaller>().BasedOn(typeof(IBladeUI)).LifestyleTransient());
            container.Register(
                Types.FromAssemblyContaining<BladesInstaller>().BasedOn(typeof(IBlade)).WithServiceBase().LifestyleTransient());

            // Todo: gimme an IBLade here that checks for duplicate files

            // http://docs.castleproject.org/Default.aspx?Page=Typed-Factory-Facility-interface-based-factories&NS=Windsor&AspxAutoDetectCookieSupport=1
            // container.Register(Component.For<IBladeFactory>().AsFactory().LifestyleTransient());
            // container.Register(Component.For<IBladeToolFactory>().AsFactory().LifestyleTransient());
        }
    }
}