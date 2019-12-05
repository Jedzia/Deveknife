// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EITFormatDisplay.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>21.02.2014 11:47</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System;
    using System.Drawing;

    using Deveknife.Blades.Overview.Eit.Formats;

    public class EITFormatDisplay
    {
        // private EITFormat data;
        public EITFormatDisplay(EITFormat data)
        {
            Guard.NotNull(() => data, data);
            this.Audio = data.Audio;
            this.Beschreibung = data.Beschreibung;
            this.Dauer = data.Dauer;
            this.EventLanguage = data.EventLanguage;
            this.EventName = data.EventName;
            this.EventPicture = data.EventPicture;
            this.EventType = data.EventType;
            this.FileSize = data.FileSize;
            this.HDAudio = data.HDAudio;
            this.HDVideo = data.HDVideo;
            this.VPid = data.VPid;
            this.Verschluesselung = data.Verschluesselung;
            this.Zeit = data.Zeit;
        }

        public int AnzahlByte { get; set; }

        public Array Audio { get; set; }

        public string Beschreibung { get; set; }

        public bool BigEndian { get; set; }

        public int Color { get; set; }

        public string Dauer { get; set; }

        public string EventLanguage { get; set; }

        public string EventName { get; set; }
        
        public Image Icon { get; set; }

        public string EventPicture { get; set; }

        public string EventType { get; set; }

        public int FileSize { get; set; }

        public string Filename { get; set; }
       
        public string Test { get; set; }

        public bool HDAudio { get; set; }

        public bool HDVideo { get; set; }

        public string Note { get; set; }

        public object Tag { get; set; }

        public string VPid { get; set; }

        public int Verschluesselung { get; set; }

        public string Zeit { get; set; }

        ////public bool IsFixed { get; set; }
        
        public EitDisplayState DisplayState { get; set; }
    }
}