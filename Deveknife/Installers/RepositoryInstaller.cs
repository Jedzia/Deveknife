// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryInstaller.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>07.11.2015 23:17</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Installers
{
    using Deveknife.Api;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// Installs the data exchange providers for this application.
    /// </summary>
    public class RepositoryInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the
        /// <see cref="Castle.Windsor.IWindsorContainer" /> .
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
        }
    }
}