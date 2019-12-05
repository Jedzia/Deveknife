// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceInstaller.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.11.2016 16:05</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Installers
{
    using SystemInterface.IO;

    using SystemWrapper.IO;

    using Deveknife.Api;

    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    /// <summary>
    /// Installs system wide dependencies for the application.
    /// </summary>
    public class ServiceInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the
        /// <see cref="Castle.Windsor.IWindsorContainer" /> .
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // System IO and Wrapper
            container.Register(Component.For<IFile>().ImplementedBy<FileWrap>());
            container.Register(Component.For<IPath>().ImplementedBy<PathWrap>());
            container.Register(Component.For<IDirectory>().ImplementedBy<DirectoryWrap>());
            container.Register(Component.For<IDirectoryInfoFactory>().ImplementedBy<DirectoryInfoFactory>());
            container.Register(Component.For<IFileInfoFactory>().ImplementedBy<FileInfoFactory>());

            container.Register(Component.For<IWrappedIOServiceFactory>().AsFactory());
            container.Register(Component.For<IOService>().ImplementedBy<FileIOService>());

            // Dialog wrappers: File / Folder / etc. 
            container.Register(Component.For<IFolderBrowserDialog>().ImplementedBy<FolderBrowserDialogWrap>());
            container.Register(Component.For<IDialogService>().AsFactory());

            // Settings
            container.Register(Component.For<ISettingsProvider>().ImplementedBy<SettingsProvider>());

            // Main Application API
            container.Register(Component.For<IHost>().ImplementedBy<Horst>());

            // Main Window, Startup.
            container.Register(Component.For<MainForm>().LifestyleSingleton());
        }
    }
}