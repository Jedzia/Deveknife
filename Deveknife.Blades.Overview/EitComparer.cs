// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EitComparer.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>23.02.2014 22:46</date>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using Deveknife.Blades.Overview.Util;

    using Castle.Core.Internal;

    internal class EitComparer
    {
        private readonly List<string> lbEitFiles = new List<string>();

        private List<EITFormatDisplay> comparerEitFiles;

        /// <summary>
        /// Gets the comparer eit list.
        /// </summary>
        /// <value>The comparer eit files.</value>
        public List<EITFormatDisplay> ComparerEitFiles
        {
            get
            {
                if (this.comparerEitFiles == null)
                {
                    this.comparerEitFiles = new List<EITFormatDisplay>();
                }

                return this.comparerEitFiles;
            }
        }

        public bool HasComparerEitFiles
        {
            get
            {
                return this.ComparerEitFiles.Count > 0;
            }
        }

        public void Clear()
        {
            // this.LastFetchedFolder = null;
            // this.LastFetchedLocalEitFiles = null;
            this.lbEitFiles.Clear();
            this.ComparerEitFiles.Clear();
        }

        public void Compare(List<EITFormatDisplay> listToCompareTo, Func<EITFormatDisplay, string> compareFunc = null, Func<EITFormatDisplay, EITFormatDisplay, string> dupNoteFunc = null)
        {
            var cmpFnc = compareFunc;
            if (compareFunc == null)
            {
                cmpFnc = display => display.Beschreibung;
            }

            if (dupNoteFunc == null)
            {
                dupNoteFunc = FormatDupMessage;
            }

            this.ColorEit(listToCompareTo, cmpFnc, dupNoteFunc);
        }

        public bool Set(string lastFetchedFolder, List<EITFormatDisplay> lastFetchedLocalEitFiles)
        {
            if (string.IsNullOrWhiteSpace(lastFetchedFolder) || lastFetchedLocalEitFiles == null
                || lastFetchedLocalEitFiles.Count <= 0)
            {
                return false;
            }

            if (this.lbEitFiles.Contains(lastFetchedFolder))
            {
                return false;
            }

            this.lbEitFiles.Add(lastFetchedFolder);
            this.ComparerEitFiles.AddRange(lastFetchedLocalEitFiles);
            return true;
        }

        /// <summary>
        /// Marks the doublette entries with Color.Yellow.
        /// </summary>
        /// <param name="eits">The list to check for doublettes.</param>
        /// <param name="compareFunc">The compare function.</param>
        /// <param name="dupNoteFunc">Generates the duplicate Message.</param>
        private void ColorEit(IEnumerable<EITFormatDisplay> eits, Func<EITFormatDisplay, string> compareFunc, Func<EITFormatDisplay, EITFormatDisplay, string> dupNoteFunc)
        {
            foreach (var eitA in this.ComparerEitFiles)
            {
                var formatDisplays = eits as IList<EITFormatDisplay> ?? eits.ToList();
                var strA = compareFunc(eitA);
                foreach (var eitB in formatDisplays)
                {
                    var strB = compareFunc(eitB);
                    //GetValue(eitB, eitB.Beschreibung, eitA.Beschreibung);
                    // bool cmp = eitA.Beschreibung == eitB.Beschreibung;
                    var cmp = strA == strB;
                    if (!cmp)
                    {
                        continue;
                    }

                    eitB.Color = Color.Yellow.ToArgb();
                    eitB.Note += dupNoteFunc(eitA, eitB);

                    //eitB.Note += string.Format("Has a duplicate in '{0}'.", a.Filename);
                    // break;
                }
            }
        }

        /// <summary>
        /// Formats the duplication message about to EITFormatDisplay's.
        /// </summary>
        /// <param name="formatDisplay">The format display (other).</param>
        /// <param name="formatDisplayB">The format display (original).</param>
        /// <returns>a message that informs about the duplication.</returns>
        private static string FormatDupMessage(EITFormatDisplay formatDisplay, EITFormatDisplay formatDisplayB)
        {
            return string.Format("Has a duplicate in '{0}'.", formatDisplay.Filename);
        }

        private void GetValue(EITFormatDisplay eitB, string strB, string strA)
        {
            var b = Str(strB);
            var a = Str(strA);
            eitB.Test = b + "=" + a;
        }

        private string Str(string str)
        {
            return str.Soundex();
            
            var trimmed = str.Replace(Environment.NewLine, "").Trim();
            var splits = trimmed.Split(new[] { '.', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var groups = splits.GroupBy(s => s);
            var enumerable = groups as IGrouping<string, string>[] ?? groups.ToArray();
            var sgroups = enumerable.OrderBy(grouping => grouping.Count());
            
            string res = string.Empty;
            sgroups.ForEach(s => res += s.Key + " ");
            return res;
        }


    }
}