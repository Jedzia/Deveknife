//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FindMissing.cs" company="EvePanix">Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>25.04.2015 19:28</date>
//  --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview.Util
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Detect missing elements.
    /// </summary>
    public static class FindMissing
    {
        /// <summary>
        /// Detect missing elements in a list.
        /// </summary>
        /// <param name="list">The list to process.</param>
        /// <param name="filterExtension">The File Extension for filtering.</param>
        /// <param name="searchExtension">The File Extension to search for.</param>
        public static List<string> InList(List<string> list, string filterExtension, string searchExtension)
        {
            var missing = new List<string>();
            var tsfiles = list.Where(s => s.EndsWith(filterExtension));
            foreach (var tsfile in tsfiles)
            {
                //var tsfilePath = Path.GetDirectoryName(tsfile);
                //var tsfileWoExtension = Path.GetFileNameWithoutExtension(tsfile);
                //var basePath = Path.Combine(tsfilePath, tsfileWoExtension);

                var tsfilePath = tsfile.Substring(0, tsfile.LastIndexOf(filterExtension));
                var otherfiles = list.Where(s => s.StartsWith(tsfilePath));

                if (!otherfiles.Any(s => s.EndsWith(searchExtension)))
                {
                    missing.Add(tsfile);
                }
                //var tsfileWoExtension = Path.GetFileNameWithoutExtension(tsfile);
                //var basePath = Path.Combine(tsfilePath, tsfileWoExtension);
            }

            return missing;
        }
    }
}