namespace Deveknife.Api
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides access to registered <see cref="IBlade" /> classes.
    /// </summary>
    public interface IBladeToolFactory
    {
        /// <summary>
        /// Creates all <see cref="IBladeTool" /> instances.
        /// </summary>
        /// <returns>
        /// the created IBlade's.
        /// </returns>
        T CreateTool<T>() where T : IBladeTool;
        T CreateTool<T>(object conn) where T : IBladeTool;

        IEnumerable<IBladeTool> CreateAll(object conn);

        T CreateTool<T>(string destinationPath, string userName, string password);
        T CreateTool<T>(string destinationPath, string userName, string password, Func<string> callbackHandler);
    }
}