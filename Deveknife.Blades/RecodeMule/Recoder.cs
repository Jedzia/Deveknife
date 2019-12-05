// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Recoder.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>25.02.2014 17:53</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.RecodeMule
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Deveknife.Api;
    using Deveknife.Blades.RecodeMule.Encoding;

    using Castle.Core.Logging;

    /// <summary>
    /// Runs encoding jobs on new video files.
    /// </summary>
    public class Recoder : IBladeTool
    {
        private static readonly object EncodeLock = new object();

        private readonly IEncoderFactory encoderFactory;

        private readonly WatcherFarm farm;

        private readonly Dictionary<string, WatcherQueue> watcherQueues = new Dictionary<string, WatcherQueue>();

        // private readonly BlockingCollection<RecorderWorkItem> readyQueue = new BlockingCollection<RecorderWorkItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Recoder" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="farm">The FileSystemWatcher factory.</param>
        /// <param name="encoderFactory">The encoder producing factory.</param>
        public Recoder(ILogger logger, WatcherFarm farm, IEncoderFactory encoderFactory)
        {
            this.Logger = logger;
            this.farm = farm;
            this.encoderFactory = encoderFactory;
        }

        /// <summary>
        /// Occurs when a message is ready to be sent.
        /// </summary>
        public event EventHandler<RecoderMessage> Message;

        public bool IsRunning
        {
            get
            {
                return !this.farm.HasNoWatcherRunning;
            }
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; private set; }

        // public Control Host { get; set; }
        public static bool IsFileLocked(string filePath)
        {
            try
            {
                using (File.Open(filePath, FileMode.Open))
                {
                }
            }
            catch (IOException e)
            {
                var errorCode = Marshal.GetHRForException(e) & ((1 << 16) - 1);
                return errorCode == 32 || errorCode == 33;
            }

            return false;
        }

        public void Enqueue(string directoryPath)
        {
            var files = Directory.EnumerateFileSystemEntries(directoryPath);
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                if (fi.DirectoryName == null)
                {
                    return;
                }

                WatcherQueue watcherQueue;
                var contains = this.watcherQueues.TryGetValue(fi.DirectoryName, out watcherQueue);
                if (contains && (fi.Extension == ".ts" || fi.Extension == ".avi" || fi.Extension == ".mpg") && watcherQueue.All(el => fi.FullName != el.Filename))
                {
                    watcherQueue.Add(new RecorderWorkItem(fi.FullName) { Overwrite = true });
                }
            }
        }

        // WatcherQueue watcherQueue = new WatcherQueue();

        // locks the encoder. run only one encoding at a time.

        /// <summary>
        /// Starts watching at the specified directory path.
        /// </summary>
        /// <param name="directoryPath">The directory path to watch for files to encode.</param>
        /// <param name="outfilter">The filter that creates the output file name.</param>
        public void Start(string directoryPath, ITextFilter outfilter)
        {
            // need a queue for each dir with different encode parameters
            // var wq = this.watcherQueue;
            var wq = new WatcherQueue(this.Logger);
            wq.QueueChanged += this.wq_QueueChanged;
            this.watcherQueues.Add(directoryPath, wq);

            var cts = this.farm.Create(directoryPath, wq.WatcherChanged/*, wq.QueueChanged*/);
            var pp = new ParameterParser(directoryPath);

            Task.Factory.StartNew(
                obj =>
                {
                    var token = (CancellationToken)obj;

                    // Todo: use the message system
                    // this.OnMessage(new RecoderMessage("Bla"));
                    while (!wq.IsCompleted)
                    {
                        // shorter: while (!wq.ReadyQueue.TryTake(out item, 400, token)) { }
                        RecorderWorkItem item;
                        while (!wq.TryTake(out item) && !token.IsCancellationRequested)
                        {
                            Thread.Sleep(400 + (DateTime.Now.Millisecond % 100));
                        }

                        var currentItem = item;
                        if (token.IsCancellationRequested)
                        {
                            var msg2 = string.Format(
                                "Task canceled. readyQueue.Count={0}, item={1}", 
                                wq.Count, 
                                directoryPath);
                            this.Logger.Warn(msg2);
                            OnEncoderChanged(new RecoderStateChangedEventArgs(RecoderStateChangedAction.Cancelled, directoryPath));

                            // Perform cleanup if necessary.

                            // ...

                            // Terminate the operation.
                            break;
                        }

                        /*var fi = new FileInfo(currentItem.Filename);
                            if (fi.DirectoryName != directoryPath)
                            {
                                wq.ReadyQueue.Add(currentItem);
                                Thread.Sleep(2000 - (new Random().Next(500)));
                                continue;
                            }*/
                        var msg3 = string.Format(
                            "Running Task for '{1}' item. readyQueue.Count={0}", 
                            wq.Count, 
                            currentItem.Filename);
                        this.Logger.Warn(msg3);
                        try
                        {
                            lock (EncodeLock)
                            {
                                var success = this.RunEncoding(
                                    currentItem.Filename, 
                                    currentItem.Overwrite, 
                                    outfilter, 
                                    pp);
                                this.AfterEncoding(success, currentItem.Filename, currentItem.Overwrite, outfilter, pp);
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Logger.Error(ex.ToString);
                        }
                    }

                    // confirmExit();
                    // this.IsRunning = false;
                }, 
                cts.Token);
        }

        /// <summary>
        /// Handles the QueueChanged event of the wq control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Recoder.WatcherQueueChangedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void wq_QueueChanged(object sender, Recoder.WatcherQueueChangedEventArgs e)
        {
            switch (e.Action)
            {
                case WatcherQueueChangedAction.Add:
                    //string message = "WQ: ";
                    //this.OnMessage(new RecoderMessage(message));
                    OnEncoderChanged(new RecoderStateChangedEventArgs(RecoderStateChangedAction.Enqueued, e.Item.Filename));
                    break;
                case WatcherQueueChangedAction.Remove:
                    OnEncoderChanged(new RecoderStateChangedEventArgs(RecoderStateChangedAction.Dequeued, e.Item.Filename));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Stops all activity.
        /// </summary>
        public void Stop()
        {
            this.Logger.Info("Stopping FileSystemWatcher.");
            this.farm.DestroyAll();
            this.watcherQueues.Clear();
        }

        /// <summary>
        /// Called when a message is ready to be sent.
        /// </summary>
        /// <param name="e">The event parameter.</param>
        protected virtual void OnMessage(RecoderMessage e)
        {
            var handler = this.Message;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void AfterEncoding(bool success, string filename, bool overwrite, ITextFilter outfilter, ParameterParser pp)
        {
            if (success)
            {
                OnEncoderChanged(new RecoderStateChangedEventArgs(RecoderStateChangedAction.Successful, filename));
                this.CopyFileWithExtension(filename, overwrite, outfilter, ".eit");
                this.CopyFilesWithExtension(filename, overwrite, outfilter, ".srt");
            }
            else
            {
                var message = string.Format("No success encoding '{0}'.", filename);
                this.Logger.Warn(message);
                OnEncoderChanged(new RecoderStateChangedEventArgs(RecoderStateChangedAction.UnSuccessful, filename));
            }
        }

        private void CopyFileWithExtension(string filename, bool overwrite, ITextFilter outfilter, string extension)
        {
            var copyFile = Path.ChangeExtension(filename, extension);
            var outFile = outfilter.Apply(Path.ChangeExtension(filename, extension));
            if (File.Exists(copyFile) && (!overwrite || !File.Exists(outFile)))
            {
                var message = string.Format("Copy '{0}' to '{1}'.", copyFile, outFile);
                this.Logger.Info(message);
                File.Copy(copyFile, outFile);
            }
        }

        private void CopyFilesWithExtension(string filename, bool overwrite, ITextFilter outfilter, string extension)
        {
            var fi = new FileInfo(filename);
            var searchPattern = "*" + Path.GetFileNameWithoutExtension(filename) + "*" + extension;
            var files = Directory.EnumerateFileSystemEntries(
                fi.DirectoryName, 
                searchPattern, 
                SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                var copyFile = file;
                var outFile = outfilter.Apply(file);
                if (File.Exists(copyFile) && (!overwrite || !File.Exists(outFile)))
                {
                    this.Logger.Info("Copy '" + copyFile + "' to '" + outFile + "'.");
                    File.Copy(copyFile, outFile);
                }
            }
        }

        private void FfmpegEncoderOnStatusChanged(object sender, StatusData statusData)
        {
        }

        private bool RunEncoding(string videoInPath, bool overwrite, ITextFilter outfilter, ParameterParser pp)
        {
            // var develOverwrite = true;
            var develOverwrite = overwrite;
            var msg = string.Format("RunEncoding='{0}'", videoInPath);
            this.Logger.Info(msg);

            var lockFile = new FileLocker(videoInPath);
            if ((!develOverwrite) && lockFile.Exists)
            {
                msg = string.Format("lock file for '{0}' exists, aborting.", videoInPath);
                this.Logger.Info(msg);
                return false;
            }

            var enfile = new EnigmaFile(videoInPath);
            pp.Fetch();

            // Todo: determine encoderParameters for each file.
            // Encoder encoder = new Mencoder(s => Logger.Info(s));
            // Encoder encoder = new FFmpeg(this.Logger, @"D:\Program Files\ffmpeg\bin\ffmpeg.exe");
            // var encoderParameters = configLoader.CheckEncodeParameters(videoInPath);
            // var encoderParameters = new EncoderParameters(1260, 128) { DevelOverwrite = develOverwrite, TwoPass = true };
            var encoderParameters = new EncoderParameters(pp.VideoBitrate, pp.AudioBitrate)
                                    {
                                        Preset = pp.Preset, 
                                        VideoOptions =
                                            pp.VideoOptions, 
                                        AudioOptions =
                                            pp.AudioOptions, 
                                        DevelOverwrite =
                                            develOverwrite, 
                                        TwoPass = pp.Passes == 2
                                    };
            if (!string.IsNullOrEmpty(pp.FrameRate))
            {
                float frameRate;
                var success = float.TryParse(pp.FrameRate, out frameRate);
                if (success)
                {
                    encoderParameters.FrameRate = frameRate;
                }
            }

            // ITextFilter outfilter = new FixedPathFilter();
            var encoder = this.encoderFactory.GetByName(pp.Encoder);
            var ffmpegEncoder = encoder as FFmpeg;
            if (ffmpegEncoder != null)
            {
                // ffmpegEncoder.StatusChanged+=FfmpegEncoderOnStatusChanged;
            }

            var process = encoder.Encode(videoInPath, encoderParameters, outfilter);
            if (process != null)
            {
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    msg = string.Format("Encoder exited with error code: '{0}'", process.ExitCode);
                    this.Logger.Error(msg);

                    // if(process.StartInfo.RedirectStandardError)
                    // this.Logger.Error(process.StandardError.ReadToEnd());
                    Thread.Sleep(1000);
                    return false;
                }

                lockFile.Create();
                msg = string.Format("Done Encoding '{0}'", videoInPath);
                this.Logger.Info(msg);
            }

            // lockFile.Erase();
            return true;
        }

        /// <summary>
        /// Holds references to items in work.
        /// </summary>
        internal class RecorderWorkItem
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RecorderWorkItem" /> class.
            /// </summary>
            /// <param name="filename">The filename.</param>
            public RecorderWorkItem(string filename)
            {
                this.Filename = filename;
            }

            /// <summary>
            /// Gets the filename.
            /// </summary>
            /// <value>The filename.</value>
            public string Filename { get; private set; }

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="RecorderWorkItem" /> is allowed to overwrite the output file.
            /// </summary>
            /// <value><c>true</c> if overwrite is allowed; otherwise, <c>false</c>.</value>
            public bool Overwrite { get; set; }
        }

        public enum WatcherQueueChangedAction
        {
            Add,
            Remove/*,
            Replace,
            Move,
            Reset,*/
        }

        internal class WatcherQueueChangedEventArgs : EventArgs
        {
            public WatcherQueueChangedEventArgs(WatcherQueueChangedAction action)
                : this(action, null)
            {
            }

            public WatcherQueueChangedEventArgs(WatcherQueueChangedAction action, RecorderWorkItem item)
            {
                this.Item = item;
                this.Action = action;
            }

            public RecorderWorkItem Item { get; private set; }

            public WatcherQueueChangedAction Action { get; private set; }
        }

        private class WatcherQueue : IEnumerable<RecorderWorkItem>, INotifyWatcherQueueChanged
        {
            public event EventHandler<WatcherQueueChangedEventArgs> QueueChanged;

            protected virtual void OnQueueChanged(WatcherQueueChangedEventArgs e)
            {
                var handler = this.QueueChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }

            private readonly Dictionary<string, bool> checkMap = new Dictionary<string, bool>();

            private readonly BlockingCollection<RecorderWorkItem> readyQueue =
                new BlockingCollection<RecorderWorkItem>();

            public WatcherQueue(ILogger logger)
            {
                this.Logger = logger;
            }

            public int Count
            {
                get
                {
                    return this.ReadyQueue.Count;
                }
            }

            public bool IsCompleted
            {
                get
                {
                    return this.ReadyQueue.IsCompleted;
                }
            }

            private ILogger Logger { get; set; }

            private BlockingCollection<RecorderWorkItem> ReadyQueue
            {
                get
                {
                    return this.readyQueue;
                }
            }

            /// <summary>
            /// Adds the specified item.
            /// </summary>
            /// <param name="item">The item.</param>
            public void Add(RecorderWorkItem item)
            {
                this.ReadyQueue.Add(item);
                OnQueueChanged(new WatcherQueueChangedEventArgs(WatcherQueueChangedAction.Add, item));
            }

            public void Add(RecorderWorkItem item, CancellationToken cancellationToken)
            {
                this.ReadyQueue.Add(item, cancellationToken);
                OnQueueChanged(new WatcherQueueChangedEventArgs(WatcherQueueChangedAction.Add, item));
            }

            public bool TryTake(out RecorderWorkItem item)
            {
                var success = this.ReadyQueue.TryTake(out item);
                if (success)
                {
                    OnQueueChanged(new WatcherQueueChangedEventArgs(WatcherQueueChangedAction.Remove, item));
                }
                return success;
            }

            public bool TryTake(out RecorderWorkItem item, TimeSpan timeout)
            {
                var success = this.ReadyQueue.TryTake(out item, timeout);
                if (success)
                {
                    OnQueueChanged(new WatcherQueueChangedEventArgs(WatcherQueueChangedAction.Remove, item));
                }
                return success;
            }

            public bool TryTake(out RecorderWorkItem item, int millisecondsTimeout, CancellationToken cancellationToken)
            {
                var success = this.ReadyQueue.TryTake(out item, millisecondsTimeout, cancellationToken);
                if (success)
                {
                    OnQueueChanged(new WatcherQueueChangedEventArgs(WatcherQueueChangedAction.Remove, item));
                }
                return success;
            }

            public bool TryTake(out RecorderWorkItem item, int millisecondsTimeout)
            {
                var success = this.ReadyQueue.TryTake(out item, millisecondsTimeout);
                if (success)
                {
                    OnQueueChanged(new WatcherQueueChangedEventArgs(WatcherQueueChangedAction.Remove, item));
                }
                return success;
            }

            /// <summary>
            /// Handles the Changed and Create events of monitored file system 
            /// paths.
            /// </summary>
            /// <param name="sender">The source of the event.</param>
            /// <param name="e">
            /// The <see cref="System.IO.FileSystemEventArgs" /> instance containing
            /// the event data.
            /// </param>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// <c>e</c> is out of range. Unknown Enumeration of WatcherChangeTypes.
            /// </exception>
            public void WatcherChanged(object sender, FileSystemEventArgs e)
            {
                var fullPath = e.FullPath;
                var fi = new FileInfo(fullPath);
                if (fi.Extension != ".ts" && fi.Extension != ".avi" && fi.Extension != ".mpg")
                {
                    return;
                }

                var watcherChangeTypes = e.ChangeType;
                var name = e.Name;

                var isFileLocked = IsFileLocked(fullPath);
                this.LogWatcherFile(fullPath, watcherChangeTypes, name, fi, isFileLocked);

                switch (watcherChangeTypes)
                {
                    case WatcherChangeTypes.Deleted:
                        break;
                    case WatcherChangeTypes.Created:
                    case WatcherChangeTypes.Changed:

                        bool val;
                        var isInCheckMap = this.checkMap.TryGetValue(fullPath, out val);
                        if (isInCheckMap)
                        {
                            break;
                        }

                        this.checkMap.Add(fullPath, false);
                        new DeferedAction(
                            1000, 
                            () =>
                            {
                                isFileLocked = IsFileLocked(fullPath);
                                this.LogWatcherFile(fullPath, watcherChangeTypes, name, fi, isFileLocked, "{Defered}");
                                this.checkMap.Remove(fullPath);
                                if (!isFileLocked)
                                {
                                    this.Add(new RecorderWorkItem(fullPath));

                                    // this.readyQueue.CompleteAdding();
                                }

                                var msg2 = string.Format("readyQueue.Count={0}", this.ReadyQueue.Count);
                                this.Logger.Info(msg2);
                            });
                        break;
                    case WatcherChangeTypes.Renamed:
                        break;
                    case WatcherChangeTypes.All:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("e", "Unknown Enumeration of WatcherChangeTypes.");
                }

                var msg = string.Format("readyQueue.Count={0}", this.ReadyQueue.Count);
                this.Logger.Info(msg);
            }

            IEnumerator<RecorderWorkItem> IEnumerable<RecorderWorkItem>.GetEnumerator()
            {
                return this.ReadyQueue.ToList().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.ReadyQueue.ToList().GetEnumerator();

                // return ((IEnumerable)this.readyQueue).GetEnumerator();
            }

            private void LogWatcherFile(
                string fullPath, 
                WatcherChangeTypes watcherChangeTypes, 
                string name, 
                FileInfo fi, 
                bool isFileLocked, 
                string optional = "")
            {
                var lockTxt = isFileLocked ? "locked" : "not locked";
                var msg = string.Format(
                    "[{0}]({6}) {1} - {2} - {3} {4} {5}", 
                    watcherChangeTypes, 
                    name, 
                    fi.LastWriteTime, 
                    fi.CreationTime, 
                    lockTxt, 
                    optional, 
                    this.ReadyQueue.Count);
                this.Logger.Info(msg);
            }
        }

        public event EventHandler<RecoderStateChangedEventArgs> EncoderChanged;

        protected virtual void OnEncoderChanged(RecoderStateChangedEventArgs e)
        {
            var handler = this.EncoderChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public enum RecoderStateChangedAction
    {
        Enqueued,
        Dequeued,
        Cancelled,
        Successful,
        UnSuccessful
    }

    public class RecoderStateChangedEventArgs : EventArgs
    {
        public RecoderStateChangedEventArgs(RecoderStateChangedAction action)
            : this(action, null)
        {
        }

        public RecoderStateChangedEventArgs(RecoderStateChangedAction action, string item)
        {
            this.Item = item;
            this.Action = action;
        }

        public string Item { get; private set; }

        public RecoderStateChangedAction Action { get; private set; }
    }

}