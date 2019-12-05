//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParameterParser.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.12.2013 19:04</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule
{
    using System;
    using System.IO;

    using IniParser;
    using IniParser.Model;

    public class ParameterParser
    {
        private readonly DirectoryInfo directory;

        private DateTime timeStamp = DateTime.MinValue;

        public ParameterParser(string directory)
        {
            this.directory = new DirectoryInfo(directory);
        }

        public ushort AudioBitrate { get; private set; }

        public string AudioOptions { get; private set; }

        public string Encoder { get; private set; }

        public bool IsValid { get; private set; }

        public string Preset { get; private set; }

        public ushort VideoBitrate { get; private set; }

        public string VideoOptions { get; private set; }

        /// <summary>
        /// Fetches and checks the parameters of the directory.
        /// </summary>
        /// <exception cref="System.IO.FileNotFoundException">
        /// No 'encode.ini'
        /// <see cref="Deveknife.Blades.RecodeMule.ParameterParser.Encoder" />
        /// parameter definitions are present in the directory.
        /// </exception>
        public void Fetch()
        {
            var files = this.directory.GetFiles("encode.ini");
            if (files.Length != 1)
            {
                throw new FileNotFoundException(
                    "No 'encode.ini' Encoder parameter definitions are present in '" + this.directory.FullName + "'");
            }
            var inifile = files[0];
            // Todo: check for changes with the timeStamp
            if (inifile.LastWriteTimeUtc <= this.timeStamp)
            {
                return;
            }

            this.IsValid = false;
            var parser = new FileIniDataParser();
            // Load ini file
            //var data = parser.LoadFile(inifile.FullName);
            var data = parser.ReadFile(inifile.FullName);

            // Retrieve the value for the key with name 'fullscreen' inside a config section named 'ConfigSection'
            // values are always retrieved as an string
            if (data.Sections.ContainsSection("General"))
            {
                var gs = data["General"];

                this.Encoder = gs.Gk("Encoder");
                this.Passes = ushort.Parse(gs.Gk("Passes"));

                this.Preset = gs.Gk("Preset");

                this.FrameRate = gs.Gk("FrameRate");
                this.VideoOptions = gs.Gk("VideoOptions");
                //this.VideoBitrate = gs.Gk("VideoBitrate");
                this.VideoBitrate = ushort.Parse(gs.Gk("VideoBitrate"));

                this.AudioOptions = gs.Gk("AudioOptions");
                //this.AudioBitrate = gs.Gk("AudioBitrate");
                this.AudioBitrate = ushort.Parse(gs.Gk("AudioBitrate"));

                /*this.Encoder = generalSection["Encoder"].Trim('\"');
                this.VideoOptions = generalSection["VideoOptions"].Trim('\"');
                this.VideoBitrate = generalSection["VideoBitrate"].Trim('\"');
                this.AudioOptions = generalSection["AudioOptions"].Trim('\"');
                this.AudioBitrate = generalSection["AudioBitrate"].Trim('\"');*/

                this.timeStamp = inifile.LastWriteTimeUtc;

                this.IsValid = true;
            }
        }

        public string FrameRate { get; private set; }

        public ushort Passes { get; private set; }
    }

    public static class ParameterParserExtensions
    {
        public static string Gk(this KeyDataCollection keyData, string key)
        {
            var result = string.Empty;
            if (keyData.ContainsKey(key))
            {
                result = keyData[key];
                result = result.Trim('\"');
            }
            return result;
        }
    }
}