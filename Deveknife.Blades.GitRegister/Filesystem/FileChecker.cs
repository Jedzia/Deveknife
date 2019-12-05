// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileChecker.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2019, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2019 22:59</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using SystemInterface.IO;

    using Castle.Core.Logging;

    /// <summary>
    /// Filesystem utility.
    /// </summary>
    public class FileChecker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileChecker" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="directoryInfoFactory">The directory information factory.</param>
        /// <param name="fileInfoFactory">The file information factory.</param>
        public FileChecker([NotNull] ILogger logger, [NotNull] IDirectoryInfoFactory directoryInfoFactory, IFileInfoFactory fileInfoFactory)
        {
            this.FileInfoFactory = fileInfoFactory;
            this.Logger = Guard.NotNull(() => logger, logger);
            this.DirectoryInfoFactory = Guard.NotNull(() => directoryInfoFactory, directoryInfoFactory);
            this.FileInfoFactory = Guard.NotNull(() => fileInfoFactory, fileInfoFactory);
        }

        /// <summary>
        /// Gets the directory information factory.
        /// </summary>
        /// <value>The directory information factory.</value>
        [NotNull]
        public IDirectoryInfoFactory DirectoryInfoFactory { get; private set; }

        /// <summary>
        /// Gets the file information factory.
        /// </summary>
        /// <value>The file information factory.</value>
        [NotNull]
        public IFileInfoFactory FileInfoFactory { get; private set; }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        [NotNull]
        public ILogger Logger { get; private set; }

        /// <summary>
        /// Compares the content of two folders.
        /// </summary>
        /// <param name="pathA">The first path for comparison.</param>
        public void CompareFolders([CanBeNull] string pathA, Func<string, IDirectoryInfo, bool> repositoryFoundCallback)
        {
            this.Logger.Info($"<Exec-Compare> Folder '{pathA}'.");

            if(pathA == null)
            {
                this.Logger.Warn("First compare Folder specified has a null value.");
                return;
            }

            var dirA = this.DirectoryInfoFactory.Create(pathA);

            if(!dirA.Exists)
            {
                var message = $"First compare Folder '{pathA}' does not exist.";
                this.Logger.Warn(message);
                return;
            }

            var filterFunc = new Func<IFileInfo, bool>(info => info.FullName.EndsWith("\\.git\\config"));

            //var result1 = SearchAccessibleFiles(dirA, "\\.git\\config",
            var result1 = this.SearchForDirectories(
                dirA,
                "config",
                (info, b) =>
                {
                    var dir = info.Directory.Parent.FullName;
                    var isValid = filterFunc(info);
                    if(isValid)
                    {
                        this.Logger.Info($"    Git-Config-File: {info.FullName}");
                        var result = repositoryFoundCallback(dir, info.Directory.Parent);
                    }
                    else
                    {
                        this.Logger.Debug($"    Skipped Config : {info.FullName}");
                    }

                    Application.DoEvents();
                    return new Tuple<bool, IDirectoryInfo>(isValid, info.Directory.Parent);

                    ;
                });

            return;

            var fileListA = dirA.GetFiles("*.*", SearchOption.AllDirectories);

            var fileNameListA = fileListA.Select(info => info.Name);

            // var hashFunc = new Func<IFileInfo, int>(
            // (IFileInfo info) =>
            // {
            // return new VariableFileHash(info, fileInfo => fileInfo.Length.GetHashCode());
            // });

            // filtFunc: filters the files to check, hashFunc: determines which parameters to check
            ////var hashFunc = new Func<IFileInfo, int>(info => info.Length.GetHashCode());
            ////var filtFunc = new Func<IFileInfo, bool>(info => !info.FullName.Contains(".git"));
            ////var filtFunc = new Func<IFileInfo, bool>(info => info.FullName.Contains(".git") && (info.Length > 16 * 1024));
            var hashFunc = new Func<IFileInfo, int>(info => info.Name.GetHashCode() + info.Length.GetHashCode());

            var fileInfoListA = fileListA.Where(filterFunc).Select(info => new VariableFileHash(info, hashFunc)).ToList();

            ////var fileInfoListA = fileListA.Where(filtFunc).Select(info => new VariableFileHash(info, fileInfo => hashFunc(fileInfo))).ToList();
            ////var fileInfoListA = fileListA.Where(info => !info.FullName.Contains(".git")).Select(info => new VariableFileHash(info, fileInfo => FileChecker.Bla(fileInfo))).ToList();
            ////var fileInfoListA = fileListA.Where(info => !info.FullName.Contains(".git")).Select(info => FileChecker.GetVariableFileHash(info)).ToList();

            /*foreach(var fileInfo in fileListA)
            {
                if (fileListB.Contains(fileInfo))
                {
                    
                }
            }*/
            fileInfoListA.ForEach(
                hash =>
                {
                    this.Logger.Info($"    Git-Config-File: {hash.FullName}");
                    //var dir = hash.FullName;
                    var dir = hash.FileInfo.Directory.Parent.FullName;
                    var result = repositoryFoundCallback(dir, hash.FileInfo.Directory.Parent);
                });

            var cnt = 0;
            /*foreach(var file in fileInfoListA)
            {

                    ////var fileMD5 = this.BuildFileHash(file.FullName);
                    this.Logger.Info($"File: '{file.FullName}':");

                    Application.DoEvents();
                    cnt++;

                if(cnt > 200)
                {
                    return;
                }
            }*/
        }

        IEnumerable<IFileInfo> SearchAccessibleFiles(
            [NotNull] IDirectoryInfo root,
            [NotNull] string searchTerm,
            [NotNull] Func<IFileInfo, bool, bool> hook)
        {
            var files = new List<IFileInfo>();

            foreach(var file in root.GetFiles(searchTerm, SearchOption.TopDirectoryOnly))
            {
                if(hook(file, false))
                {
                    files.Add(file);
                }
            }

            foreach(var subDir in root.GetDirectories())
            {
                try
                {
                    files.AddRange(this.SearchAccessibleFiles(subDir, searchTerm, hook));
                }
                catch(UnauthorizedAccessException ex)
                {
                    // ...
                }
            }

            return files;
        }

        IEnumerable<IDirectoryInfo> SearchForDirectories(
            [NotNull] IDirectoryInfo root,
            [NotNull]  string searchTerm,
            [NotNull]  Func<IFileInfo, bool, Tuple<bool, IDirectoryInfo>> hook)
        {
            var files = new List<IDirectoryInfo>();

            foreach(var file in root.GetFiles(searchTerm, SearchOption.TopDirectoryOnly))
            {
                var result = hook(file, false);
                if(result.Item1)
                {
                    files.Add(result.Item2);
                }
            }

            foreach(var subDir in root.GetDirectories())
            {
                try
                {
                    files.AddRange(this.SearchForDirectories(subDir, searchTerm, hook));
                }
                catch(UnauthorizedAccessException ex)
                {
                    this.Logger.Error($"Unauthorized Access Exception while traversing directory '{subDir.FullName}': {ex.Message}", ex);
                }
                catch(ArgumentNullException ex)
                {
                    this.Logger.Error($"Argument NullException while traversing directory '{subDir.FullName}': {ex.Message}", ex);
                }
            }

            return files;
        }
    }
}