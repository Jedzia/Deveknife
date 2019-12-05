//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="PathFilter.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 07:58</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Utils.Filters
{
    using Deveknife.Api;

    /// <summary>
    /// A base class for path filters
    /// </summary>
    public abstract class BasePathFilter : ITextFilter
    {
        /// <summary>
        /// Applies the transformation to the specified text.
        /// </summary>
        /// <param name="text">The text to transform.</param>
        /// <returns>the transformed text.</returns>
        public abstract string Apply(string text);

        /// <summary>
        /// Determines whether the text contains the filter criteria.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>true</c> if the text contains the filter criteria; otherwise, <c>false</c>.</returns>
        public abstract bool Contains(string text);
    }
}