// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace EitView
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;

    using Deveknife.Blades.Overview.Eit.Formats;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// A newline constant. Equal to <see cref="Environment.NewLine"/>.
        /// </summary>
        private static readonly string Nl = Environment.NewLine;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var args = Environment.GetCommandLineArgs();

            // var passArguments = string.Empty;
            var files = new List<string>();

            // Blend debugging helper
            // files.Add(@"E:\Shared\Video\Q\20150101 0801 - WDR HD Köln - Luzie, der Schrecken der Straße (6_6).eit");
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (i > 0)
                {
                    var finfo = new FileInfo(arg);
                    if (finfo.Extension == ".eit")
                    {
                        files.Add(arg);
                    }
                }

                var text = string.Format("{0}:{1}", i, arg);
                this.textBox1.AppendText(text);
                this.textBox1.AppendText(Environment.NewLine);
            }

            // var processPath = @"D:\Program Files\Autodesk\Maya2014\bin\maya.exe";
            // this.textBox1.AppendText(Environment.NewLine);
            // this.textBox1.AppendText("starting '" + processPath + "' with \"" + passArguments + "\".");
            
            // this.ParseEitFiles(files);
            this.ParseEitFilesHtml(files);
        }

        private void ParseEitFiles(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                var eit = new EITFormat();
                eit.OpenFile(file);

                this.textBox1.AppendText(eit.EventName);
                this.textBox1.AppendText(Environment.NewLine);
                this.textBox1.AppendText(Environment.NewLine);

                this.textBox1.AppendText(eit.Beschreibung);
                this.textBox1.AppendText(Environment.NewLine);
                this.textBox1.AppendText(
                    "--------------------------------------------------------------------------------------");
                this.textBox1.AppendText(Environment.NewLine);
            }
        }

        private void ParseEitFilesHtml(IEnumerable<string> files)
        {
            foreach (var file in files)
            {
                var eit = new EITFormat();
                eit.OpenFile(file);

                var text = eit.EventName + Nl;
                this.rtb.AppendBoldText(text, Brushes.Blue);
                text = eit.EventType + Nl;
                this.rtb.AppendNormalText(text, Brushes.Blue);

                text = eit.Zeit + "   Länge: " + eit.Dauer + Nl + Nl;
                this.rtb.AppendNormalText(text, Brushes.Brown);

                text = eit.Beschreibung + Nl;
                this.rtb.AppendNormalText(text);

                text = "--------------------------------------------------------------------------------------"
                       + Nl;
                this.rtb.AppendNormalText(text, Brushes.Red);

                /*tr = new TextRange(this.rtb.Document.ContentEnd, this.rtb.Document.ContentEnd);
                sb.Clear();
                sb.Append(eit.Beschreibung);
                sb.Append(Environment.NewLine);
                tr.Text = sb.ToString();
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
                tr.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);

                tr = new TextRange(this.rtb.Document.ContentEnd, this.rtb.Document.ContentEnd);
                sb.Clear();
                sb.Append("--------------------------------------------------------------------------------------");
                sb.Append(Environment.NewLine);
                tr.Text = sb.ToString();
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
                tr.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);*/
            }
        }
    }
}