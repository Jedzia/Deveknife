//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ITextFilter.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 09:56</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Api
{
    /// <summary>
    /// A string filter, that transforms input text.
    /// </summary>
    public interface ITextFilter
    {
        /// <summary>
        /// Applies the transformation to the specified text.
        /// </summary>
        /// <param name="text">The text to transform.</param>
        /// <returns>the transformed text.</returns>
        string Apply(string text);

        /// <summary>
        /// Determines whether the text contains the filter criteria.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>true</c> if the text contains the filter criteria; otherwise, <c>false</c>.</returns>
        bool Contains(string text);
    }
}