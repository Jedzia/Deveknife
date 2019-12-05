//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EitHdParser.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>09.12.2013 10:19</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Eit.Formats
{
    using Microsoft.VisualBasic.CompilerServices;

    /*public static class EITParserExtension
    {
        public static void GetHDVideo(this EITFormat f, byte[] streamData, int index)
        {
            var parser = new EITHdParser(streamData, index);
            parser.GetHDVideo(f);
        }

        public static void GetVideo(this EITFormat f, byte[] streamData, int index)
        {
            var parser = new EITHdParser(streamData, index);
            parser.GetVideo(f);
        }
    }*/

    public class EITHdParser
    {
        private readonly int index;

        private readonly byte[] streamData;

        private EITHdParser(byte[] streamData, int index)
        {
            this.streamData = streamData;
            this.index = index;
        }

        public static void GetHDVideo(EITFormat f, byte[] streamData, int index)
        {
            var parser = new EITHdParser(streamData, index);
            parser.GetHDVideo(f);
        }

        public static void GetVideo(EITFormat f, byte[] streamData, int index)
        {
            var parser = new EITHdParser(streamData, index);
            parser.GetVideo(f);
        }

        private void GetHDVideo(EITFormat f)
        {
            f.EventPicture =
                EITStringHelper.STrim(
                    Conversions.ToString(
                        EITDeserialization.GetString(
                            this.streamData, this.index + 8, this.streamData[this.index + 1] - 6)));
            f.VPid = EITDeserialization.GetPID(this.streamData, this.index + 3);
            f.HDVideo = true;
        }

        private void GetVideo(EITFormat f)
        {
            f.EventPicture =
                EITStringHelper.STrim(
                    Conversions.ToString(
                        EITDeserialization.GetString(
                            this.streamData, this.index + 8, this.streamData[this.index + 1] - 6)));
            f.VPid = EITDeserialization.GetPID(this.streamData, this.index + 3);
        }
    }

    public class EITShortDescriptionParser
    {
        private readonly int index;

        private readonly byte[] streamData;

        private EITShortDescriptionParser(byte[] streamData, int index)
        {
            this.streamData = streamData;
            this.index = index;
        }

        public static void GetShortDescription(EITFormat f, byte[] streamData, int index)
        {
            var parser = new EITShortDescriptionParser(streamData, index);
            parser.GetShortDescription(f);
        }

        private void GetShortDescription(EITFormat f)
        {
            f.EventLanguage =
                EITStringHelper.STrim(
                    Conversions.ToString(EITDeserialization.GetString(this.streamData, this.index + 2, 3)));
            f.EventName =
                EITStringHelper.STrim(
                    Conversions.ToString(
                        EITDeserialization.GetString(this.streamData, this.index + 6, this.streamData[this.index + 5])));
            var start = (this.index + 7) + this.streamData[this.index + 5];
            f.EventType =
                EITStringHelper.STrim(
                    Conversions.ToString(
                        EITDeserialization.GetString(this.streamData, start, this.streamData[start - 1])));
        }
    }
}