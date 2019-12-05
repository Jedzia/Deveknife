// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FtpEitLister.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>21.04.2015 15:43</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.FtpClient;

    using Deveknife.Api;
    using Deveknife.Blades.Overview.Eit.Formats;

    using Castle.Core.Logging;

    public interface IFtpLister
    {
        FtpListItem[] GetListing(string path);
    }

    public class Dingens : IBladeTool
    {
    }

    public class FtpLister : IFtpLister, IBladeTool
    {
        private readonly FtpClient conn;

        public FtpLister(FtpClient conn)
        {
            Guard.NotNull(() => conn, conn);
            this.conn = conn;
        }

        public FtpListItem[] GetListing(string path)
        {
            if (!this.conn.IsConnected)
            {
                this.conn.Connect();
            }

            var result = this.conn.GetListing(path);
            return result;
        }
    }

    public class CachedFtpLister : IFtpLister, IBladeTool
    {
        private static readonly Dictionary<string, FtpListItem[]> listingsCache =
            new Dictionary<string, FtpListItem[]>();

        private readonly IFtpLister lister;

        public CachedFtpLister(IFtpLister lister)
        {
            Guard.NotNull(() => lister, lister);
            this.lister = lister;
        }

        public FtpListItem[] GetListing(string path)
        {
            FtpListItem[] result;
            var success = listingsCache.TryGetValue(path, out result);
            if (!success)
            {
                result = this.lister.GetListing(path);
                listingsCache.Add(path, result);
            }

            return result;
        }

        public static void Invalidate()
        {
            listingsCache.Clear();
        }
    }

    public class FtpFileLister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FtpFileLister"/> class. 
        /// Initializes a new instance of the <see cref="FtpEitLister"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="userName">
        /// Name of the user.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        public FtpFileLister(ILogger logger, string host, string userName, string password)
        {
            Guard.NotNull(() => logger, logger);
            this.Logger = logger;
            Guard.NotNullOrEmpty(() => host, host);
            this.Host = host;
            Guard.NotNullOrEmpty(() => userName, userName);
            this.UserName = userName;
            this.Password = password;
        }

        public string Host { get; private set; }

        public ILogger Logger { get; private set; }

        public string UserName { get; private set; }

        private string Password { get; set; }

        public List<string> GetFileList(string path)
        {
            CachedFtpLister.Invalidate();
            var fileList = new List<string>();
            FtpListItem[] lst = null;
            using (var conn = new FtpClient())
            {
                this.InitHost(conn);

                // conn.Connect();
                // lst = conn.GetListing(path);
                var lister = new CachedFtpLister(new FtpLister(conn));
                lst = lister.GetListing(path);

                if (lst == null)
                {
                    return fileList;
                }

                // this.lbEitFiles.Items.Clear();
                foreach (var ftpListItem in lst)
                {
                    if (ftpListItem.Type != FtpFileSystemObjectType.File)
                    {
                        continue;
                    }

                    /*if (Path.GetExtension(ftpListItem.Name) != ".eit")
                    {
                        continue;
                    }*/

                    // ftpListItem.
                    // conn.OpenRead()
                    // this.lbEitFiles.Items.Add(ftpListItem.Name);

                    // byte[] buffer = new byte[1024];
                    var fullFileName = "ftp://" + conn.Host + ftpListItem.FullName;
                    fileList.Add(fullFileName);

                    try
                    {
                        // var eitDisp = new EITFormatDisplay(eit);
                        // eitDisp.Filename = "ftp://" + UserName + ":bauer" + "@" + conn.Host + ftpListItem.FullName;
                        // eitDisp.Filename = "ftp://" + conn.Host + fullFileName;
                        // eits.Add(eitDisp);
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format("Error in File: '{0}'", fullFileName);
                        this.Logger.Error(message);
                        this.Logger.Error(ex.ToString());
                    }
                }
            }

            return fileList;
        }

        protected void InitHost(FtpClient conn)
        {
            conn.Host = this.Host;
            conn.Credentials = new NetworkCredential(this.UserName, this.Password);
        }
    }

    public class FtpEitLister : FtpFileLister
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FtpEitLister" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="host">The host.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public FtpEitLister(ILogger logger, string host, string userName, string password)
            : base(logger, host, userName, password)
        {
        }

        /*/// <summary>
        /// Initializes a new instance of the <see cref="FtpEitLister" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="host">The host.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        public FtpEitLister(ILogger logger, string host, string userName, string password)
        {
            Guard.NotNull(() => logger, logger);
            this.Logger = logger;
            Guard.NotNullOrEmpty(() => host, host);
            this.Host = host;
            Guard.NotNullOrEmpty(() => userName, userName);
            this.UserName = userName;
            this.Password = password;
        }*/
        public List<EITFormatDisplay> GetEitList(string path)
        {
            // this can be refactored with base.GetFileList ... or keep
            // it separate cos the open handle to the file.
            CachedFtpLister.Invalidate();
            var eits = new List<EITFormatDisplay>();
            FtpListItem[] lst = null;
            using (var conn = new FtpClient())
            {
                this.InitHost(conn);

                // conn.Connect();
                // lst = conn.GetListing(path);
                var lister = new CachedFtpLister(new FtpLister(conn));
                lst = lister.GetListing(path);

                if (lst == null)
                {
                    return eits;
                }

                // this.lbEitFiles.Items.Clear();
                foreach (var ftpListItem in lst)
                {
                    if (ftpListItem.Type != FtpFileSystemObjectType.File)
                    {
                        continue;
                    }

                    if (Path.GetExtension(ftpListItem.Name) != ".eit")
                    {
                        continue;
                    }

                    // ftpListItem.
                    // conn.OpenRead()
                    // this.lbEitFiles.Items.Add(ftpListItem.Name);

                    // byte[] buffer = new byte[1024];
                    var fullFileName = ftpListItem.FullName;
                    try
                    {
                        using (var istream = conn.OpenRead(fullFileName))
                        {
                            // byte[] buf = new byte[8192];
                            var buf = new byte[istream.Length];
                            var read = 0;
                            var eit = new EITFormat();
                            try
                            {
                                // istream.Position is incremented accordingly to the reads you perform
                                // istream.Length == file size if the server supports getting the file size
                                // also note that file size for the same file can vary between ASCII and Binary
                                // modes and some servers won't even give a file size for ASCII files! It is
                                // recommended that you stick with Binary and worry about character encodings
                                // on your end of the connection.
                                // var br = new StringReader(istream);

                                // var len = istream.Read(buffer, 0, 1024);
                                // var str = Encoding.ASCII.GetString(buffer);

                                /*while ((read = istream.Read(buf, 0, buf.Length)) > 0)
                        {
                            Console.Write(
                                "\r{0}/{1} {2:p}     ",
                                istream.Position,
                                istream.Length,
                                ((double)istream.Position / (double)istream.Length));

                            
                        }*/
                                read = istream.Read(buf, 0, buf.Length);
                                eit.OpenBytes(buf);
                                var eitDisp = new EITFormatDisplay(eit);

                                // eitDisp.Filename = "ftp://" + UserName + ":bauer" + "@" + conn.Host + ftpListItem.FullName;
                                eitDisp.Filename = "ftp://" + conn.Host + fullFileName;
                                eits.Add(eitDisp);
                            }
                            catch (Exception ex)
                            {
                                var message = string.Format("Error in File: '{0}'", fullFileName);
                                this.Logger.Error(message);
                                this.Logger.Error(ex.ToString());
                            }
                            finally
                            {
                                Console.WriteLine();
                                istream.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var message = string.Format("Error in File: '{0}'", fullFileName);
                        this.Logger.Error(message);
                        this.Logger.Error(ex.ToString());
                    }
                }
            }

            return eits;
        }
    }
}