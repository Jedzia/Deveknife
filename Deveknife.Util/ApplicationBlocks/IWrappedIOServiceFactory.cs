// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWrappedIOServiceFactory.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.11.2016 15:08</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife
{
    using SystemInterface.IO;

    /// <summary>
    /// Provides a meta factory that will create the wrapped SystemInterface.IO factories.
    /// </summary>
    /// <remarks>Here is the place to add other SystemInterface wrapper factories.</remarks>
    public interface IWrappedIOServiceFactory
    {
        /// <summary>
        /// Creates the directory information factory.
        /// </summary>
        /// <returns>an instance of the directory information factory.</returns>
        IDirectoryInfoFactory CreateDirectoryInfoFactory();

        /// <summary>
        /// Creates the file information factory.
        /// </summary>
        /// <returns>an instance of the file information factory.</returns>
        IFileInfoFactory CreateFileInfoFactory();
    }
}