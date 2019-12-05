// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnigmaFileProcessor.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>10.04.2014 03:51</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.FtpClient;
    using System.Windows.Forms;

    using Deveknife.Api;
    using Deveknife.Blades.Overview.Util;

    using Castle.Core.Logging;

    public class EnigmaFileProcessor : IBladeTool
    {
        private readonly FileQueueProcessor fileQueue = new FileQueueProcessor();

        private readonly Func<string> getCopyPath;

        public EnigmaFileProcessor(
            ILogger logger, 
            string destinationPath, 
            /*string host,*/ string userName, 
            string password, 
            Func<string> callbackHandler)
        {
            this.getCopyPath = callbackHandler;
            Guard.NotNull(() => logger, logger);
            this.Logger = logger;
            Guard.NotNullOrEmpty(() => destinationPath, destinationPath);
            this.DestinationPath = destinationPath;

            /*Guard.NotNullOrEmpty(() => host, host);
            this.Host = host;*/
            Guard.NotNullOrEmpty(() => userName, userName);
            this.UserName = userName;
            this.Password = password;
        }

        private delegate void SingleUriHandler(
            bool doCopy, 
            bool deleteAfterCopy, 
            FtpClient conn, 
            Uri uri, 
            List<Uri> filesToDelete);

        public event EventHandler<FileProgressArgs> Progress;

        public string DestinationPath { get; private set; }

        public string Host { get; private set; }

        public ILogger Logger { get; private set; }

        public string UserName { get; private set; }

        private string Password { get; set; }

        public void CopyFiles(IEnumerable<string> files)
        {
            this.HandleFiles(files, true, false);
        }

        public List<string> CheckForMissingEit(List<string> result)
        {
            var missing = FindMissing.InList(result, ".ts", ".eit");
            return missing; 
        }

        public void DeleteFiles(IEnumerable<string> files)
        {
            this.HandleFiles(files, false, true);
        }

        public void MoveFiles(IEnumerable<string> files)
        {
            this.HandleFiles(files, true, true);
        }

        protected virtual void OnProgress(FileProgressArgs e)
        {
            var handler = this.Progress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private static DerivedVerb BuildCopyVerb(bool doCopy, bool deleteAfterCopy)
        {
            var actionVerb = new DerivedVerb { Verb = "checking", Derived = "check" };
            if (doCopy && !deleteAfterCopy)
            {
                actionVerb.Verb = "copying";
                actionVerb.Derived = "copy";
            }
            else if (doCopy)
            {
                actionVerb.Verb = "moving";
                actionVerb.Derived = "move";
            }

            /*else if (!doCopy && deleteAfterCopy)
            {
                actionStr1 = "deleting";
                actionStr2 = "delete";
            }*/
            return actionVerb;
        }

        private static string BuildFtpListItemDescription(
            bool doCopy, 
            bool deleteAfterCopy, 
            string text, 
            FtpListItem ftpListItem)
        {
            var first = string.Empty;
            if (doCopy && !deleteAfterCopy)
            {
                first = "Copying";
            }
            else if (doCopy)
            {
                first = "Moving";
            }
            else if (deleteAfterCopy)
            {
                first = "Mark for Deletion";
            }

            var size = ftpListItem.Size.ToString("#,###", CultureInfo.InvariantCulture) + " bytes";
            if (ftpListItem.Size > 1024)
            {
                size = string.Format("{0:#,###.00} kB", ftpListItem.Size / 1024d);
            }

            if (ftpListItem.Size > 1024 * 1024)
            {
                size = string.Format("{0:#,###.00} MB", ftpListItem.Size / 1024d / 1024d);
            }

            var result = string.Format("{0}{1} '{2}' with {3}.", text, first, ftpListItem.FullName, size);
            return result;
        }

        private static bool IsFTPFile(IGrouping<string, Uri> uriGroup)
        {
            return uriGroup.Key != string.Empty;
        }

        private void DeleteSingleUri(
            bool doCopy, 
            bool deleteAfterCopy, 
            FtpClient conn, 
            Uri uri, 
            List<Uri> filesToDelete)
        {
            var msg = "deleting '" + uri + "'.";
            this.Logger.Debug(msg);
            var fileName = uri.LocalPath;
            conn.DeleteFile(fileName);
        }

        private void HandleFiles(IEnumerable<string> files, bool doCopy, bool deleteAfterCopy)
        {
            // Note that for ftp-files it is a two step process, when "Moving" files. First copy,
            // then delete. So 'HandleFtpFiles' is modular and does these two steps after a security messagebox
            // for the user ( accept deleting files ).
            var uris = files.Select(file => new Uri(file)).ToList();
            var uriGroups = uris.GroupBy(uri => uri.Host);
            var filesToDelete = new List<Uri>();

            // Log the action.
            var actionVerb = BuildCopyVerb(doCopy, deleteAfterCopy);

            this.Logger.Info("Going to " + actionVerb.Derived + " " + uris.Count() + " file groups.");

            // copy and sum up the files that take part in the action. 
            foreach (var uriGroup in uriGroups)
            {
                // Or group first by uri.IsFile ?
                if (IsFTPFile(uriGroup))
                {
                    try
                    {
                        // copy and delete-mark ftp files
                        this.HandleFtpFiles(
                            uriGroup, 
                            doCopy, 
                            deleteAfterCopy, 
                            filesToDelete, 
                            this.HandleSingleUriCopyMove);
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Error(ex.ToString);
                    }
                }
                else
                {
                    try
                    {
                        // copy and delete-mark local files
                        // this.HandleLocalFiles(uriGroup, doCopy, deleteAfterCopy, filesToDelete);
                        this.HandleLocalFiles(uriGroup, doCopy, deleteAfterCopy, filesToDelete, actionVerb);
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Error(ex.ToString);
                    }
                }
            }

            // Is anything to do ? Then log that.
            if (filesToDelete.Count <= 0)
            {
                this.Logger.Debug("There are no files to delete.");
                this.Logger.Info("Done with " + actionVerb.Verb + " " + uris.Count() + " groups.");
                return;
            }

            this.Logger.Info(
                "Done with " + actionVerb.Verb + " " + filesToDelete.Count + " files in " + uris.Count() + " groups.");

            // ask the user.
            var displayfile = filesToDelete.Select(uri => uri.LocalPath);
            // Todo: request that stuff over IoC, or over generic host mechanism 
            var dialog = new ListingDialogForm("Are you sure, you want to delete these files?");
            var result = dialog.ShowDialog(displayfile);
            if (result != DialogResult.Yes)
            {
                return;
            }

            // finally erase the files.
            var delGroups = filesToDelete.GroupBy(uri => uri.Host);

            foreach (var uriGroup in delGroups)
            {
                if (IsFTPFile(uriGroup))
                {
                    try
                    {
                        // delete ftp files
                        this.HandleFtpFiles(uriGroup, false, true, filesToDelete, this.DeleteSingleUri);
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Error(ex.ToString);
                    }
                }
                else
                {
                    try
                    {
                        // delete local files
                        if (doCopy)
                        {
                            continue;
                        }

                        var localFiles = uriGroup.Select(uri => uri.OriginalString);
                        this.fileQueue.DeleteFiles(localFiles /*, delAction ... print the stuff*/);
                        foreach (var delFile in localFiles)
                        {
                            var msg = "deleting '" + delFile + "'.";
                            this.Logger.Debug(msg);

                            // Todo: all local file operations have to be queued to an extra processor, that runs on his own thread.
                            // Recycle.DeleteFileOperation(delFile);
                            File.Delete(delFile);
                        }

                        // this.HandleLocalFiles(uriGroup, false, true, filesToDelete);
                    }
                    catch (Exception ex)
                    {
                        this.Logger.Error(ex.ToString);
                    }
                }
            }

            this.Logger.Info("Done with deleting " + filesToDelete.Count + " files in " + uris.Count() + " groups.");
        }

        private void HandleFtpFiles(
            IEnumerable<Uri> files, 
            bool doCopy, 
            bool deleteAfterCopy, 
            List<Uri> filesToDelete, 
            SingleUriHandler singleUriHandler)
        {
            var byHost = files.GroupBy(uri => uri.Host);
            foreach (var host in byHost)
            {
                using (var conn = new FtpClient())
                {
                    conn.Host = host.Key;
                    conn.Credentials = new NetworkCredential(this.UserName, this.Password);

                    foreach (var uri in host)
                    {
                        singleUriHandler(doCopy, deleteAfterCopy, conn, uri, filesToDelete);
                    }
                }
            }
        }

        private void HandleLocalFiles(
            IEnumerable<Uri> eitFiles, 
            bool doCopy, 
            bool deleteAfterCopy, 
            ICollection<Uri> filesToDelete, 
            DerivedVerb actionVerb)
        {
            var destinationFolder = string.Empty;
            if (doCopy)
            {
                if (this.getCopyPath == null)
                {
                    this.Logger.Error("No getCopyPath Func<string> is set.");
                    return;
                }

                destinationFolder = this.getCopyPath();
                if (string.IsNullOrEmpty(destinationFolder))
                {
                    this.Logger.Warn("Cancelled " + actionVerb.Derived + " operation.");
                    return;
                }
            }

            var doneFiles = new List<string>();
            var uris = eitFiles as IList<Uri> ?? eitFiles.ToList();
            var localEitFiles = uris.Select(uri => uri.OriginalString);
            foreach (var eitFile in localEitFiles)
            {
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(eitFile);
                var path = Path.GetDirectoryName(eitFile);
                var doFiles = Directory.EnumerateFiles(path, fileNameWithoutExtension + ".*").ToList();
                //var filenames4 =  Directory.EnumerateFiles(path, ".*", SearchOption.TopDirectoryOnly).Select(Path.GetFileName).ToList(); 
                //var filenames5 =  Directory.EnumerateFiles(path, ".*", SearchOption.TopDirectoryOnly).ToList(); 
                //var filenames6 =  Directory.EnumerateFiles(@"Y:\HDS721050CLA362\movie", "20190106 0545 - kabel eins - Elementary" + "*.*", SearchOption.TopDirectoryOnly).ToList(); 
                //var filenames7 =  Directory.EnumerateFiles(@"Y:\HDS721050CLA362\movie", fileNameWithoutExtension + "*.*", SearchOption.TopDirectoryOnly).ToList(); 

                //var filenames8 =  Directory.EnumerateFiles(@"Y:\HDS721050CLA362\movie", "20190106 0545 - kabel eins - Elementary" + "*.*", SearchOption.TopDirectoryOnly).ToList(); 
                //var filenames8 =  Directory.EnumerateFiles(@"Y:\HDS721050CLA362\movie", "20190106 0545 - k" + "*.*", SearchOption.TopDirectoryOnly).ToList(); 
                //var filenames9 =  Directory.EnumerateFiles(path, "20190106 0545 - " + "*.*", SearchOption.TopDirectoryOnly).ToList(); 
                // hack: my linux ext4 fs over symlink and mounted as network drive does not match with filenames > 16
                var filenames10 =  Directory.EnumerateFiles(path, 
                    fileNameWithoutExtension.Substring(0, Math.Min(fileNameWithoutExtension.Length, 16)) + "*.*", SearchOption.TopDirectoryOnly); 
                var filenames11 = filenames10.Where(s => s.Contains(fileNameWithoutExtension+".")).ToList();
                if(filenames11.Count > doFiles.Count)
                    doFiles = filenames11;

                if (doFiles.Count == 0)
                {
                    this.Logger.Info(
                        "No local Files to " + actionVerb.Derived + " for group " + path + " -> "
                        + fileNameWithoutExtension + ".*");
                }

                foreach (var file in doFiles)
                {
                    if (!File.Exists(file))
                    {
                        var msg = "File '" + file + "' does not exist!";
                        this.Logger.Warn(msg);
                    }
                    else
                    {
                        var fileUri = new Uri(file);
                        if (doCopy)
                        {
                            if (!deleteAfterCopy)
                            {
                                // copy
                                var fileName = Path.GetFileName(file);
                                var destinationPath = Path.Combine(destinationFolder, fileName);
                                var msg = "Copying '" + file + "'.";
                                this.Logger.Debug(msg);

                                // Todo: all local file operations have to be queued to an extra processor, that runs on his own thread.
                                File.Copy(file, destinationPath);
                                this.fileQueue.Copy(file);

                                // filesToDelete.Add(fileUri);
                                doneFiles.Add(file);
                            }
                            else
                            {
                                // move
                                var fileName = Path.GetFileName(file);
                                var destinationPath = Path.Combine(destinationFolder, fileName);
                                var msg = "Moving '" + file + "'.";
                                this.Logger.Debug(msg);

                                // Todo: all local file operations have to be queued to an extra processor, that runs on his own thread.
                                File.Move(file, destinationPath);
                                this.fileQueue.Move(file);
                                doneFiles.Add(file);

                                // filesToDelete.Add(fileUri);
                            }
                        }
                        else if (deleteAfterCopy)
                        {
                            // mark for deletion
                            var msg = "Erasing '" + file + "'.";
                            this.Logger.Debug(msg);
                            filesToDelete.Add(fileUri);

                            // var localFiles = uriGroup.Select(uri => uri.OriginalString);
                            // this.DeleteLocalFiles(file);
                        }
                    }
                }
            }

            this.Logger.Info(
                "Done with " + "HandleLocalFiles " + actionVerb.Derived + ", " + doneFiles.Count + " files in "
                + uris.Count() + " groups.");
        }

        private void HandleSingleUriCopyMove(
            bool doCopy, 
            bool deleteAfterCopy, 
            FtpClient conn, 
            Uri uri, 
            List<Uri> filesToDelete)
        {
            // var finfo = new FileInfo(uri.LocalPath);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(uri.LocalPath);
            var path = Path.GetDirectoryName(uri.LocalPath).Replace("\\", "/");

            // Todo: route this over an IHost container mechanism.
            var lister = new CachedFtpLister(new FtpLister(conn));
            var lst = lister.GetListing(path);
            var searchPath = path + "/" + fileNameWithoutExtension;
            var fileList = lst.Where(item => item.FullName.StartsWith(searchPath)).ToList();
            foreach (var ftpListItem in fileList)
            {
                var destination = Path.Combine(this.DestinationPath, ftpListItem.Name);
                var msg = BuildFtpListItemDescription(doCopy, deleteAfterCopy, string.Empty, ftpListItem);
                this.Logger.Debug(msg);
                if (doCopy)
                {
                    // var ftproc = new FtpReader(UserName, Password);
                    // ftproc.Progress += (sender, args) => this.OnProgress(args);
                    // ftproc.OpenReadURI(uri, DestinationPath);
                    var exists = File.Exists(destination);
                    if (exists)
                    {
                        var msg2 = string.Format("File '{0}' already exists, skipping copy.", ftpListItem.FullName);
                        this.Logger.Warn(msg2);

                        // this continue skips a delete, as an already existing file is treated as problem.
                        continue;
                    }

                    FtpReader.OpenRead(ftpListItem.FullName, conn, destination, this.OnProgress);
                }

                if (deleteAfterCopy)
                {
                    // var delUri = new Uri("ftp://" + conn.Host + ftpListItem.FullName);
                    // filesToDelete.Add(delUri);
                    // conn.DeleteFile(ftpListItem.FullName);
                    var delUri = new Uri("ftp://" + conn.Host + ftpListItem.FullName);
                    filesToDelete.Add(delUri);
                }
            }

            // lst.Select()
        }
    }
}