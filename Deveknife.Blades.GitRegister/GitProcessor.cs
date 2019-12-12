// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitProcessor.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2019, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>12.12.2019 05:08</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.GitRegister
{
    using System.Collections.Generic;

    using Castle.Core.Logging;

    using Deveknife.Api;

    /// <summary>
    /// Example Class GitProcessor.
    /// </summary>
    [UsedImplicitly]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GitProcessor : IBladeTool
    {
        public GitProcessor(ILogger logger)
        {
            this.Logger = Guard.NotNull(() => logger, logger);
        }

        private ILogger Logger { get; set; }

        public void CopyFiles(IEnumerable<string> select)
        {
            Guard.NotNull(() => select, select);
            this.Logger.Info("GitProcessor CopyFiles");
        }

        public void DeleteFiles(IEnumerable<string> select)
        {
            Guard.NotNull(() => select, select);
            this.Logger.Info("GitProcessor DeleteFiles");
        }

        public void MoveFiles(IEnumerable<string> select)
        {
            Guard.NotNull(() => select, select);
            this.Logger.Info("GitProcessor MoveFiles");
        }

        public void RegisterWithRemoteGit(GitItem item)
        {
            Guard.NotNull(() => item, item);
            this.Logger.Info($"GitProcessor RegisterWithRemoteGit {item.Name}, {item.Path}, {item.Remote}");

            if(item.Remote == "<NOT PRESENT>")
            {
              this.Logger.Info($"    working on {item.Name}.");
            }
        }
    }
}