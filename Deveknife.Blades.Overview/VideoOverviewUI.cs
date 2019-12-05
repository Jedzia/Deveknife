// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VideoOverviewUI.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>07.11.2015 23:21</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Deveknife.Api;
    using Deveknife.Blades.Overview.Util;
    using Deveknife.Blades.Utils.Filters;

    using Castle.Core.Logging;

    using DevExpress.XtraGrid.Views.Grid;

    using Microsoft.Win32;

    /// <summary>
    ///     The user-interface of the  Video-Overview tool.
    /// </summary>
    public partial class VideoOverviewUI : UserControl, IBladeUI
    {
        /// <summary>
        ///     The FTP eit base path
        /// </summary>
        private const string FtpEitBasePath = "/media/hdd/movie";

        /// <summary>
        ///     The FTP host
        /// </summary>
        private const string FtpHost = "192.168.3.43";

        /// <summary>
        ///     The FTP password
        /// </summary>
        private const string FtpPassword = "";

        /// <summary>
        ///     The FTP user name
        /// </summary>
        private const string FtpUserName = "root";

        /// <summary>
        /// The registry key to the current user blades overview temp fetch folder
        /// </summary>
        private const string HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempFetchFolders =
            "FetchFolderRootFolders";

        /// <summary>
        /// The registry key to the current user temp key name
        /// </summary>
        private const string HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName =
            @"HKEY_CURRENT_USER\Software\EvePanix\Deveknife\Blades\Overview\Temp";

        /// <summary>
        ///     The eit colorer
        /// </summary>
        private readonly EitListColorer eitColorer;

        /// <summary>
        ///     The eit comparer
        /// </summary>
        private readonly EitComparer eitComparer = new EitComparer();

        /// <summary>
        ///     The eit fetcher
        /// </summary>
        private readonly EitListFetcher eitFetcher;

        /// <summary>
        ///     The eit picture chooser
        /// </summary>
        private readonly EitListPictureChooser eitPictureChooser;

        /// <summary>
        ///     The fixed filter
        /// </summary>
        private readonly ITextFilter fixedFilter = new FixedPathFilter();

        /// <summary>
        ///     The host
        /// </summary>
        private readonly IHost host;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoOverviewUI"/> class.
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
        public VideoOverviewUI(IHost host, ILogger logger, IBladeToolFactory bladeToolFactory)
            : this()
        {
            Guard.NotNull(() => host, host);
            this.host = host;
            Guard.NotNull(() => logger, logger);
            this.Logger = logger.CreateChildLogger("VideoOverviewLogger");

            Guard.NotNull(() => bladeToolFactory, bladeToolFactory);
            this.BladeToolFactory = bladeToolFactory;

            this.InitDisplayTypeComboBox();
            this.eitPictureChooser = new EitListPictureChooser(logger, eit => !this.fixedFilter.Contains(eit.Filename));
            this.eitColorer = new EitListColorer(
                logger,
                this.GroupEitForColoring,
                eit => !this.fixedFilter.Contains(eit.Filename));
            this.eitFetcher = new EitListFetcher(logger);
            this.InitGridMenuRunActions(
                new VideoOverviewUiGridMenu<EITFormatDisplay>(this.gridView1),
                this.BladeToolFactory);

            // var ftpClient = new FtpClient();
            // var bladeToolA = this.BladeToolFactory.CreateTool<Dingens>();
            // var bladeToolB = this.BladeToolFactory.CreateTool<FtpLister>(ftpClient);
            // var bladeToolB = this.BladeToolFactory.CreateTool<CachedFtpLister>(ftpClient);
            // var bladeTools = this.BladeToolFactory.CreateAll(ftpClient);
            this.LoadSavedComparers();
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="VideoOverviewUI" /> class from being created.
        /// </summary>
        private VideoOverviewUI()
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
                return "EIT Overview";
            }
        }

        /// <summary>
        ///     Gets the blade tool factory.
        /// </summary>
        /// <value>The blade tool factory.</value>
        public IBladeToolFactory BladeToolFactory { get; private set; }

        /// <summary>
        ///     Gets or sets the last fetched folder.
        /// </summary>
        /// <value>The last fetched folder.</value>
        public string LastFetchedFolder { get; set; }

        /// <summary>
        ///     Gets or sets the last fetched local eit files.
        /// </summary>
        /// <value>The last fetched local eit files.</value>
        public List<EITFormatDisplay> LastFetchedLocalEitFiles { get; set; }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; private set; }

        /// <summary>
        ///     A method for testing, that throws an exception.
        /// </summary>
        /// <exception cref="TargetException">Test Exception.</exception>
        /// <exception cref="System.Reflection.TargetException">One to rule them all ... Test Exception.</exception>
        public static void ThrowSomething()
        {
            throw new TargetException("One to rule them all ... Test Exception.");
        }

        /// <summary>
        /// Asks the user about a file system path.
        /// </summary>
        /// <returns>the path to copy to.</returns>
        private static string AskCopyPath()
        {
            var destinationFolder = string.Empty;
            using (var fdia = new FolderBrowserDialog())
            {
                const string HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempValueName =
                    "MoveFolderRootFolder";
                var browseStartPath =
                    Registry.GetValue(
                        HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName,
                        HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempValueName,
                        string.Empty);
                if (browseStartPath != null)
                {
                    fdia.SelectedPath = browseStartPath.ToString();
                }

                var result = fdia.ShowDialog();
                if (result != DialogResult.OK || string.IsNullOrWhiteSpace(fdia.SelectedPath))
                {
                    // this.Logger.Warn("Cancelled " + actionVerb.Derived + " operation.");
                    return destinationFolder;
                }

                destinationFolder = fdia.SelectedPath;

                // this.GetLocalEitFilesFromPath(fdia.SelectedPath, this.btnFetchFolder);
                Registry.SetValue(
                    HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName,
                    HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempValueName,
                    fdia.SelectedPath);
            }

            return destinationFolder;
        }

        /// <summary>
        /// Formats the duplication message about to EITFormatDisplay's.
        /// </summary>
        /// <param name="formatDisplay">
        /// The format display (other).
        /// </param>
        /// <param name="formatDisplayB">
        /// The format display (original).
        /// </param>
        /// <returns>
        /// a message that informs about the duplication.
        /// </returns>
        private static string FormatDuplicateMessage(EITFormatDisplay formatDisplay, EITFormatDisplay formatDisplayB)
        {
            var s = new StringBuilder();
            s.AppendLine(string.Format("Has a duplicate in '{0}'.", formatDisplay.Filename));
            s.AppendLine(string.Format("'{0}'.", formatDisplay.EventType));
            s.AppendLine(string.Format("'{0}'.", formatDisplay.Beschreibung));

            var result = s.ToString();
            return result;
        }

        /// <summary>
        /// Adds to eit comparer <c>async</c>.
        /// </summary>
        /// <param name="videoBasePath">
        /// The video base path.
        /// </param>
        private void AddToEitComparerAsync(string videoBasePath)
        {
            this.Logger.Info("Fetching Overview Directories from '" + videoBasePath + "'");

            // button.Enabled = false;

            // Todo: remove the colouring process from the EitListFetcher.
            var getData = new Task<List<EITFormatDisplay>>(() => this.eitFetcher.GetLocalEitFiles(videoBasePath));
            getData.ContinueWith(
                x =>
                    {
                        /*if (initialAction != null)
                    {
                        initialAction(x.Result);
                    }*/

                        // this.eitColorer.Apply(x.Result);
                        // this.eitPictureChooser.Apply(x.Result);
                        // this.LastFetchedLocalEitFiles = x.Result;
                        // this.eITFormatDisplayBindingSource.DataSource = x.Result;
                        // button.Enabled = true;
                        if (this.eitComparer.Set(videoBasePath, x.Result))
                        {
                            this.lbEitFiles.Items.Add(videoBasePath);
                        }
                    },
                TaskScheduler.FromCurrentSynchronizationContext());

            getData.Start();
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
            const string VideoBasePath = Globals.FsWatchBasePath;
            var result = this.eitFetcher.GetLocalEitFilesThatAreDone(VideoBasePath);
            var enigmaFileProcessor = this.BladeToolFactory.CreateTool<EnigmaFileProcessor>(
                Globals.FsVideoBasePath,
                FtpUserName,
                FtpPassword,
                VideoOverviewUI.AskCopyPath);

            enigmaFileProcessor.DeleteFiles(result);
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
            // this.LastFetchedFolder = null;
            // this.LastFetchedLocalEitFiles = null;
            this.eitComparer.Clear();
            this.lbEitFiles.Items.Clear();
            RegistryHelper.SetRegDictEntryForRootFolders(
                HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName,
                HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempFetchFolders,
                string.Empty);
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
            var lastFetchedFolder = this.LastFetchedFolder;
            if (!this.eitComparer.Set(lastFetchedFolder, this.LastFetchedLocalEitFiles))
            {
                return;
            }

            this.lbEitFiles.Items.Add(lastFetchedFolder);

            Dictionary<string, bool> browseDict;
            RegistryHelper.AddRegDictEntryForRootFolders(
                lastFetchedFolder,
                out browseDict,
                HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName,
                HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempFetchFolders);
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
            this.GetLocalEitFilesFromPath(
                Globals.FsVideoBasePath,
                this.btnFetch,
                x => this.eitComparer.Compare(x, this.GroupEitForColoring, VideoOverviewUI.FormatDuplicateMessage));
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
            using (var fdia = new FolderBrowserDialog())
            {
                const string HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempValueName =
                    "FetchFolderRootFolder";
                var browseStartPath =
                    Registry.GetValue(
                        HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName,
                        HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempValueName,
                        string.Empty);
                if (browseStartPath != null)
                {
                    fdia.SelectedPath = browseStartPath.ToString();
                }

                var result = fdia.ShowDialog();
                if (result != DialogResult.OK || string.IsNullOrWhiteSpace(fdia.SelectedPath))
                {
                    return;
                }

                this.LastFetchedFolder = fdia.SelectedPath;
                this.GetLocalEitFilesFromPath(fdia.SelectedPath, this.btnFetchFolder);
                Registry.SetValue(
                    HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName,
                    HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempValueName,
                    fdia.SelectedPath);
            }
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
            // ftp://root@192.168.3.43/media/hdd/movie/20140122%201131%20-%20ProSieben%20MAXX%20-%20Stargate.ts.sc
            this.Logger.Info("Fetching Overview Directories from 'ftp://" + FtpHost + FtpEitBasePath + "'");

            this.btnFetchFtp.Enabled = false;
            var getData =
                new Task<List<EITFormatDisplay>>(
                    () => this.eitFetcher.GetFtpEitFiles(FtpHost, FtpUserName, FtpPassword, FtpEitBasePath));
            getData.ContinueWith(
                x =>
                    {
                        this.eitColorer.Apply(x.Result);
                        this.eitComparer.Compare(
                            x.Result,
                            this.GroupEitForColoring,
                            VideoOverviewUI.FormatDuplicateMessage);
                        this.eITFormatDisplayBindingSource.DataSource = x.Result;
                        this.btnFetchFtp.Enabled = true;
                    },
                TaskScheduler.FromCurrentSynchronizationContext());

            getData.Start();
        }

        private void BtnFixClick(object sender, EventArgs e)
        {
            ////const string VideoBasePath = Globals.FsWatchBasePath;
            // var result = this.eitFetcher.GetLocalEitFilesThatAreDone(VideoBasePath);
            var result = this.eitFetcher.GetAllFtpFiles(FtpHost, FtpUserName, FtpPassword, FtpEitBasePath);
            var enigmaFileProcessor = this.BladeToolFactory.CreateTool<EnigmaFileProcessor>(
                Globals.FsVideoBasePath,
                FtpUserName,
                FtpPassword,
                VideoOverviewUI.AskCopyPath);
            this.EnigmaFileProcessorSetProgress(enigmaFileProcessor);

            var missing = enigmaFileProcessor.CheckForMissingEit(result);
            enigmaFileProcessor.MoveFiles(new[] { missing.First() });
            
            // enigmaFileProcessor.MoveFiles(missing);
            // HandleSingleUriCopyMove
            // reconstruct eit ?
            // ToDo: enigmaFileProcessor.MoveFilesReconstructEit
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
            var executablePath = Application.ExecutablePath;
            var fi = new FileInfo(executablePath);
            if (fi.DirectoryName != null)
            {
                var layoutfile = Path.Combine(fi.DirectoryName, this.GetType().FullName + ".layout.xml");
                this.gridControl1.DefaultView.SaveLayoutToXml(layoutfile);
            }
            else
            {
                this.Logger.Error("DirectoryName of '" + executablePath + "' is null.");
                this.host.DisplayStatus(new StatusData("Bla!"));
            }
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
            var filt = this.gridView1.ActiveFilter;
            var crit = filt.Criteria;
            this.TestCrit2(crit);
        }

        private void EnigmaFileProcessorSetProgress(EnigmaFileProcessor enigmaFileProcessor)
        {
            enigmaFileProcessor.Progress += (o, args) =>
                {
                    if (args.Length > int.MaxValue)
                    {
                        // Todo: switch to a dividable of args ( this.pbProgress.Properties.Maximum is an int value )
                        return;
                    }

                    if (Convert.ToInt64(this.pbProgress.EditValue) == args.Position)
                    {
                        return;
                    }

                    this.pbProgress.IvReq(
                        () =>
                            {
                                this.pbProgress.Properties.Maximum = (int)args.Length;
                                this.pbProgress.EditValue = args.Position;
                            });
                };
        }

        /// <summary>
        /// Gets the local eit files from path.
        /// </summary>
        /// <param name="videoBasePath">The video base path.</param>
        /// <param name="button">The button.</param>
        /// <param name="initialAction">The initial action to do before colorer, picture chooser, etc. is run.</param>
        private void GetLocalEitFilesFromPath(
            string videoBasePath,
            Button button,
            Action<List<EITFormatDisplay>> initialAction = null)
        {
            this.Logger.Info("Fetching Overview Directories from '" + videoBasePath + "'");
            button.Enabled = false;

            // Todo: remove the colouring process from the EitListFetcher.
            var getData = new Task<List<EITFormatDisplay>>(() => this.eitFetcher.GetLocalEitFiles(videoBasePath));
            getData.ContinueWith(
                x =>
                    {
                        if (initialAction != null)
                        {
                            initialAction(x.Result);
                        }

                        this.eitColorer.Apply(x.Result);
                        this.eitPictureChooser.Apply(x.Result);
                        this.LastFetchedLocalEitFiles = x.Result;
                        this.eITFormatDisplayBindingSource.DataSource = x.Result;
                        button.Enabled = true;
                    },
                TaskScheduler.FromCurrentSynchronizationContext());

            getData.Start();
        }

        /// <summary>
        /// Handles the CustomFilterDialog event of the gridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CustomFilterDialogEventArgs"/> instance containing the event data.</param>
        private void GridView1CustomFilterDialog(object sender, CustomFilterDialogEventArgs e)
        {
        }

        /// <summary>
        /// Grids the view1 row style.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RowStyleEventArgs" /> instance containing the event data.</param>
        private void GridView1RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (view == null || e.RowHandle < 0)
            {
                return;
            }

            var displayText = view.GetRowCellDisplayText(e.RowHandle, view.Columns[this.colColor.FieldName]);
            if (string.IsNullOrWhiteSpace(displayText))
            {
                return;
            }

            int argbval;
            var parseOk = int.TryParse(displayText, out argbval);

            if (parseOk)
            {
                e.Appearance.BackColor2 = Color.FromArgb(argbval);
                e.Appearance.BackColor = Color.SeaShell;
            }
        }

        /// <summary>
        /// The function that groups the eit list for coloring.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>a shortened description string.</returns>
        private string GroupEitForColoring(EITFormatDisplay items)
        {
            const int CompareLen = 80;

            // var teststr = items.EventName.Trim() + " " + items.EventType.Trim() + " " + items.Beschreibung.Trim().Soundex();
            var containsWdh = items.Beschreibung.StartsWith("Wiederholung");
            var beschreibung = items.Beschreibung;
            var beschreibung80 = items.Beschreibung;
            if (containsWdh)
            {
                var f = items.Beschreibung.IndexOf(".", StringComparison.Ordinal);
                beschreibung = items.Beschreibung.TruncateLongString(beschreibung.Length, f + 2);
                beschreibung80 = items.Beschreibung.TruncateLongString(CompareLen, f + 2);
            }

            if (this.cbSoundex.Checked)
            {
                var teststr = items.EventName.Trim() + " " + beschreibung.Trim().Soundex();

                // var teststr = items.EventName.Trim() + " " + items.EventType.Trim() + " " + beschreibung.Trim().Soundex();
                return teststr;
            }

            return !containsWdh ? items.Beschreibung.TruncateLongString(CompareLen) : beschreibung80;
        }

        private void InitDisplayTypeComboBox()
        {
            this.imageList1.Images.Add(Icons64.link);
            this.imageList1.Images.Add(Icons64.gnome_folder);
            this.imageList1.Images.Add(Icons64.documents);
            this.imageList1.Images.Add(Icons64.exec);
            this.repositoryItemImageComboBox1.Items.AddEnum<EitDisplayState>();
        }

        /// <summary>
        /// Initializes the grid menu run actions.
        /// </summary>
        /// <param name="gmenu">The grid menu.</param>
        /// <param name="bladeToolFactory">The blade tool factory.</param>
        private void InitGridMenuRunActions(
            VideoOverviewUiGridMenu<EITFormatDisplay> gmenu,
            IBladeToolFactory bladeToolFactory)
        {
            this.pbProgress.EditValue = 0L;
            var enigmaFileProcessor = bladeToolFactory.CreateTool<EnigmaFileProcessor>(
                Globals.FsVideoBasePath,
                FtpUserName,
                FtpPassword,
                VideoOverviewUI.AskCopyPath);
            this.EnigmaFileProcessorSetProgress(enigmaFileProcessor);

            gmenu.AddAction(
                "Open",
                delegate(IEnumerable<EITFormatDisplay> items, string s)
                    {
                        var firstOrDefault = items.FirstOrDefault();
                        if (firstOrDefault == null)
                        {
                            return;
                        }

                        var filename = firstOrDefault.Filename;

                        // var filepath = Path.GetFullPath(filename);
                        // var filewoext = Path.GetFileNameWithoutExtension(filename);
                        // var newfile = Path.Combine(filepath, filewoext, ".ts");
                        foreach (var newfile in
                            new List<string> { ".ts", ".mkv", ".avi" }.Select(
                                ext => Path.ChangeExtension(filename, ext)).Where(File.Exists))
                        {
                            FileStarter.Execute(newfile);
                            return;
                        }
                    });
            gmenu.AddAction(
                "Copy",
                (items, s) => enigmaFileProcessor.CopyFiles(items.Select(eitItem => eitItem.Filename)));
            gmenu.AddAction(
                "Move",
                (items, s) => enigmaFileProcessor.MoveFiles(items.Select(eitItem => eitItem.Filename)));
            gmenu.AddAction(
                "Delete",
                (items, s) => enigmaFileProcessor.DeleteFiles(items.Select(eitItem => eitItem.Filename)));
        }

        /// <summary>
        /// Loads the saved comparer's.
        /// </summary>
        private void LoadSavedComparers()
        {
            var serializer = new DataContractSerializer(typeof(Dictionary<string, bool>));
            Dictionary<string, bool> browseDict;
            if (
                !RegistryHelper.GetRegDictForRootFolders(
                    HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempKeyName,
                    HkeyCurrentUserSoftwareEvepanixDeveknifeBladesOverviewTempFetchFolders,
                    serializer,
                    out browseDict))
            {
                return;
            }

            foreach (var b in browseDict)
            {
                this.AddToEitComparerAsync(b.Key);
            }
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
        private void MnuCountClearTextClick(object sender, EventArgs e)
        {
            this.tbOverviewLog.Clear();
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
            this.tbOverviewLog.SelectionStart = this.tbOverviewLog.Text.Length;
            this.tbOverviewLog.ScrollToCaret();
            this.tbOverviewLog.SelectionLength = 0;
        }
    }
}