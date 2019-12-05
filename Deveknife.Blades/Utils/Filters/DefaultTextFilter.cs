// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultTextFilter.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>30.07.2015 15:52</date>
// <summary>
//   Defines the DefaultTextFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.Utils.Filters
{
    /// <summary>
    /// Default text filter that does nothing.
    /// </summary>
    public class DefaultTextFilter : BasePathFilter
    {
        /// <summary>
        /// Applies the transformation to the specified text.
        /// </summary>
        /// <param name="text">The text to transform.</param>
        /// <returns>the transformed text.</returns>
        public override string Apply(string text)
        {
            return text;
        }

        /// <summary>
        /// Determines whether the text contains the filter criteria.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>true</c> if the text contains the filter criteria; otherwise, <c>false</c>.</returns>
        public override bool Contains(string text)
        {
            return true;
        }
    }
}