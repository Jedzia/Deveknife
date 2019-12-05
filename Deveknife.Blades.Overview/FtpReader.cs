//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FtpReader.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>12.02.2014 11:14</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.FtpClient;

    public class FileProgressArgs : EventArgs
    {
        public FileProgressArgs(long position, long length)
        {
            this.Position = position;
            this.Length = length;
        }

        public long Length { get; private set; }

        public long Position { get; private set; }
    }

    public class FtpReader
    {
        public FtpReader(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        public event EventHandler<FileProgressArgs> Progress;

        public string UserName { get; private set; }

        private string Password { get; set; }

        public void OpenReadURI(Uri uri, string destination )
        {
            using (var conn = new FtpClient())
            {
                conn.Host = uri.Host;
                conn.Credentials = new NetworkCredential(this.UserName, this.Password);
                OpenRead(uri.LocalPath, conn, destination, this.OnProgress);
            }
        }

        /*public static void Delete(
            string path, FtpClient conn)
        {
            conn.DeleteFile(path);
        }*/

        public static void OpenRead(string path, FtpClient conn, string destination, Action<FileProgressArgs> onProgress = null)
        {
            // hmm, only works with uri.LocalPath ... a System.Net.FtpClient problem?
            const int ReadBufSize = 8192*8;
            using (var s = conn.OpenRead(path))
            {
                var outFile = File.Create(destination, ReadBufSize);
                /*var useDiff = false;
                if (s.Length > int.MaxValue)
                {
                    useDiff = true;
                }*/
                var faktor = 100d / s.Length;
                var buf = new byte[ReadBufSize];
                var read = 0;
                try
                {
                    // istream.Position is incremented accordingly to the reads you perform
                    // istream.Length == file size if the server supports getting the file size
                    // also note that file size for the same file can vary between ASCII and Binary
                    // modes and some servers won't even give a file size for ASCII files! It is
                    // recommended that you stick with Binary and worry about character encodings
                    // on your end of the connection.
                    while ((read = s.Read(buf, 0, buf.Length)) > 0)
                    {
                        outFile.Write(buf, 0, read);
                        if (onProgress != null)
                        {
                            //if (useDiff)
                            //{
                                onProgress(new FileProgressArgs((long)(s.Position * faktor), 100));
                            //}
                            /*else
                            {
                                onProgress(new FileProgressArgs(s.Position, s.Length));
                            }*/
                            System.Windows.Forms.Application.DoEvents();
                        }
                        /*Console.Write("\r{0}/{1} {2:p}     ",
                                s.Position, s.Length,
                                ((double)s.Position / (double)s.Length));*/
                    }
                }
                finally
                {
                    s.Close();
                    outFile.Close();
                }
            }
        }

        public void OpenURI2(Uri uri)
        {
            using (var s = FtpClient.OpenRead(uri))
            {
                var buf = new byte[8192];
                var read = 0;

                try
                {
                    while ((read = s.Read(buf, 0, buf.Length)) > 0)
                    {
                        this.OnProgress(new FileProgressArgs(s.Position, s.Length));
                        /*Console.Write("\r{0}/{1} {2:p}     ",
                            s.Position, s.Length,
                            ((double)s.Position / (double)s.Length));*/
                    }
                }
                finally
                {
                    //Console.WriteLine();
                    s.Close();
                }
            }
        }

        protected virtual void OnProgress(FileProgressArgs e)
        {
            var handler = this.Progress;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}