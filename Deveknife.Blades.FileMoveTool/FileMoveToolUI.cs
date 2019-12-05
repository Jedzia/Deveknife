// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileMoveToolUI.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.11.2016 15:14</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileMoveTool
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using SystemInterface.IO;

    using Deveknife.Api;
    using Deveknife.Blades.FileMoveTool.Filesystem;
    using Deveknife.Blades.FileMoveTool.Util;

    using Castle.Core.Logging;

    /*

    To use logging add

        <target name="control4" xsi:type="FormControl" append="true"
            layout="${date:format=MM-dd-yyyy HH\:mm\:ss} [${uppercase:${level}}] ${message}${newline}"
        controlName="tbFileMoveToolLog" formName="MainForm"/>

    to the NLog <targets> section and

        <logger name="Deveknife.Blades.FileMoveTool.*" minlevel="Info" writeTo="control4" />

    to the <rules> section, where you have to adjust the name of the target (here=control4) to your suits.

    */

    /// <summary>
    ///     The user-interface of the  Video-Overview tool.
    /// </summary>
    public partial class FileMoveToolUI : UserControl, IBladeUI
    {
        /*/// <summary>
        ///     The fixed filter
        /// </summary>
        private readonly ITextFilter fixedFilter = new FixedPathFilter();*/

        /// <summary>
        ///     The host
        /// </summary>
        [UsedImplicitly]
        private readonly IHost host;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileMoveToolUI" /> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="bladeToolFactory">The blade tool factory.</param>
        /// <param name="inoutService">The io service.</param>
        /// <param name="dialogService">The dialog service.</param>
        public FileMoveToolUI(
            IHost host,
            ILogger logger,
            IBladeToolFactory bladeToolFactory,
            IOService inoutService,
            IDialogService dialogService)
            : this()
        {
            this.host = Guard.NotNull(() => host, host);
            Guard.NotNull(() => logger, logger);
            this.Logger = logger.CreateChildLogger("FileMoveToolLogger");
            this.BladeToolFactory = Guard.NotNull(() => bladeToolFactory, bladeToolFactory);

            Guard.NotNull(() => inoutService, inoutService);
            //// Todo: if this is getting too much property fields, store the WrappedIOServiceFactory or heavier, the IOService.
            this.DirectoryInfoFactory = inoutService.GetWrappedIOServiceFactory().CreateDirectoryInfoFactory();
            this.FileInfoFactory = inoutService.GetWrappedIOServiceFactory().CreateFileInfoFactory();

            Guard.NotNull(() => dialogService, dialogService);
            this.buttonEditFolderA.DialogService = dialogService;
            this.buttonEditFolderB.DialogService = dialogService;

            // Devel: Values for development, remove after testing.
            this.buttonEditFolderA.EditValue = @"D:\Users\Jedzia.pubsiX\AppData\Roaming\MAXON\CINEMA 4D R14_4A9E4467";
            this.buttonEditFolderB.EditValue = @"D:\Users\Jedzia.pubsiX\AppData\Roaming\MAXON\CINEMA 4D R16_B9954B53";
            ////this.buttonEditFolderB.EditValue = @"D:\Users\Jedzia.pubsiX\AppData\Roaming\MAXON\CINEMA 4D R18_62A5E681";


            // this.eitPictureChooser = new EitListPictureChooser(logger, eit => !this.fixedFilter.Contains(eit.Filename));
            // this.eitColorer = new EitListColorer(
            // logger, 
            // this.GroupEitForColoringFunc, 
            // eit => !this.fixedFilter.Contains(eit.Filename));
            // this.eitFetcher = new EitListFetcher(logger);
            // this.InitGridMenuRunActions(
            // new FileMoveToolUiGridMenu<EITFormatDisplay>(this.gridView1), 
            // this.BladeToolFactory);

            // var ftpClient = new FtpClient();
            // var bladeToolA = this.BladeToolFactory.CreateTool<Dingens>();
            // var bladeToolB = this.BladeToolFactory.CreateTool<FtpLister>(ftpClient);
            // var bladeToolB = this.BladeToolFactory.CreateTool<CachedFtpLister>(ftpClient);
            // var bladeTools = this.BladeToolFactory.CreateAll(ftpClient);
            this.LoadSavedComparisons();
        }

        /// <summary>
        ///     Prevents a default instance of the <see cref="FileMoveToolUI" /> class from being created.
        /// </summary>
        private FileMoveToolUI()
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
                return "FileMoveTool Module <Rename Me>";
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

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; private set; }

        /// <summary>
        /// A method for testing, that throws an exception.
        /// </summary>
        /// <exception cref="Deveknife.Blades.FileMoveTool.FileMoveToolException">One to rule them all ... Test Exception.</exception>
        public static void ThrowSomething()
        {
            throw new FileMoveToolException("One to rule them all ... Test Exception.");
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
        private void BtnCheckClick(object sender, EventArgs e)
        {
            ////var todoMessage = string.Format("<Exec-Compare> Folder '{0}' against Folder '{1}'.", this.buttonEditFolderA.EditValue, this.buttonEditFolderB.EditValue);
            ////this.Logger.Info(todoMessage);

            var checker = new FileChecker(this.Logger, this.DirectoryInfoFactory, this.FileInfoFactory);
            ////checker.CreateSymbolicLinkedContent(this.buttonEditFolderB.EditValue as string, this.buttonEditFolderA.EditValue as string);
            checker.MoveSymbolicLinkedContent(this.buttonEditFolderB.EditValue as string, this.buttonEditFolderA.EditValue as string);
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
            // result "..\\A\\symsource.txt" string
            var pathSym = @"D:\Temp\Accel\B\symsource.txt";
            
            // result null
            var path = @"D:\Temp\Accel\B\RootFileA_4.txt";

            // result "\\Temp\\Accel\\A\\JunctionSource\0\0"
            var pathHard = @"D:\Temp\Accel\B\Junction";

            // result "D:\\Temp\\Accel\\A\\JunctionSource"
            var pathJunc = @"D:\Temp\Accel\B\Junction";

            var resultPath = SymbolicLink.GetTarget(path);
            var resultSyml = SymbolicLink.GetTarget(pathSym);
            //// for hardlinks look at: http://www.flexhex.com/docs/articles/hard-links.phtml and http://stackoverflow.com/questions/4193309/list-hard-links-of-a-file-in-c
            var resultHard = Hardlink.GetTarget(pathHard);
            var resultJunc = JunctionPoint.GetTarget(pathJunc);
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

        /// <summary>
        /// Handles the Load event of the FileMoveToolUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FileMoveToolUILoad([NotNull] object sender, [NotNull] EventArgs e)
        {
            this.tbFileMoveToolLog.Clear();
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
        private void InitGridMenuRunActions(FileMoveToolUiGridMenu<Dummy> gmenu, IBladeToolFactory bladeToolFactory)
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
            this.tbFileMoveToolLog.Clear();
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
        private void TbFileMoveToolLogTextChanged(object sender, EventArgs e)
        {
            this.tbFileMoveToolLog.SelectionStart = this.tbFileMoveToolLog.Text.Length;
            this.tbFileMoveToolLog.ScrollToCaret();
            this.tbFileMoveToolLog.SelectionLength = 0;
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