//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EITFormat.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>11.02.2014 11:36</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Eit.Formats
{
    //using EITitor.My;
    using System;
    using System.IO;
    using System.Text;

    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    public class EITFormat
    {
        public string[,] parseEventData = new string[0x15,2];

        public byte[] streamdata = new byte[0x1001];

        private int audioCount;

        private byte dLoopLenghtLo;

        private ushort dLoopLength;

        private byte dLoopLengthHi;

        private string[,] eventAudio = new string[4,4];

        private string eventDescription = "";

        private string eventDuration = "";

        private byte eventIdHi;

        private byte eventIdLo;

        private string eventStartzeit = "";

        private byte runningStatus;

        public EITFormat()
        {
            this.EventType = "";
            this.EventLanguage = "";
            this.EventName = "";
            this.EventPicture = "";
        }

        public Array Audio
        {
            get
            {
                return this.eventAudio;
            }
            set
            {
                this.eventAudio = (string[,])value;
            }
        }

        public string Beschreibung
        {
            get
            {
                return this.eventDescription;
            }
            set
            {
                this.eventDescription = EITStringHelper.STrim(value);
            }
        }

        public string Dauer
        {
            get
            {
                return this.eventDuration;
            }
            set
            {
                this.eventDuration = EITStringHelper.STrim(value);
                if (this.eventDuration.Length > 8)
                {
                    this.eventDuration = Strings.Right(this.eventDuration, 8);
                }
            }
        }

        public string EventLanguage { get; set; }

        public string EventName { get; set; }

        public string EventPicture { get; set; }

        public string EventType { get; set; }

        public int FileSize { get; set; }

        public string VPid { get; set; }

        public int Verschluesselung { get; set; }

        public string Zeit
        {
            get
            {
                return this.eventStartzeit;
            }
            set
            {
                this.eventStartzeit = EITStringHelper.STrim(value);
            }
        }

        public bool HDAudio { get; set; }

        public bool HDVideo { get; set; }

        protected int AnzahlByte { get; set; }

        protected bool BigEndian { get; set; }

        public void Clear()
        {
            this.eventStartzeit = "";
            this.eventDuration = "";
            this.EventLanguage = "";
            this.EventName = "";
            this.eventDescription = "";
            this.runningStatus = 0;
            this.Verschluesselung = 0;
            this.dLoopLengthHi = 0;
            this.dLoopLenghtLo = 0;
            this.dLoopLength = 0;
            this.EventType = "";
            this.eventAudio = new string[4,4];
            this.EventPicture = "";
            this.streamdata = new byte[0x1785];
            var index = 0;
            do
            {
                this.streamdata[index] = 0;
                index++;
            }
            while (index <= 0x1784);
            var num2 = 0;
            do
            {
                this.parseEventData[num2, 0] = "";
                this.parseEventData[num2, 1] = "";
                num2++;
            }
            while (num2 <= 20);
        }

        public void OpenBytes(byte[] content)
        {
            var stream = new MemoryStream(content);
            this.ReadFromStream(stream);
        }

        public void OpenFile(string filename)
        {
            FileStream stream;
            try
            {
                stream = File.OpenRead(filename);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                ProjectData.ClearProjectError();
                return;
            }

            this.ReadFromStream(stream);
        }

        [Obsolete("Not usable in this form. Only for testing.")]
        public void OpenText(string content)
        {
            //Stream stream;
            //byte[] byteArray = Encoding.UTF8.GetBytes(content);
            var byteArray = Encoding.Default.GetBytes(content);
            var stream = new MemoryStream(byteArray);
            this.ReadFromStream(stream);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this
        /// instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                "eventName: {2}\r\nSprache: {1}\r\nBeschreibung: {0} ",
                this.Beschreibung,
                this.EventLanguage,
                this.EventName);
        }

        public void parseEvent(string description)
        {
            var num = (description.Length / 0xf9) + 1;
            var num3 = num;
            for (var i = 0; i <= num3; i++)
            {
                if (description.Length > 0xf9)
                {
                    this.parseEventData[i, 0] = Strings.Left(description, 0xf9);
                    this.parseEventData[i, 1] = "249";
                    description = Strings.Right(description, description.Length - 0xf9);
                }
                else
                {
                    this.parseEventData[i, 0] = description;
                    this.parseEventData[i, 1] = Conversions.ToString(description.Length);
                    return;
                }
            }
        }

        public void writeStreamData(string filename, bool mExist)
        {
            var path = filename;
            var write = FileAccess.Write;
            var index = 0;
            if (this.AnzahlByte >= 0x1000)
            {
                Interaction.MsgBox(
                    "Die maximale Dateigr\x00f6\x00dfe wurde erreicht. Bitte die Beschreibung verk\x00fcrzen.",
                    MsgBoxStyle.Critical,
                    "Dateifehler");
            }
            else
            {
                int num2;
                FileStream stream;
                BinaryWriter writer;
                this.streamdata = new byte[0x1001];
                index = 0;
                do
                {
                    this.streamdata[index] = 0;
                    index++;
                }
                while (index <= 0x1000);
                if (this.eventIdHi != 0)
                {
                    this.streamdata[0] = this.eventIdHi;
                }
                else
                {
                    this.streamdata[0] = 0x7b;
                }
                if (this.eventIdLo != 0)
                {
                    this.streamdata[1] = this.eventIdLo;
                }
                else
                {
                    this.streamdata[1] = 0x7b;
                }
                var num3 = EitTimeHelper.ParseToDVBTime(Strings.Left(this.eventStartzeit, 10));
                this.streamdata[2] = (byte)(num3 >> 8);
                this.streamdata[3] = (byte)(num3 - (this.streamdata[2] * 0x100));
                if (this.eventStartzeit == null)
                {
                    this.streamdata[4] = 0;
                    this.streamdata[5] = 0;
                    this.streamdata[6] = 0;
                    this.streamdata[7] = 0;
                    this.streamdata[8] = 0;
                    this.streamdata[9] = 0;
                }
                else
                {
                    this.streamdata[4] =
                        (byte)EITConversions.ToBcd(Conversions.ToInteger(Strings.Mid(this.eventStartzeit, 12, 2)));
                    this.streamdata[5] =
                        (byte)EITConversions.ToBcd(Conversions.ToInteger(Strings.Mid(this.eventStartzeit, 15, 2)));
                    this.streamdata[6] =
                        (byte)EITConversions.ToBcd(Conversions.ToInteger(Strings.Right(this.eventStartzeit, 2)));
                    this.streamdata[7] =
                        (byte)EITConversions.ToBcd(Conversions.ToInteger(Strings.Left(this.eventDuration, 2)));
                    this.streamdata[8] =
                        (byte)EITConversions.ToBcd(Conversions.ToInteger(Strings.Mid(this.eventDuration, 4, 2)));
                    this.streamdata[9] =
                        (byte)EITConversions.ToBcd(Conversions.ToInteger(Strings.Right(this.eventDuration, 2)));
                }
                this.streamdata[12] = 0x4d;
                this.streamdata[13] = (byte)((this.EventName.Length + 5) + this.EventType.Length);
                this.streamdata[14] = (byte)Strings.Asc(Strings.Left(this.EventLanguage, 1));
                this.streamdata[15] = (byte)Strings.Asc(Strings.Mid(this.EventLanguage, 2, 1));
                this.streamdata[0x10] = (byte)Strings.Asc(Strings.Right(this.EventLanguage, 1));
                this.streamdata[0x11] = (byte)this.EventName.Length;
                index = 0x12;
                var num7 = (index + this.EventName.Length) - 1;
                index = 0x12;
                while (index <= num7)
                {
                    this.streamdata[index] = (byte)Strings.Asc(Strings.Mid(this.EventName, index - 0x11, 1));
                    index++;
                }
                this.streamdata[index] = (byte)this.EventType.Length;
                index++;
                if (this.EventType != null)
                {
                    var length = this.EventType.Length;
                    for (num2 = 1; num2 <= length; num2++)
                    {
                        this.streamdata[index] = (byte)Strings.Asc(Strings.Mid(this.EventType, num2, 1));
                        index++;
                    }
                }
                num2 = 0;
                do
                {
                    if (this.eventAudio[num2, 1] != null)
                    {
                        this.streamdata[index] = 80;
                        index++;
                        this.streamdata[index] = (byte)(this.eventAudio[num2, 1].Length + 6);
                        index++;
                        if (this.HDAudio)
                        {
                            this.streamdata[index] = 0xf4;
                        }
                        else
                        {
                            this.streamdata[index] = 0xf2;
                        }
                        index++;
                        this.streamdata[index] = (byte)Convert.ToInt32(Strings.Left(this.eventAudio[num2, 2], 2), 0x10);
                        index++;
                        this.streamdata[index] = (byte)Convert.ToInt32(Strings.Right(this.eventAudio[num2, 2], 2), 0x10);
                        index++;
                        this.streamdata[index] = (byte)Strings.Asc(Strings.Left(this.eventAudio[num2, 0], 1));
                        index++;
                        this.streamdata[index] = (byte)Strings.Asc(Strings.Mid(this.eventAudio[num2, 0], 2, 1));
                        index++;
                        this.streamdata[index] = (byte)Strings.Asc(Strings.Right(this.eventAudio[num2, 0], 1));
                        index++;
                        var num9 = this.eventAudio[num2, 1].Length;
                        for (var i = 1; i <= num9; i++)
                        {
                            this.streamdata[index] = (byte)Strings.Asc(Strings.Mid(this.eventAudio[num2, 1], i, 1));
                            index++;
                        }
                    }
                    num2++;
                }
                while (num2 <= 3);
                if (this.EventPicture != null)
                {
                    this.streamdata[index] = 80;
                    index++;
                    this.streamdata[index] = (byte)(this.EventPicture.Length + 6);
                    index++;
                    if (this.HDVideo)
                    {
                        this.streamdata[index] = 0xf5;
                    }
                    else
                    {
                        this.streamdata[index] = 0xf1;
                    }
                    index++;
                    this.streamdata[index] = (byte)Convert.ToInt32(Strings.Left(this.VPid, 2), 0x10);
                    index++;
                    this.streamdata[index] = (byte)Convert.ToInt32(Strings.Right(this.VPid, 2), 0x10);
                    index++;
                    this.streamdata[index] = (byte)Strings.Asc(Strings.Left(this.EventLanguage, 1));
                    index++;
                    this.streamdata[index] = (byte)Strings.Asc(Strings.Mid(this.EventLanguage, 2, 1));
                    index++;
                    this.streamdata[index] = (byte)Strings.Asc(Strings.Right(this.EventLanguage, 1));
                    index++;
                    var num10 = this.EventPicture.Length;
                    num2 = 1;
                    while (num2 <= num10)
                    {
                        this.streamdata[index] = (byte)Strings.Asc(Strings.Mid(this.EventPicture, num2, 1));
                        index++;
                        num2++;
                    }
                }
                if (this.eventDescription != null)
                {
                    this.parseEvent(this.eventDescription);
                    var num = 0;
                    var num6 = 0;
                    while (this.parseEventData[num6, 1] != "")
                    {
                        num6++;
                    }
                    for (num = 0; num6 > num; num++)
                    {
                        this.streamdata[index] = 0x4e;
                        index++;
                        this.streamdata[index] = (byte)(Conversions.ToInteger(this.parseEventData[num, 1]) + 6);
                        index++;
                        this.streamdata[index] = (byte)(((num << 4) + num6) - 1);
                        index++;
                        this.streamdata[index] = (byte)Strings.Asc(Strings.Left(this.EventLanguage, 1));
                        index++;
                        this.streamdata[index] = (byte)Strings.Asc(Strings.Mid(this.EventLanguage, 2, 1));
                        index++;
                        this.streamdata[index] = (byte)Strings.Asc(Strings.Right(this.EventLanguage, 1));
                        index++;
                        this.streamdata[index] = 0;
                        index++;
                        this.streamdata[index] = (byte)this.parseEventData[num, 0].Length;
                        index++;
                        var num11 = this.parseEventData[num, 0].Length;
                        for (num2 = 1; num2 <= num11; num2++)
                        {
                            if (Strings.Mid(this.parseEventData[num, 0], num2, 1)
                                == Conversions.ToString(Strings.Chr(0xff)))
                            {
                                this.streamdata[index] = 0x8a;
                                index++;
                            }
                            else
                            {
                                this.streamdata[index] =
                                    (byte)Strings.Asc(Strings.Mid(this.parseEventData[num, 0], num2, 1));
                                index++;
                            }
                        }
                    }
                }
                if (this.BigEndian)
                {
                    this.dLoopLengthHi = (byte)Math.Round((double)Conversion.Fix((((index - 12)) / 255.0)));
                    this.dLoopLenghtLo = (byte)((index - 12) - (this.dLoopLengthHi * 0xff));
                    this.streamdata[10] = this.runningStatus;
                    if (this.Verschluesselung == 1)
                    {
                        this.Verschluesselung = 0x80;
                    }
                    this.streamdata[10] = (byte)(((byte)(this.streamdata[10] << 1)) + this.Verschluesselung);
                    this.streamdata[10] = (byte)(((byte)(this.streamdata[10] << 4)) + this.dLoopLengthHi);
                    this.streamdata[11] = this.dLoopLenghtLo;
                }
                var openOrCreate = FileMode.OpenOrCreate;
                try
                {
                    stream = new FileStream(path, openOrCreate, write);
                    writer = new BinaryWriter(stream);
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    var exception = exception1;
                    Interaction.MsgBox(exception.Message, MsgBoxStyle.Critical, null);
                    stream = null;
                    writer = null;
                    ProjectData.ClearProjectError();
                    return;
                }
                writer.Write(this.streamdata, 0, index);
                writer.Close();
                stream.Close();
                stream = null;
                writer = null;
            }
        }

        private void GetAudio(byte[] streamData, int Zeiger)
        {
            this.eventAudio[this.audioCount, 1] =
                Conversions.ToString(EITDeserialization.GetString(streamData, Zeiger + 5, 3));
            this.eventAudio[this.audioCount, 1] = Strings.StrConv(
                this.eventAudio[this.audioCount, 1], VbStrConv.Lowercase, 0);
            this.eventAudio[this.audioCount, 2] =
                Conversions.ToString(EITDeserialization.GetString(streamData, Zeiger + 8, streamData[Zeiger + 1] - 6));
            this.eventAudio[this.audioCount, 3] = EITDeserialization.GetPID(streamData, Zeiger + 3);
            this.audioCount++;
            this.HDAudio = false;
        }

        private void GetHDAudio(int Zeiger)
        {
            this.eventAudio[this.audioCount, 1] =
                Conversions.ToString(EITDeserialization.GetString(this.streamdata, Zeiger + 5, 3));
            this.eventAudio[this.audioCount, 1] = Strings.StrConv(
                this.eventAudio[this.audioCount, 1], VbStrConv.Lowercase, 0);
            this.eventAudio[this.audioCount, 2] =
                Conversions.ToString(
                    EITDeserialization.GetString(this.streamdata, Zeiger + 8, this.streamdata[Zeiger + 1] - 6));
            this.eventAudio[this.audioCount, 3] = EITDeserialization.GetPID(this.streamdata, Zeiger + 3);
            this.audioCount++;
            this.HDAudio = true;
        }

        private void ReadFromStream(Stream stream)
        {
            var flag = false;
            var index = 12;
            this.audioCount = 0;
            this.Clear();
            stream.Read(this.streamdata, 0, (int)stream.Length);
            this.FileSize = (int)stream.Length;
            this.eventIdHi = this.streamdata[0];
            this.eventIdLo = this.streamdata[1];
            this.eventDuration = EITDeserialization.GetTime(this.streamdata, 7);
            this.eventStartzeit = EitTimeHelper.ParseTime(
                this.streamdata[2], this.streamdata[3], this.streamdata[4], this.streamdata[5], this.streamdata[6]);
            this.dLoopLenghtLo = this.streamdata[11];
            if (this.BigEndian)
            {
                this.runningStatus = (byte)(this.streamdata[10] >> 5);
                this.Verschluesselung = (byte)(((byte)(this.streamdata[10] >> 4)) << 7);
                this.dLoopLengthHi = (byte)(((byte)(this.streamdata[10] << 4)) >> 4);
            }
            else
            {
                this.dLoopLengthHi = (byte)(((byte)(this.streamdata[10] >> 4)) << 4);
                this.Verschluesselung = (byte)(((byte)(this.streamdata[10] << 4)) >> 7);
                this.runningStatus = (byte)(this.streamdata[10] << 5);
            }
            this.dLoopLength = (ushort)((this.dLoopLengthHi * 0x100) + this.dLoopLenghtLo);
            if (this.Verschluesselung == 0x80)
            {
                this.Verschluesselung = 1;
            }
            while (this.streamdata[index + 1] < stream.Length)
            {
                var num3 = this.streamdata[index];
                if (num3 == EITConstants.Desc_Short_Event)
                {
                    EITShortDescriptionParser.GetShortDescription(this, this.streamdata, index);
                    // this.GetShortDescription(index);
                    flag = false;
                }
                else if (num3 == EITConstants.Desc_Extended_Event)
                {
                    this.eventDescription = EITDeserialization.GetDescExtended(
                        this.streamdata, index, this.eventDescription);
                    flag = false;
                }
                else if (num3 == EITConstants.Desc_Content)
                {
                    flag = false;
                }
                else if (num3 == EITConstants.Desc_Componente)
                {
                    byte num2 = 0;
                    if (this.audioCount <= 3)
                    {
                        num2 = this.streamdata[index + 2];
                        var num4 = num2;
                        if (num4 == EITConstants.Desc_HDVideo)
                        {
                            EITHdParser.GetHDVideo(this, this.streamdata, index);
                            //this.GetHDVideo(this.streamdata, index);
                        }
                        else if (num4 == EITConstants.Desc_HDAudio)
                        {
                            this.GetHDAudio(index);
                        }
                        else if (num4 != EITConstants.Desc_Untertitel)
                        {
                            if (num4 == EITConstants.Desc_Audio)
                            {
                                this.GetAudio(this.streamdata, index);
                            }
                            else if (num4 == EITConstants.Desc_Video)
                            {
                                EITHdParser.GetVideo(this, this.streamdata, index);
                                //this.GetVideo(index);
                            }
                        }
                    }
                    flag = false;
                }
                else if (num3 == 0x5f)
                {
                    flag = false;
                }
                else if (num3 == 0x65)
                {
                    flag = false;
                }
                else if (num3 == 0x69)
                {
                    flag = false;
                }
                else if (num3 == 130)
                {
                    flag = false;
                }
                else
                {
                    if (!(!flag & (index < stream.Length))
                        || (Interaction.MsgBox(
                            string.Format("Fehlerhaftes EITFormat-File{0}Nochmal versuchen ?", "\r\n"),
                            MsgBoxStyle.Critical | MsgBoxStyle.YesNo,
                            "Lesefehler") != MsgBoxResult.Yes))
                    {
                        break;
                    }
                    flag = true;
                    index++;
                }
                if (!flag)
                {
                    index = (index + this.streamdata[index + 1]) + 2;
                }
            }
            stream.Close();
        }
    }
}