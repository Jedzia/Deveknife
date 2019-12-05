// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileManagerUI.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 10:44</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    using Deveknife.Api;
    using Deveknife.Blades.FileManager.Jobs;
    using Deveknife.Blades.FileManager.Jobs.Compression;
    using Deveknife.Blades.FileManager.UI;
    using Deveknife.Blades.FileManager.Util;
    using Deveknife.Blades.Utils.Filters;

    using Castle.Core.Logging;

    /// <summary>
    ///     The user-interface of the  Video-Overview tool.
    /// </summary>
    public partial class FileManagerUI : UserControl, IBladeUI
    {
        /// <summary>
        ///     The fixed filter
        /// </summary>
        private readonly ITextFilter fixedFilter = new FixedPathFilter();

        /// <summary>
        ///     The host
        /// </summary>
        private readonly IHost host;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileManagerUI"/> class.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="bladeToolFactory">
        /// The blade tool factory.
        /// </param>
        public FileManagerUI(IHost host, ILogger logger, IBladeToolFactory bladeToolFactory)
            : this()
        {
            Guard.NotNull(() => host, host);
            this.host = host;
            Guard.NotNull(() => logger, logger);
            this.Logger = logger.CreateChildLogger("FileManagerLogger");

            Guard.NotNull(() => bladeToolFactory, bladeToolFactory);
            this.BladeToolFactory = bladeToolFactory;

            this.treeListExplorer1.Logger = this.Logger;
            this.jobControl1.Logger = this.Logger;

            // this.eitPictureChooser = new EitListPictureChooser(logger, eit => !this.fixedFilter.Contains(eit.Filename));
            // this.eitColorer = new EitListColorer(
            // logger, 
            // this.GroupEitForColoringFunc, 
            // eit => !this.fixedFilter.Contains(eit.Filename));
            // this.eitFetcher = new EitListFetcher(logger);
            // this.InitGridMenuRunActions(
            // new FileManagerUiGridMenu<EITFormatDisplay>(this.gridView1), 
            // this.BladeToolFactory);

            // var ftpClient = new FtpClient();
            // var bladeToolA = this.BladeToolFactory.CreateTool<Dingens>();
            // var bladeToolB = this.BladeToolFactory.CreateTool<FtpLister>(ftpClient);
            // var bladeToolB = this.BladeToolFactory.CreateTool<CachedFtpLister>(ftpClient);
            // var bladeTools = this.BladeToolFactory.CreateAll(ftpClient);
            this.LoadSavedComparisons();
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="FileManagerUI" /> class from being created.
        /// </summary>
        private FileManagerUI()
        {
            this.InitializeComponent();
        }

        /// <summary>
        ///     Gets the corresponding blade for this User Interface.
        /// </summary>
        /// <value>The attached blade.</value>
        public IBlade Blade { get; internal set; }

        /// <summary>
        ///     Gets the name of the <see cref="IBlade" />.
        /// </summary>
        /// <value>The name.</value>
        public string BladeName
        {
            get
            {
                return "File Manager";
            }
        }

        /// <summary>
        ///     Gets the blade tool factory.
        /// </summary>
        /// <value>The blade tool factory.</value>
        public IBladeToolFactory BladeToolFactory { get; private set; }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; private set; }

        /// <summary>
        ///     A method for testing, that throws an exception.
        /// </summary>
        /// <exception cref="FileManagerException">One to rule them all ... Test Exception.</exception>
        public static void ThrowSomething()
        {
            throw new FileManagerException("One to rule them all ... Test Exception.");
        }
        
        private static int GetRnd()
        {
            var random = new Random(1234).Next();
            return random;
        }

        private string AskCopyPath()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the Click event of the CleanupDone button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnCleanupDoneClick(object sender, EventArgs e)
        {
            // const string VideoBasePath = Globals.FsWatchBasePath;
            // var result = this.eitFetcher.GetLocalEitFilesThatAreDone(VideoBasePath);
            // var enigmaFileProcessor = this.BladeToolFactory.CreateTool<EnigmaFileProcessor>(
            // Globals.FsVideoBasePath, 
            // FtpUserName, 
            // FtpPassword, 
            // AskCopyPath);
            // enigmaFileProcessor.DeleteFiles(result);
        }

        /// <summary>
        /// Handles the Click event of the "Set Comparer" button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnComparerSetClick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the <c>btnFetch</c> button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnFetchClick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnFetchFolder control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnFetchFolderClick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the <c>btnFetchFtp</c> button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnFetchFtpClick(object sender, EventArgs e)
        {
        }

        private void BtnFixClick(object sender, EventArgs e)
        {
            int x = 2;
            var num = DateTime.Now.Second;
            if((this.Bottom > (num * 2)) && ((DateTime.Now.Second & 0x0203) > 6))
            {
                x = 3;
            }
        }

        /// <summary>
        /// Handles the Click event of the <c>btnRunTestJob</c> button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnRunTestJobClick(object sender, EventArgs e)
        {
            var path = this.treeListExplorer1.SelectedDirectory;

            // var jobA = new Traverse();
            // var jobB = new Compare();
            // var jobC = new FileWinRarAction();
            var jobParameters = new JobParameters();
            jobParameters.Add(Traverse.MainParameterId, path);
            var choice = (int)this.rdgAsync.EditValue;
            if(choice == 0)
            {
                var result = this.jobControl1.ExecuteJobsAsync(jobParameters);

                // result.Execute();
            }
            else
            {
                var result = this.jobControl1.ExecuteJobs(jobParameters);
            }
        }

        /// <summary>
        /// Handles the Click event of the Test JumpPath button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnTestJumpPath2Click(object sender, EventArgs e)
        {
            this.treeListExplorer1.JumpToPath(@"S:\!Torrent\!Video+Grafik\!Poser\!DAZ\!Models\!Genesis\");
        }

        /// <summary>
        /// Handles the Click event of the Test JumpPath button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnTestJumpPathClick(object sender, EventArgs e)
        {
            this.treeListExplorer1.JumpToPath(@"E:\Shared\!Remove\DAZ\!V4");
        }

        /// <summary>
        /// Handles the Click event of the btnComparerClear control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnTestWinrarClick(object sender, EventArgs e)
        {
            var winrarPath = @"C:\Program Files\WinRAR\WinRAR.exe";
            var rarRunner = new WinRar(this.Logger, winrarPath);

            string path = @"E:\Shared\!Remove\DAZ\!V4\97429 Modern School Uniform";
            IWinrarParameters parameters = new WinrarParameters();
            ITextFilter filter = new DefaultTextFilter();
            var proc = rarRunner.Remove(path, parameters, filter);
        }

        /// <summary>
        /// Handles the Load event of the FileManagerUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FileManagerUILoad(object sender, EventArgs e)
        {
            this.Logger.Info("FileManagerUI_Load, FileManager up and running.");
        }

        /// <summary>
        /// Initializes the grid menu run actions.
        /// </summary>
        /// <param name="gmenu">
        /// The gmenu.
        /// </param>
        /// <param name="bladeToolFactory">
        /// The blade tool factory.
        /// </param>
        private void InitGridMenuRunActions(FileManagerUiGridMenu<Dummy> gmenu, IBladeToolFactory bladeToolFactory)
        {
            this.pbProgress.EditValue = 0L;
            var enigmaFileProcessor = bladeToolFactory.CreateTool<DummyProcessor>(
                Globals.FsVideoBasePath,
                "FtpUserName",
                "FtpPassword",
                this.AskCopyPath);

            // this.EnigmaFileProcessorSetProgress(enigmaFileProcessor);
            gmenu.AddAction(
                "Open",
                delegate(IEnumerable<Dummy> items, string s)
                {
                    var firstOrDefault = items.FirstOrDefault();
                    if(firstOrDefault == null)
                    {
                        return;
                    }

                    var filename = firstOrDefault.Filename;

                    // var filepath = Path.GetFullPath(filename);
                    // var filewoext = Path.GetFileNameWithoutExtension(filename);
                    // var newfile = Path.Combine(filepath, filewoext, ".ts");
                    foreach(var newfile in
                        new List<string> { ".ts", ".mkv", ".avi" }.Select(ext => Path.ChangeExtension(filename, ext)).Where(File.Exists))
                    {
                        FileStarter.Execute(newfile);
                        return;
                    }
                });
            gmenu.AddAction("Copy", (items, s) => enigmaFileProcessor.CopyFiles(items.Select(eitItem => eitItem.Filename)));
            gmenu.AddAction("Move", (items, s) => enigmaFileProcessor.MoveFiles(items.Select(eitItem => eitItem.Filename)));
            gmenu.AddAction("Delete", (items, s) => enigmaFileProcessor.DeleteFiles(items.Select(eitItem => eitItem.Filename)));
        }

        /// <summary>
        /// Loads the saved comparisons.
        /// </summary>
        private void LoadSavedComparisons()
        {
            /*var serializer = new DataContractSerializer(typeof(Dictionary<string, bool>));
            Dictionary<string, bool> browseDict;
            if (
                !RegistryAccess.GetRegDictForRootFolders(
                    HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName,
                    HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempFetchFolders,
                    serializer,
                    out browseDict))
            {
                return;
            }

            foreach (var b in browseDict)
            {
                //this.AddToEitComparerAsync(b.Key);
            }*/
        }

        /// <summary>
        /// Handles the Click event of the context menu's ClearText entry.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void MenuContextClearTextClick(object sender, EventArgs e)
        {
            this.tbFileManagerLog.Clear();
        }

        /// <summary>
        /// Tbs the overview log text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void TbOverviewLogTextChanged(object sender, EventArgs e)
        {
            this.tbFileManagerLog.SelectionStart = this.tbFileManagerLog.Text.Length;
            this.tbFileManagerLog.ScrollToCaret();
            this.tbFileManagerLog.SelectionLength = 0;
        }

        /// <summary>
        /// Handles the DirectoryChanged event of the treeListExplorer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectoryChangedEventArgs"/> instance containing the event data.</param>
        private void treeListExplorer1_DirectoryChanged(object sender, DirectoryChangedEventArgs e)
        {
        }

        /// <summary>
        /// Handles the DirectoryChanged event of the treeListExplorer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DirectoryChangedEventArgs"/> instance containing the event data.</param>
        private void TreeListExplorer1DirectoryChanged(object sender, DirectoryChangedEventArgs e)
        {
            this.Logger.Info(string.Format("Directory changed to: '{0}'.", e.Directory));
        }

        /// <summary>
        /// Handles the FilesAvailable event of the treeListExplorer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UI.FilesEventArgs"/> instance containing the event data.</param>
        private void TreeListExplorer1FilesAvailable(object sender, FilesEventArgs e)
        {
            var data = e.Files.Select(s => new FileInfo(s));
            this.gridExplorer1.DataSource = data;
        }

        private class Dummy
        {
            public string Filename { get; set; }
        }

        private class DummyProcessor
        {
            public void CopyFiles(IEnumerable<string> select)
            {
                throw new NotImplementedException();
            }

            public void DeleteFiles(IEnumerable<string> select)
            {
                throw new NotImplementedException();
            }

            public void MoveFiles(IEnumerable<string> select)
            {
                throw new NotImplementedException();
            }
        }
    }
}