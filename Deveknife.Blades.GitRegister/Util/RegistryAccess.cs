// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistryAccess.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2016, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>22.10.2016 05:06</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.GitRegister.Util
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Xml;

    using Castle.Core.Internal;

    using Microsoft.Win32;

    /// <summary>
    /// Windows-Registry Helper Methods.
    /// </summary>
    public class RegistryAccess
    {
        /// <summary>
        /// Adds the registry dictionary entry for root folders.
        /// </summary>
        /// <param name="lastFetchedFolder">The last fetched folder.</param>
        /// <param name="browseDict">The browse dictionary.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="valueName">Name of the value.</param>
        // ReSharper disable once TooManyArguments
        public static void AddRegDictEntryForRootFolders(
            string lastFetchedFolder,
            out Dictionary<string, bool> browseDict,
            string keyName,
            string valueName)
        {
            var serializer = new DataContractSerializer(typeof(Dictionary<string, bool>));
            if (!RegistryAccess.GetRegDictForRootFolders(keyName, valueName, serializer, out browseDict))
            {
                return;
            }

            if (browseDict.ContainsKey(lastFetchedFolder))
            {
                return;
            }

            browseDict.Add(lastFetchedFolder, false);
            using (TextWriter tw = new StringWriter())
            {
                XmlWriter xm = new XmlTextWriter(tw);
                serializer.WriteObject(xm, browseDict);
                var dataStr = tw.ToString();
                Registry.SetValue(keyName, valueName, dataStr);
            }
        }

        /// <summary>
        /// Gets the registry dictionary for root folders.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="serializer">The serializer.</param>
        /// <param name="browseDict">The browse dictionary.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        // ReSharper disable once TooManyArguments
        public static bool GetRegDictForRootFolders(
            string keyName,
            string valueName,
            XmlObjectSerializer serializer,
            out Dictionary<string, bool> browseDict)
        {
            browseDict = new Dictionary<string, bool>();

            var browseStartPath = Registry.GetValue(keyName, valueName, string.Empty);

            var inData = browseStartPath as string;
            if (inData.IsNullOrEmpty())
            {
                browseDict = new Dictionary<string, bool>();
                return true;

                // return false;
            }

            Debug.Assert(inData != null, "inData != null");
            TextReader tr = new StringReader(inData);
            XmlReader xr = new XmlTextReader(tr);
            var res = serializer.ReadObject(xr) as Dictionary<string, bool>;
            if (res == null)
            {
                return false;
            }

            browseDict = res;
            return true;
        }

        /// <summary>
        /// Sets the registry dictionary entry for root folders.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="dataStr">The data string.</param>
        /// <exception cref="UnauthorizedAccessException">The <see cref="T:Microsoft.Win32.RegistryKey" /> is read-only, and thus cannot be written to; for example, it is a root-level node. </exception>
        /// <exception cref="SecurityException">The user does not have the permissions required to create or modify registry keys. </exception>
        public static void SetRegDictEntryForRootFolders(string keyName, string valueName, string dataStr)
        {
            Registry.SetValue(keyName, valueName, dataStr);
        }
    }
}