//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RecoderUI.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>12.02.2014 08:46</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    using Deveknife.Api;
    using Deveknife.Blades.Utils.Filters;

    using Castle.Core.Logging;

    /// <summary>
    /// Class <see cref="RecoderUI" />
    /// </summary>
    public partial class RecoderUI : UserControl, IBladeUI
    {
        private const bool StartUpRunning = false;

        private readonly IHost host;

        private readonly ITextFilter outFileFilter = new ChangeDirectoryFilter(
            new FixedPathFilter(), Globals.VideoOutPath);

        private readonly Recoder recoder;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoderUI" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="recoder">The recoder.</param>
        public RecoderUI(IHost host, ILogger logger, Recoder recoder)
            : this()
        {
            Guard.NotNull(() => host, host);
            this.host = host;
            Guard.NotNull(() => logger, logger);
            this.Logger = logger;
            Guard.NotNull(() => recoder, recoder);
            this.recoder = recoder;
            recoder.EncoderChanged += this.RecoderOnStateChanged;
        }

        /// <summary>
        /// Gets the name of the <see cref="IBlade" />.
        /// </summary>
        /// <value>The name.</value>
        public string BladeName
        {
            get
            {
                return "Recode Mule";
            }
        }

        public IBlade Blade { get; internal set; }

        /// <summary>
        /// Handles the EncoderChanged event of the recoder.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="RecoderStateChangedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="ArgumentOutOfRangeException">A not recognized RecoderStateChangedAction occured.</exception>
        private void RecoderOnStateChanged(object sender, RecoderStateChangedEventArgs args)
        {
            switch (args.Action)
            {
                case RecoderStateChangedAction.Enqueued:
                    this.host.NotifyWorkItem(this.Blade, args.Item, WorkItemAction.Enqueued);
                    break;
                case RecoderStateChangedAction.Dequeued:
                    break;
                case RecoderStateChangedAction.Successful:
                case RecoderStateChangedAction.UnSuccessful:
                    this.host.NotifyWorkItem(this.Blade, args.Item, WorkItemAction.Dequeued);
                    break;
                case RecoderStateChangedAction.Cancelled:
                    this.host.NotifyWorkItem(this.Blade, args.Item, WorkItemAction.Cancelled);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("args", "A not recognized RecoderStateChangedAction occured.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecoderUI" /> class.
        /// </summary>
        private RecoderUI()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the logging text box.
        /// </summary>
        /// <value>
        /// The log box.
        /// </value>
        public TextBox LogBox
        {
            get
            {
                return this.tbLog;
            }
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; private set; }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// <see langword="true" /> if managed resources should be disposed;
        /// otherwise, false.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Logger.Info("Disposing Recoder.");
                this.Logger.Info("------------------------------------------------------------------------------------");
                this.recoder.Stop();
                while (this.recoder.IsRunning)
                {
                    Thread.Sleep(2000);
                    this.Logger.Info("Disposing Recoder ... waiting 2000ms.");
                }
                recoder.EncoderChanged -= this.RecoderOnStateChanged;

                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private static string[] GetWatcherPaths()
        {
            var di = new DirectoryInfo(Globals.FsWatchBasePath);
            var paths = di.GetDirectories().Select(dir => dir.FullName).ToArray();
            return paths;
        }

        /// <summary>
        /// Handles the Click event of the bnAddDirectory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void BnAddDirectoryClick(object sender, EventArgs e)
        {
            this.lbDirectories.Items.Add("Blafasel");
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void Button1Click(object sender, EventArgs e)
        {
            if (this.recoder != null)
            {
                var paths = GetWatcherPaths();
                foreach (var path in paths)
                {
                    this.recoder.Enqueue(path);
                }
                //this.recoder.Enqueue(FsWatchPath);
            }
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void Button2Click(object sender, EventArgs e)
        {
            this.StartWatcher(Globals.FsWatchPath2, this.outFileFilter);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the checkBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void CheckBox1CheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            if (cb == null)
            {
                return;
            }

            if (cb.CheckState == CheckState.Checked)
            {
                cb.Text = "Stop";

                // ITextFilter outFileFilter = new ChangeDirectoryFilter(new FixedPathFilter(), VideoOutPath);

                var paths = GetWatcherPaths();
                //var paths = FsWatchPaths;
                foreach (var path in paths)
                {
                    this.StartWatcher(path, this.outFileFilter);
                }
            }
            else
            {
                cb.Text = "Run";
                this.StopWatcher();
            }
        }

        /// <summary>
        /// Handles the Load event of the <c>RecoderUI</c> control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void Form1Load(object sender, EventArgs e)
        {
            //this.recoder.Host = this;
            this.recoder.Message += this.RecoderMessage;
            this.Logger.Info("------------------------------------------------------------------------------------");
            this.Logger.Info("Starting RecoderUI.");
            if (StartUpRunning)
            {
                this.checkBox1.CheckState = CheckState.Checked;
            }

            this.host.StatusChanged += (o, data) => this.IvReq(() => this.tbStatus.Text = data.Text);
        }

        /// <summary>
        /// Handles the Message event of the recorder.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void RecoderMessage(object sender, RecoderMessage e)
        {
            this.tbLog.IvReq(
                () =>
                    {
                        this.tbLog.AppendText(e.Message);
                        this.tbLog.AppendText(Environment.NewLine);
                    });
        }

        /// <summary>
        /// Starts the watcher.
        /// </summary>
        /// <param name="directoryPath">The directory path.</param>
        /// <param name="fileFilter">The file filter.</param>
        private void StartWatcher(string directoryPath, ITextFilter fileFilter)
        {
            this.lbDirectories.Items.Add(directoryPath);
            this.recoder.Start(directoryPath, fileFilter);
        }

        /// <summary>
        /// Stops the watcher.
        /// </summary>
        private void StopWatcher()
        {
            this.lbDirectories.Items.Clear();
            this.recoder.Stop();
        }

        /// <summary>
        /// Handles the TextChanged event of the tbLog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.EventArgs" /> instance containing the event
        /// data.
        /// </param>
        private void TbLogTextChanged(object sender, EventArgs e)
        {
            this.tbLog.SelectionStart = this.tbLog.Text.Length;
            this.tbLog.ScrollToCaret();
            this.tbLog.SelectionLength = 0;
        }

        /// <summary>
        /// Handles the Click event of the mnuCntClearText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void mnuCntClearText_Click(object sender, EventArgs e)
        {
            tbLog.Clear();
        }
    }
}