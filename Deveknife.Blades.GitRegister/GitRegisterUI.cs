// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GitRegisterUI.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2019, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2019 20:24</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.GitRegister
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using SystemInterface.IO;

    using Castle.Core.Logging;

    using Deveknife.Api;
    using Deveknife.Blades.FileMoveTool.Filesystem;
    using Deveknife.Blades.GitRegister.Util;
    using Deveknife.Blades.Utils.Filters;

    using LibGit2Sharp;

    using Renci.SshNet;

    /// <summary>
    ///     The user-interface of the  Video-Overview tool.
    /// </summary>
    public partial class GitRegisterUI : UserControl, IBladeUI
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
        /// Initializes a new instance of the <see cref="GitRegisterUI" /> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="bladeToolFactory">The blade tool factory.</param>
        /// <param name="inoutService">The io service.</param>
        /// <param name="dialogService">The dialog service.</param>
        public GitRegisterUI(
            IHost host,
            ILogger logger,
            IBladeToolFactory bladeToolFactory,
            IOService inoutService,
            IDialogService dialogService)
            : this()
        {
            this.host = Guard.NotNull(() => host, host);
            Guard.NotNull(() => logger, logger);
            this.Logger = logger.CreateChildLogger("GitRegisterLogger");

            this.BladeToolFactory = Guard.NotNull(() => bladeToolFactory, bladeToolFactory);
            this.buttonEditFolder.DialogService = Guard.NotNull(() => dialogService, dialogService);

            this.DirectoryInfoFactory = inoutService.GetWrappedIOServiceFactory().CreateDirectoryInfoFactory();
            this.FileInfoFactory = inoutService.GetWrappedIOServiceFactory().CreateFileInfoFactory();

            // Devel: Values for development, remove after testing.
            this.buttonEditFolder.EditValue = @"E:\Projects\Elektronik\test";

            this.GitDisplayItems = new ObservableCollection<GitDisplayItem>
                                   {
                                       new GitDisplayItem { Name = "MyName", Path = "MyPath", Remote = "RemoteUri", Selected = false },
                                       new GitDisplayItem { Name = "MyName2", Path = "MyPath1", Remote = "RemoteUri1", Selected = false },
                                       new GitDisplayItem { Name = "MyName3", Path = "MyPath2", Remote = "RemoteUri2", Selected = true },
                                       new GitDisplayItem { Name = "MyName4", Path = "MyPath3", Remote = "RemoteUri3", Selected = false }
                                   };
            this.gitDisplayItemBindingSource.DataSource = this.GitDisplayItems;

            this.LoadSavedComparisons();
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="GitRegisterUI" /> class from being created.
        /// </summary>
        private GitRegisterUI()
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
                return "Git Registrar";
            }
        }

        /// <summary>
        ///     Gets the blade tool factory.
        /// </summary>
        /// <value>The blade tool factory.</value>
        public IBladeToolFactory BladeToolFactory { get; private set; }

        /// <summary>
        /// Gets the directory information factory.
        /// </summary>
        /// <value>The directory information factory.</value>
        public IDirectoryInfoFactory DirectoryInfoFactory { get; private set; }

        /// <summary>
        /// Gets the file information factory.
        /// </summary>
        /// <value>The file information factory.</value>
        [NotNull]
        public IFileInfoFactory FileInfoFactory { get; private set; }

        public ObservableCollection<GitDisplayItem> GitDisplayItems { get; set; }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; private set; }

        /// <summary>
        /// A method for testing, that throws an exception.
        /// </summary>
        /// <exception cref="Deveknife.Blades.GitRegister.GitRegisterException">One to rule them all ... Test Exception.</exception>
        public static void ThrowSomething()
        {
            throw new GitRegisterException("One to rule them all ... Test Exception.");
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
        /// Handles the Click event of the btnComparerClear control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnComparerClearClick(object sender, EventArgs e)
        {
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
            //var repoPath = @"E:\Projects\Elektronik\test\RepoB";
            var repoPath = this.buttonEditFolder.EditValue.ToString();

            var checker = new FileChecker(this.Logger, this.DirectoryInfoFactory, this.FileInfoFactory);
            checker.CompareFolders(repoPath);

            // var keyFileStream = new FileStream(@"D:\Users\Jedzia.pubsiX\.ssh\vuduo2-id_rsa.ppk",FileMode.Open);

            ////var client = new SshClient("vuduo2x", "git", "xxxxx");
            // ReSharper disable StringLiteralTypo
            var sp = this.host.GetSettingsProvider();
            Guard.NotNull(() => sp, sp);
            var sshHost = sp["SSH_Host"];
            var sshUser = sp["SSH_User"];
            var sshKeyFile = sp["SSH_KeyFile"];
            //Debug.Assert(sshHost != null, nameof(sp) + " sshHost Setting != null");
            // ToDo: better use a transparent message for users, when no connection setting is applied.
            Guard.NotNullOrEmpty(() => sshHost, sshHost);
            Guard.NotNullOrEmpty(() => sshUser, sshUser);
            Guard.NotNullOrEmpty(() => sshKeyFile, sshKeyFile);

            using(var client = new SshClient(sshHost, sshUser, new PrivateKeyFile(sshKeyFile)))
            {
                client.Connect();

                var result = client.RunCommand("ls -la");
                var r = result.Result.Replace("\n", "\r\n");
                this.Logger.Info($"from ssh :{Environment.NewLine}{r}");

                client.Disconnect();
            }

            return;

            try
            {
                using(var repo = new Repository(repoPath))
                {
                    // Object lookup
                    var obj = repo.Lookup("sha");

                    var show = new StatusShowOption();
                    var status = repo.RetrieveStatus(new StatusOptions { Show = show });

                    var RFC2822Format = "ddd dd MMM HH:mm:ss yyyy K";

                    foreach(Commit c in repo.Commits.Take(15))
                    {
                        this.Logger.Info($"commit {c.Id}");

                        if(c.Parents.Count() > 1)
                        {
                            this.Logger.Info($"Merge: {string.Join(" ", c.Parents.Select(p => p.Id.Sha.Substring(0, 7)).ToArray())}");
                        }

                        this.Logger.Info($"Author: {c.Author.Name} <{c.Author.Email}>");
                        this.Logger.Info($"Date:   {c.Author.When.ToString(RFC2822Format, CultureInfo.InvariantCulture)}");
                        this.Logger.Info(Environment.NewLine);
                        this.Logger.Info(c.Message);
                        this.Logger.Info(Environment.NewLine);
                    }
                }
            }
            catch(Exception exception)
            {
                this.Logger.Error($"Exception while traversing directories: {exception.Message}", exception);
            }
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
        }

        /// <summary>
        /// Handles the Click event of the <c>btnSaveLayout</c> button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnSaveLayoutClick(object sender, EventArgs e)
        {
            // var executablePath = Application.ExecutablePath;
            // var fi = new FileInfo(executablePath);
            // if (fi.DirectoryName != null)
            // {
            // var layoutfile = Path.Combine(fi.DirectoryName, this.GetType().FullName + ".layout.xml");
            // this.gridControl1.DefaultView.SaveLayoutToXml(layoutfile);
            // }
            // else
            // {
            // this.Logger.Error("DirectoryName of '" + executablePath + "' is null.");
            // this.host.DisplayStatus(new StatusData("Bla!"));
            // }
        }

        /// <summary>
        /// Handles the Click event of the TestFilter button.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnTestFilterClick(object sender, EventArgs e)
        {
            // var filt = this.gridView1.ActiveFilter;
            // var crit = filt.Criteria;
            // this.TestCrit2(crit);
        }

        private void GitRegisterUI_Load(object sender, EventArgs e)
        {
            this.tbGitRegisterLog.Clear();
            this.Logger.Info(string.Format("Initialized: '{0}'.", this.GetType()));
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
        private void InitGridMenuRunActions(GitRegisterUiGridMenu<Dummy> gmenu, IBladeToolFactory bladeToolFactory)
        {
            this.pbProgress.EditValue = 0L;
            var enigmaFileProcessor = bladeToolFactory.CreateTool<DummyProcessorExample>(
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
                    foreach(var newfile in new List<string> { ".ts", ".mkv", ".avi" }.Select(ext => Path.ChangeExtension(filename, ext))
                        .Where(File.Exists))
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
            this.tbGitRegisterLog.Clear();
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
            this.tbGitRegisterLog.SelectionStart = this.tbGitRegisterLog.Text.Length;
            this.tbGitRegisterLog.ScrollToCaret();
            this.tbGitRegisterLog.SelectionLength = 0;
        }

        /// <summary>
        /// Example Class Dummy.
        /// </summary>
        private class Dummy
        {
            public string Filename { get; [UsedImplicitly] set; }
        }

        /// <summary>
        /// Example Class DummyProcessorExample.
        /// </summary>
        [UsedImplicitly]
        private class DummyProcessorExample
        {
            public void CopyFiles(IEnumerable<string> select)
            {
                Guard.NotNull(() => select, select);
                throw new NotImplementedException();
            }

            public void DeleteFiles(IEnumerable<string> select)
            {
                Guard.NotNull(() => select, select);
                throw new NotImplementedException();
            }

            public void MoveFiles(IEnumerable<string> select)
            {
                Guard.NotNull(() => select, select);
                throw new NotImplementedException();
            }
        }
    }
}