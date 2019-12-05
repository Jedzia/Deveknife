//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="EnigmaFile.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>06.12.2013 01:58</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.RecodeMule
{
    using System;
    using System.Globalization;
    using System.IO;

    public class EnigmaFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnigmaFile" /> class.
        /// </summary>
        /// <param name="filename">
        /// The filename or path of the enigma 2 file.
        /// </param>
        public EnigmaFile(string filename)
        {
            var infi = new FileInfo(filename);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(infi.Name);
            if (fileNameWithoutExtension == null)
            {
                return;
            }

            var parts = fileNameWithoutExtension.Split('-');

            if (parts.Length != 3)
            {
                return;
            }

            this.IsValid = this.ParseDate(parts[0].Trim());
            this.ServiceName = parts[1].Trim();
            this.Title = parts[2].Trim();
            //IsValid &= dtsuccess;
        }

        private bool ParseDate(string rawdatetime)
        {
            //var culture = CultureInfo.CreateSpecificCulture("en-US");
            var culture = CultureInfo.CurrentCulture;
            var styles = DateTimeStyles.None;
            
            //var parts = rawdatetime.Split(' ');

            DateTime beginDate;
            var dtsuccess = DateTime.TryParseExact(rawdatetime, "yyyyMMdd HHmm", culture, styles, out beginDate);
            if (dtsuccess)
            {
                BeginDate = beginDate;
            }
            return dtsuccess;
        }

        public bool IsValid { get; private set; }
        public DateTime BeginDate { get; private set; }

        public string ServiceName { get; private set; }

        public string Title { get; private set; }
    }
}