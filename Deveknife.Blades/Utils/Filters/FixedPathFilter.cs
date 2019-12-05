// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixedPathFilter.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>30.07.2015 15:52</date>
// <summary>
//   Removes the ending "_fixed" from a filename in a path.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Utils.Filters
{
    using System.IO;

    /// <summary>
    /// Removes the ending "_fixed" from a filename in a path.
    /// </summary>
    public class FixedPathFilter : BasePathFilter
    {
        private const string Fixed = "_fixed";

        /// <summary>
        /// Applies the transformation to the specified text.
        /// </summary>
        /// <param name="text">The text to transform.</param>
        /// <returns>
        /// the transformed text.
        /// </returns>
        public override string Apply(string text)
        {
            return text.Replace(Fixed, string.Empty);
            var result = text;
            var fi = new FileInfo(text);
            var pwe = Path.GetFileNameWithoutExtension(text);
            if (pwe != null && pwe.EndsWith(Fixed))
            {
                var substr = pwe.Substring(0, pwe.Length - Fixed.Length);
                if (fi.Directory != null)
                {
                    return Path.Combine(fi.Directory.FullName, substr + fi.Extension);
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether the text contains the filter criteria.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>true</c> if the text contains the filter criteria; otherwise, <c>false</c>.</returns>
        public override bool Contains(string text)
        {
            return text.Contains(Fixed);
        }
    }
}