// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileChecker.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>29.11.2016 14:03</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
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
        /// <param name="pathB">The second path for comparison.</param>
        public void CompareFolders([CanBeNull] string pathA, [CanBeNull] string pathB)
        {
            var todoMessage = string.Format("<Exec-Compare> Folder '{0}' against Folder '{1}'.", pathA, pathB);
            this.Logger.Info(todoMessage);

            if(pathA == null)
            {
                this.Logger.Warn("First compare Folder specified has a null value.");
                return;
            }

            var dirA = this.DirectoryInfoFactory.Create(pathA);

            if(!dirA.Exists)
            {
                var message = string.Format("First compare Folder '{0}' does not exist.", pathA);
                this.Logger.Warn(message);
                return;
            }

            if(pathB == null)
            {
                this.Logger.Warn("Second compare Folder specified has a null value.");
                return;
            }

            var dirB = this.DirectoryInfoFactory.Create(pathB);

            if(!dirB.Exists)
            {
                var message = string.Format("Second compare Folder '{0}' does not exist.", pathB);
                this.Logger.Warn(message);
            }

            var fileListA = dirA.GetFiles("*.*", SearchOption.AllDirectories);
            var fileListB = dirB.GetFiles("*.*", SearchOption.AllDirectories);

            var fileNameListA = fileListA.Select(info => info.Name);
            var fileNameListB = fileListB.Select(info => info.Name).ToList();

            // var hashFunc = new Func<IFileInfo, int>(
            // (IFileInfo info) =>
            // {
            // return new VariableFileHash(info, fileInfo => fileInfo.Length.GetHashCode());
            // });

            // filtFunc: filters the files to check, hashFunc: determines which parameters to check
            ////var hashFunc = new Func<IFileInfo, int>(info => info.Length.GetHashCode());
            ////var filtFunc = new Func<IFileInfo, bool>(info => !info.FullName.Contains(".git"));
            var filtFunc = new Func<IFileInfo, bool>(info => !info.FullName.Contains(".git") && (info.Length > 16 * 1024));
            var hashFunc = new Func<IFileInfo, int>(info => info.Name.GetHashCode() + info.Length.GetHashCode());

            var fileInfoListA = fileListA.Where(filtFunc).Select(info => new VariableFileHash(info, hashFunc)).ToList();

            ////var fileInfoListA = fileListA.Where(filtFunc).Select(info => new VariableFileHash(info, fileInfo => hashFunc(fileInfo))).ToList();
            ////var fileInfoListA = fileListA.Where(info => !info.FullName.Contains(".git")).Select(info => new VariableFileHash(info, fileInfo => FileChecker.Bla(fileInfo))).ToList();
            ////var fileInfoListA = fileListA.Where(info => !info.FullName.Contains(".git")).Select(info => FileChecker.GetVariableFileHash(info)).ToList();
            var fileInfoListB = fileListB.Where(filtFunc).Select(info => new VariableFileHash(info, hashFunc)).ToList();

            /*foreach(var fileInfo in fileListA)
            {
                if (fileListB.Contains(fileInfo))
                {
                    
                }
            }*/
            var cnt = 0;
            foreach(var file in fileInfoListA)
            {
                var xlist = fileInfoListB.Where(hash => hash.Equals(file)).ToList();

                // if(fileInfoListB.Contains(file, VariableFileHash.HashComparer))
                // {
                // }
                if(xlist.Count > 1)
                {
                    ////var fileMD5 = this.BuildFileHash(file.FullName);
                    string header = string.Format("File: '{0}':", file.FullName);
                    this.Logger.Info(header);

                    xlist.ForEach(hash => this.Logger.Info(string.Format("    {0}", hash.FullName)));
                    Application.DoEvents();
                    cnt++;
                }

                if(cnt > 200)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Moves the content out of path A and reconstructs it via symbolic links under path B.
        /// Todo: specify some criteria.
        /// </summary>
        /// <param name="pathSource">The path a.</param>
        /// <param name="pathDestination">The path b.</param>
        public void CreateSymbolicLinkedContent([CanBeNull] string pathSource, [CanBeNull] string pathDestination)
        {
            var todoMessage = string.Format(
                "<Exec-CreateSymbolicLinkedContent> Folder '{0}' against Folder '{1}'.",
                pathSource,
                pathDestination);
            this.Logger.Info(todoMessage);

            if(pathSource == null)
            {
                this.Logger.Warn("Source content folder specified has a null value.");
                return;
            }

            var dirA = this.DirectoryInfoFactory.Create(pathSource);

            if(!dirA.Exists)
            {
                var message = string.Format("Source folder '{0}' does not exist.", pathSource);
                this.Logger.Warn(message);
                return;
            }

            if(pathDestination == null)
            {
                this.Logger.Warn("Destination folder specified has a null value.");
                return;
            }

            var dirB = this.DirectoryInfoFactory.Create(pathDestination);

            if(!dirB.Exists)
            {
                var message = string.Format("Destination folder '{0}' does not exist.", pathDestination);
                this.Logger.Warn(message);
            }

            var fileListA = dirA.GetFiles("*.*", SearchOption.AllDirectories);
            var fileListB = dirB.GetFiles("*.*", SearchOption.AllDirectories);

            var filtFunc = new Func<IFileInfo, bool>(info => !info.FullName.Contains(".git") && (info.Length > 1 * 1024 * 1024));
            var hashFunc = new Func<IFileInfo, int>(info => info.Name.GetHashCode() + info.Length.GetHashCode());

            var fileInfoListA = fileListA.Where(filtFunc).Select(info => new VariableFileHash(info, hashFunc)).ToList();

            var cnt = 0;
            foreach(var file in fileInfoListA)
            {
                var xlist = fileInfoListA.Where(hash => hash.Equals(file)).ToList();

                // if(fileInfoListB.Contains(file, VariableFileHash.HashComparer))
                // {
                // }
                if(xlist.Count > 1)
                {
                    ////var fileMD5 = this.BuildFileHash(file.FullName);
                    string header = string.Format("File: '{0}':", file.FullName);
                    this.Logger.Info(header);

                    xlist.ForEach(hash => this.Logger.Info(string.Format("    {0}", hash.FullName)));
                    Application.DoEvents();
                    cnt++;
                }

                if(cnt > 200)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Checks for symbolic links in a folder and moves the link sources to a destination directory, replacing themselves with symbolic links.
        /// </summary>
        /// <param name="pathWithPossibleLinks">The path containing existing links.</param>
        /// <param name="pathDestination">The destination path where the new source data gets moved to.</param>
        public void MoveSymbolicLinkedContent([CanBeNull] string pathWithPossibleLinks, [CanBeNull] string pathDestination)
        {
            var todoMessage =
                string.Format(
                    "<Exec-MoveSymbolicLinkedContent> Folder '{0}' with possible symbolic links to Destination folder '{1}'",
                    pathWithPossibleLinks,
                    pathDestination);
            this.Logger.Info(todoMessage);

            if(pathWithPossibleLinks == null)
            {
                this.Logger.Warn("First compare Folder specified has a null value.");
                return;
            }

            var dirA = this.DirectoryInfoFactory.Create(pathWithPossibleLinks);

            if(!dirA.Exists)
            {
                var message = string.Format("First compare Folder '{0}' does not exist.", pathWithPossibleLinks);
                this.Logger.Warn(message);
                return;
            }

            if(pathDestination == null)
            {
                this.Logger.Warn("Second compare Folder specified has a null value.");
                return;
            }

            var dirB = this.DirectoryInfoFactory.Create(pathDestination);

            if(!dirB.Exists)
            {
                var message = string.Format("Destination Folder '{0}' does not exist.", pathDestination);
                this.Logger.Warn(message);
            }

            var fileListA = dirA.GetFiles("*.*", SearchOption.AllDirectories);
            var fileListB = dirB.GetFiles("*.*", SearchOption.AllDirectories);

            ////var fileNameListA = fileListA.Select(info => info.Name);
            ////var fileNameListB = fileListB.Select(info => info.Name).ToList();

            // var hashFunc = new Func<IFileInfo, int>(
            // (IFileInfo info) =>
            // {
            // return new VariableFileHash(info, fileInfo => fileInfo.Length.GetHashCode());
            // });

            // filtFunc: filters the files to check, hashFunc: determines which parameters to check
            ////var hashFunc = new Func<IFileInfo, int>(info => info.Length.GetHashCode());
            ////var filtFunc = new Func<IFileInfo, bool>(info => !info.FullName.Contains(".git"));
            var filtFunc = new Func<IFileInfo, bool>(info => info.IsValidSymlink(this.Logger));
            var hashFunc = new Func<IFileInfo, int>(info => info.Name.GetHashCode() + info.Length.GetHashCode());

            var sycrFunc = new Func<IFileInfo, IFileInfo>(info =>
                           {
                               return this.FileInfoFactory.Create(info.GetSymbolicLinkTarget());
                               var symlink = SymbolicLink.GetTarget(info.FullName);
                               var fileInfo = this.FileInfoFactory.Create(symlink);
                               if (fileInfo.Exists)
                               {
                                   return fileInfo;
                               }

                               // Todo: try to get the absolute path
                               return info;
                           });

            ////var fileInfoListA = fileListA.Where(filtFunc).Select(info => new VariableFileHash(info, hashFunc)).ToList();
            var fileInfoListA = fileListA.Where(filtFunc).Select(info => new SymbolicLinkInfo(info, sycrFunc(info))).ToList();
            ////var fileInfoListA = fileListA.Where(filtFunc).ToList();

            ////var fileInfoListA = fileListA.Where(filtFunc).Select(info => new VariableFileHash(info, fileInfo => hashFunc(fileInfo))).ToList();
            ////var fileInfoListA = fileListA.Where(info => !info.FullName.Contains(".git")).Select(info => new VariableFileHash(info, fileInfo => FileChecker.Bla(fileInfo))).ToList();
            ////var fileInfoListA = fileListA.Where(info => !info.FullName.Contains(".git")).Select(info => FileChecker.GetVariableFileHash(info)).ToList();
            ////var fileInfoListB = fileListB.Where(filtFunc).Select(info => new VariableFileHash(info, hashFunc)).ToList();

            /*foreach(var fileInfo in fileListA)
            {
                if (fileListB.Contains(fileInfo))
                {
                    
                }
            }*/
            var cnt = 0;

            string header = string.Format("Symlinks in '{0}':", dirA.FullName);
            this.Logger.Info(header);
            fileInfoListA.ForEach(info => this.Logger.Info(string.Format("    {0} -> {1} {2}", info.Link.FullName, info.Target.FullName, info.Target.Length)));

            foreach(var file in fileInfoListA)
            {
                ////var xlist = fileInfoListB.Where(hash => hash.Equals(file)).ToList();

                // if(fileInfoListB.Contains(file, VariableFileHash.HashComparer))
                // {
                // }
               /* if(xlist.Count > 1)
                {
                    ////var fileMD5 = this.BuildFileHash(file.FullName);
                    string header = string.Format("File: '{0}':", file.FullName);
                    this.Logger.Info(header);

                    xlist.ForEach(hash => this.Logger.Info(string.Format("    {0}", hash.FullName)));
                    Application.DoEvents();
                    cnt++;
                }*/

                if(cnt > 200)
                {
                    return;
                }
            }
        }



        /*/// <summary>
        /// Determines whether the specified path is a symbolic link.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <remarks>But this also identifies hard links and junction points positive...</remarks>
        /// <returns><c>true</c> if the specified path is symbolic; otherwise, <c>false</c>.</returns>
        private bool IsSymbolic(string path)
        {
            // see http://stackoverflow.com/questions/1485155/check-if-a-file-is-real-or-a-symbolic-link
            var pathInfo = new FileInfo(path);
            return pathInfo.Attributes.HasFlag(FileAttributes.ReparsePoint);
        }*/


        private class SymbolicLinkInfo
        {
            public IFileInfo Link { get; private set; }
            public IFileInfo Target { get; private set; }

            public SymbolicLinkInfo(IFileInfo link, IFileInfo target)
            {
                this.Link = link;
                this.Target = target;

            }
        }

        /// <summary>
        /// Compares the symbolic linked content of two folders and externalizes them into a destination folder.
        /// </summary>
        /// <param name="pathWithPossibleLinks">The path containing existing links.</param>
        /// <param name="pathToLookForSources">The path that contains the source data.</param>
        /// <param name="pathDestination">The destination path where the new source data gets moved to.</param>
        public void MoveSymbolicLinkedContentOld(
            [CanBeNull] string pathWithPossibleLinks,
            [CanBeNull] string pathToLookForSources,
            [CanBeNull] string pathDestination = null)
        {
            // Todo: check if this is a valid option/solution, otherwise use only pathWithPossibleLinks and pathDestination, as the source can be reconstructed by the symbolic link.
            var todoMessage =
                string.Format(
                    "<Exec-MoveSymbolicLinkedContentOld> Folder '{0}' with possible symbolic links against Folder '{1}'"
                    + " containing the source data to compare. Destination folder is: '{2}'",
                    pathToLookForSources,
                    pathWithPossibleLinks,
                    pathDestination);
            this.Logger.Info(todoMessage);

            if(pathToLookForSources == null)
            {
                this.Logger.Warn("First compare Folder specified has a null value.");
                return;
            }

            var dirA = this.DirectoryInfoFactory.Create(pathToLookForSources);

            if(!dirA.Exists)
            {
                var message = string.Format("First compare Folder '{0}' does not exist.", pathToLookForSources);
                this.Logger.Warn(message);
                return;
            }

            if(pathWithPossibleLinks == null)
            {
                this.Logger.Warn("Second compare Folder specified has a null value.");
                return;
            }

            var dirB = this.DirectoryInfoFactory.Create(pathWithPossibleLinks);

            if(!dirB.Exists)
            {
                var message = string.Format("Second compare Folder '{0}' does not exist.", pathWithPossibleLinks);
                this.Logger.Warn(message);
            }

            var fileListA = dirA.GetFiles("*.*", SearchOption.AllDirectories);
            var fileListB = dirB.GetFiles("*.*", SearchOption.AllDirectories);

            var fileNameListA = fileListA.Select(info => info.Name);
            var fileNameListB = fileListB.Select(info => info.Name).ToList();

            // var hashFunc = new Func<IFileInfo, int>(
            // (IFileInfo info) =>
            // {
            // return new VariableFileHash(info, fileInfo => fileInfo.Length.GetHashCode());
            // });

            // filtFunc: filters the files to check, hashFunc: determines which parameters to check
            ////var hashFunc = new Func<IFileInfo, int>(info => info.Length.GetHashCode());
            ////var filtFunc = new Func<IFileInfo, bool>(info => !info.FullName.Contains(".git"));
            var filtFunc = new Func<IFileInfo, bool>(info => !info.FullName.Contains(".git") && (info.Length > 16 * 1024));
            var hashFunc = new Func<IFileInfo, int>(info => info.Name.GetHashCode() + info.Length.GetHashCode());

            var fileInfoListA = fileListA.Where(filtFunc).Select(info => new VariableFileHash(info, hashFunc)).ToList();

            ////var fileInfoListA = fileListA.Where(filtFunc).Select(info => new VariableFileHash(info, fileInfo => hashFunc(fileInfo))).ToList();
            ////var fileInfoListA = fileListA.Where(info => !info.FullName.Contains(".git")).Select(info => new VariableFileHash(info, fileInfo => FileChecker.Bla(fileInfo))).ToList();
            ////var fileInfoListA = fileListA.Where(info => !info.FullName.Contains(".git")).Select(info => FileChecker.GetVariableFileHash(info)).ToList();
            var fileInfoListB = fileListB.Where(filtFunc).Select(info => new VariableFileHash(info, hashFunc)).ToList();

            /*foreach(var fileInfo in fileListA)
            {
                if (fileListB.Contains(fileInfo))
                {
                    
                }
            }*/
            var cnt = 0;
            foreach(var file in fileInfoListA)
            {
                var xlist = fileInfoListB.Where(hash => hash.Equals(file)).ToList();

                // if(fileInfoListB.Contains(file, VariableFileHash.HashComparer))
                // {
                // }
                if(xlist.Count > 1)
                {
                    ////var fileMD5 = this.BuildFileHash(file.FullName);
                    string header = string.Format("File: '{0}':", file.FullName);
                    this.Logger.Info(header);

                    xlist.ForEach(hash => this.Logger.Info(string.Format("    {0}", hash.FullName)));
                    Application.DoEvents();
                    cnt++;
                }

                if(cnt > 200)
                {
                    return;
                }
            }
        }

        private int BuildFileHash(string path)
        {
            using(var md5 = MD5.Create())
            {
                using(var stream = File.OpenRead(path))
                {
                    var hash = md5.ComputeHash(stream);
                    return hash.GetHashCode();
                }
            }
        }
    }
}