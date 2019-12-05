//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="RegistryHelper.cs" company="EvePanix">Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>28.09.2014 13:49</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Util
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    using Castle.Core.Internal;

    using Microsoft.Win32;

    public class RegistryHelper
    {
        public static void AddRegDictEntryForRootFolders(
            string lastFetchedFolder,
            out Dictionary<string, bool> browseDict,
            string keyName,
            string valueName)
        {
            var serializer = new DataContractSerializer(typeof(Dictionary<string, bool>));
            if (!GetRegDictForRootFolders(keyName, valueName, serializer, out browseDict))
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

        public static bool GetRegDictForRootFolders(
            string keyName,
            string valueName,
            XmlObjectSerializer serializer,
            out Dictionary<string, bool> browseDict)
        {
            browseDict = new Dictionary<string, bool>();

            var browseStartPath = Registry.GetValue(keyName, valueName, String.Empty);

            var inData = browseStartPath as string;
            if (inData.IsNullOrEmpty())
            {
                browseDict = new Dictionary<string, bool>();
                return true;

                //return false;
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

        public static void SetRegDictEntryForRootFolders(string keyName, string valueName, string dataStr)
        {
            Registry.SetValue(keyName, valueName, dataStr);
        }
    }
}