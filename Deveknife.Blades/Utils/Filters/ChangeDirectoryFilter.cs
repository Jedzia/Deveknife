//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ChangeDirectoryFilter.cs" company="EvePanix">Copyright (c) Jedzia 2001-2013, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.</copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2013 10:02</date>
//  --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Utils.Filters
{
    using System.IO;

    using Deveknife.Api;

    /// <summary>
    /// A decorator Filter that can change the directory part of a given path.
    /// </summary>
    internal class ChangeDirectoryFilter : BasePathFilter
    {
        private readonly string directory;

        private ITextFilter infilter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeDirectoryFilter" /> class.
        /// </summary>
        /// <param name="directory">The new directory.</param>
        public ChangeDirectoryFilter(string directory)
            : this(null, directory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeDirectoryFilter" /> class.
        /// </summary>
        /// <param name="infilter">The filter to decorate.</param>
        /// <param name="directory">The new directory.</param>
        public ChangeDirectoryFilter(ITextFilter infilter, string directory)
        {
            this.infilter = infilter;
            this.directory = directory;
        }

        /// <summary>
        /// Applies the transformation to the specified text.
        /// </summary>
        /// <param name="text">The text to transform.</param>
        /// <returns>the transformed text.</returns>
        public override string Apply(string text)
        {
            if (this.infilter != null)
            {
               text = this.infilter.Apply(text);
            }

            var fi = new FileInfo(text);
            return Path.Combine(this.directory, fi.Name);
        }

        /// <summary>
        /// Determines whether the text contains the filter criteria.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><c>true</c> if the text contains the filter criteria; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool Contains(string text)
        {
            if (this.infilter != null)
            {
                return this.infilter.Contains(text);
            }

            return false;
        }
    }
}