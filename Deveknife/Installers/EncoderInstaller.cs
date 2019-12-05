//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EncoderInstaller.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.12.2013 13:12</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Installers
{
    using System.Reflection;

    using Deveknife.Api;

    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class EncoderInstaller : IWindsorInstaller
    {
        #region Public Methods and Operators

        /// <summary>
        /// Performs the installation in the
        /// <see cref="Castle.Windsor.IWindsorContainer" /> .
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // The UnitOfWork-Factory way.
            container.Register(
                Types.FromAssemblyNamed("Deveknife.Blades").BasedOn(typeof(IEncoderProbe))
                    // Todo: Later, move this into a configuration class.
                     .Configure(e => e.DependsOn(new { ffprobePath = @"D:\Program Files\ffmpeg\bin\ffprobe.exe" })).LifestyleTransient());
            container.Register(
                Types.FromAssemblyNamed("Deveknife.Blades").BasedOn(typeof(IEncoder)) //.WithServiceBase()
                     .Configure(e => e.DependsOn(new { ffmpegPath = @"D:\Program Files\ffmpeg\bin\ffmpeg.exe" })).LifestyleTransient());

            container.Register(
                Component.For<ITypedFactoryComponentSelector>().ImplementedBy<EncoderTypedFactoryComponentSelector>());
            container.Register(
                Component.For<IEncoderFactory>()
                         .AsFactory(c => c.SelectedWith<EncoderTypedFactoryComponentSelector>())
                         .LifestyleTransient()
                         );
        }

        #endregion
    }

    public class EncoderTypedFactoryComponentSelector : DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            if (method.Name == "GetByName" && arguments.Length == 1 && arguments[0] is string)
            {
                var componentName = (string)arguments[0];
                if (!componentName.Contains("."))
                {
                    componentName = "Deveknife.Blades.RecodeMule.Encoding." + componentName;
                }
                return componentName;
            }
            return base.GetComponentName(method, arguments);
        }
    }
}