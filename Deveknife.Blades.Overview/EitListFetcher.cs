// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EitListFetcher.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.02.2014 22:35</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Deveknife.Blades.Overview.Eit.Formats;

    using Castle.Core.Logging;

    internal class EitListFetcher
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EitListFetcher" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public EitListFetcher(ILogger logger)
        {
            Guard.NotNull(() => logger, logger);
            this.logger = logger;
        }

        public List<EITFormatDisplay> GetFtpEitFiles(
            string ftpHost, 
            string ftpUserName, 
            string ftpPassword, 
            string ftpEitBasePath)
        {
            var eits = new List<EITFormatDisplay>();
            try
            {
                var ftpLister = new FtpEitLister(this.logger, ftpHost, ftpUserName, ftpPassword);
                eits = ftpLister.GetEitList(ftpEitBasePath);
            }
            catch (Exception ex)
            {
                // Typical exceptions here are IOException, SocketException, or a FtpCommandException
                this.logger.Error(ex.ToString());
            }

            return eits;
        }

        /// <summary>
        /// Gets all FTP files.
        /// </summary>
        /// <param name="ftpHost">The FTP host.</param>
        /// <param name="ftpUserName">Name of the FTP user.</param>
        /// <param name="ftpPassword">The FTP password.</param>
        /// <param name="ftpEitBasePath">The FTP eit base path.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetAllFtpFiles(
    string ftpHost,
    string ftpUserName,
    string ftpPassword,
    string ftpEitBasePath)
        {
            var eits = new List<string>();
            try
            {
                var ftpLister = new FtpFileLister(this.logger, ftpHost, ftpUserName, ftpPassword);
                eits = ftpLister.GetFileList(ftpEitBasePath);
            }
            catch (Exception ex)
            {
                // Typical exceptions here are IOException, SocketException, or a FtpCommandException
                this.logger.Error(ex.ToString());
            }

            return eits;
        }

        public List<EITFormatDisplay> GetLocalEitFiles(string fsVideoBasePath)
        {
            var eits = new List<EITFormatDisplay>();
            try
            {
                var eitFiles = GetWatcherPath(fsVideoBasePath);

                // eitFiles.Sort();
                foreach (var file in eitFiles)
                {
                    var filename = file + ".eit";
                    var fi = Path.Combine(fsVideoBasePath, filename);
                    var eit = new EITFormat();
                    eit.OpenFile(fi);
                    var eitsDispl = new EITFormatDisplay(eit);
                    eitsDispl.Filename = Path.Combine(fsVideoBasePath, filename);
                    eits.Add(eitsDispl);
                    var beschMaxLen = Math.Min(eit.Beschreibung.Length, 80);
                    var beschr = eit.Beschreibung.Substring(0, beschMaxLen);
                    var s = string.Format("{0}, {1} .. {2}", eit.EventName, eit.EventType, beschr);

                    // this.lbEitFiles.Items.Add(s);
                    // ThrowSomething();
                }
            }
            catch (Exception ex)
            {
                // Typical exceptions here are IOException, SocketException, or a FtpCommandException
                this.logger.Error(ex.ToString());
            }

            return eits;
        }

        public IEnumerable<string> GetLocalEitFilesThatAreDone(string fsVideoBasePath)
        {
            //var eits = new List<EITFormatDisplay>();
            try
            {
                var eitFiles = GetDoneFiles(fsVideoBasePath, SearchOption.AllDirectories);
                var fff = eitFiles.ToList();
                return fff;
                // eitFiles.Sort();
                /*foreach (var file in eitFiles)
                {
                    var filename = file + ".eit";
                    var fi = Path.Combine(fsVideoBasePath, filename);
                    var eit = new EITFormat();
                    eit.OpenFile(fi);
                    var eitsDispl = new EITFormatDisplay(eit);
                    eitsDispl.Filename = Path.Combine(fsVideoBasePath, filename);
                    eits.Add(eitsDispl);
                    var beschMaxLen = Math.Min(eit.Beschreibung.Length, 80);
                    var beschr = eit.Beschreibung.Substring(0, beschMaxLen);
                    var s = string.Format("{0}, {1} .. {2}", eit.EventName, eit.EventType, beschr);

                    // this.lbEitFiles.Items.Add(s);
                    // ThrowSomething();
                }*/
            }
            catch (Exception ex)
            {
                // Typical exceptions here are IOException, SocketException, or a FtpCommandException
                this.logger.Error(ex.ToString());
            }

            return new List<string>();
            //return eits;
        }

        private static IEnumerable<string> GetWatcherPath(string fsVideoBasePath)
        {
            var di = new DirectoryInfo(fsVideoBasePath);
            if(!di.Exists)
            {
                return new List<string>();
            }

            var paths =
                di.GetFiles("*.eit", SearchOption.TopDirectoryOnly)
                    .Select(file => Path.GetFileNameWithoutExtension(file.FullName))
                    .ToArray();

            // var paths = di.GetFiles("*.eit", SearchOption.TopDirectoryOnly).Select(file => Path.Combine(file.DirectoryName, Path.GetFileNameWithoutExtension(file.FullName))).ToArray();
            return paths;
        }

        private static IEnumerable<string> GetDoneFiles(string fsVideoBasePath, SearchOption searchOption)
        {
            var di = new DirectoryInfo(fsVideoBasePath);
            var paths =
                di.GetFiles("*.eit", searchOption)
                    .Select(file => { return file.FullName; })
                    .Where(
                        file =>
                        {
                            var filenameNoExt = Path.GetFileNameWithoutExtension(file);
                            var directory = Path.GetDirectoryName(file);
                            var donePath = Path.Combine(directory, filenameNoExt + ".ts.done");
                            var exists = File.Exists(donePath);
                            return exists;
                        })
                    .ToArray();

            // var paths = di.GetFiles("*.eit", SearchOption.TopDirectoryOnly).Select(file => Path.Combine(file.DirectoryName, Path.GetFileNameWithoutExtension(file.FullName))).ToArray();
            return paths;
        }

    }
}