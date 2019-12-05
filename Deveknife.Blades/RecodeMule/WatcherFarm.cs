//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WatcherFarm.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 11:45</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    using Deveknife.Api;

    using Castle.Core.Logging;

    /// <summary>
    /// Creates and manages FileSystemWatcher entities.
    /// </summary>
    public class WatcherFarm : IBladeComponentFactory
    {
        /// <summary>
        /// The <see cref="logger"/>
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The list of created watchers.
        /// </summary>
        private readonly Stack<WatcherFarmEntity> watchers = new Stack<WatcherFarmEntity>();
        private int started;

        /// <summary>
        /// Initializes a new instance of the <see cref="WatcherFarm" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public WatcherFarm(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has no watcher
        /// running.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has no watcher running; otherwise, 
        /// <c>false</c> .
        /// </value>
        public bool HasNoWatcherRunning
        {
            get
            {
                return this.started == 0 && this.watchers.Count == 0;
            }
        }

        /// <summary>
        /// Creates a FileSystemWatcher for the specified directory path and
        /// attach it to the specified event handler.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="watcherChanged">
        /// The event handler to attach the watcher to.
        /// </param>
        /// <returns>
        /// Action.
        /// </returns>
        public CancellationTokenSource Create(string directoryPath, EventHandler<FileSystemEventArgs> watcherChanged)
        {
            var watcher = new FileSystemWatcher(directoryPath);
            watcher.Changed += (sender, args) => watcherChanged(sender, args);
            watcher.Created += (sender, args) => watcherChanged(sender, args);
            watcher.EnableRaisingEvents = true;
            var watcherFarmEntity = new WatcherFarmEntity(watcher);
            this.watchers.Push(watcherFarmEntity);
            this.started++;
            this.logger.Info(string.Format("Starting FileSystemWatcher({1}) for '{0}'.", directoryPath, this.started));
            return watcherFarmEntity.CancellationTokenSource;
        }

        /// <summary>
        /// Destroys all <see cref="watchers"/> previously created.
        /// </summary>
        public void DestroyAll()
        {
            WatcherFarmEntity watcher;
            while (this.watchers.Count > 0)
            {
                watcher = this.watchers.Pop();
                this.logger.Info(string.Format("Disposing FileSystemWatcher for '{0}'.", watcher.FileSystemWatcher.Path));
                watcher.FileSystemWatcher.Dispose();
                
                watcher.CancellationTokenSource.Token.Register(this.Detach, watcher);
                watcher.CancellationTokenSource.Cancel();
            }
        }

        private void Callback(object o)
        {
            
        }

        /// <summary>
        /// Signals that a watcher thread has ended.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <remarks>For every <see cref="started" /> watcher, this has to be called once,
        /// when the program logic ends the duties <see cref="started" /> by the
        /// watcher. This is for long running threaded actions, to signal that
        /// all jobs are done.</remarks>
        private void Detach(object state)
        {
            var entity = (WatcherFarmEntity)state;
            this.logger.Info(string.Format("Detach FileSystemWatcher({0}): {1}.", this.started, entity.FileSystemWatcher.Path));
            this.started--;
            if (this.HasNoWatcherRunning)
            {
                this.logger.Info(string.Format("All FileSystemWatchers are detached, now."));
            }
        }

        /// <summary>
        /// Holds references to FileSystemWatchers. 
        /// </summary>
        internal class WatcherFarmEntity
        {
            /// <summary>
            /// Gets the file system watcher.
            /// </summary>
            /// <value>The file system watcher.</value>
            public FileSystemWatcher FileSystemWatcher { get; private set; }

            /// <summary>
            /// Gets the cancellation token source.
            /// </summary>
            /// <value>The cancellation token source.</value>
            public CancellationTokenSource CancellationTokenSource { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="WatcherFarmEntity" /> class.
            /// </summary>
            /// <param name="fileSystemWatcher">The file system watcher.</param>
            public WatcherFarmEntity(FileSystemWatcher fileSystemWatcher)
            {
                FileSystemWatcher = fileSystemWatcher;
                CancellationTokenSource = new CancellationTokenSource();
            }
        }
    }
}