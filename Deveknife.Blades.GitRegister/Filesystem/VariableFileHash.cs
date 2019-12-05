// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VariableFileHash.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2019, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>05.12.2019 21:47</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.FileMoveTool.Filesystem
{
    using System;
    using System.Collections.Generic;

    using SystemInterface.IO;

    public class VariableFileHash
    {
        private static readonly IEqualityComparer<VariableFileHash> HashComparerInstance = new HashEqualityComparer();

        // private string FullPath;
        private readonly IFileInfo fileInfo;

        private readonly int hash;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableFileHash"/> class.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="hashCodeFnc">The hash code FNC.</param>
        /// <exception cref="Exception">A delegate callback throws an exception.</exception>
        public VariableFileHash(IFileInfo fileInfo, Func<IFileInfo, int> hashCodeFnc)
        {
            this.fileInfo = fileInfo;
            this.hash = hashCodeFnc(fileInfo);
        }

        /// <summary>
        /// Gets the hash comparer.
        /// </summary>
        /// <value>The hash comparer.</value>
        public static IEqualityComparer<VariableFileHash> HashComparer
        {
            get
            {
                return VariableFileHash.HashComparerInstance;
            }
        }

        public IFileInfo FileInfo => this.fileInfo;

        /// <summary>
        /// Gets the full name of the file.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName => this.FileInfo.FullName;

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals([CanBeNull] object obj)
        {
            if(object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if(object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if(obj.GetType() != this.GetType())
            {
                return false;
            }

            return VariableFileHash.HashComparerInstance.Equals(this, (VariableFileHash)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return this.hash;
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool Equals(VariableFileHash other)
        {
            return this.hash == other.hash;
        }

        private sealed class HashEqualityComparer : IEqualityComparer<VariableFileHash>
        {
            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
            /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
            /// <returns>true if the specified objects are equal; otherwise, false.</returns>
            public bool Equals(VariableFileHash x, VariableFileHash y)
            {
                if(object.ReferenceEquals(x, y))
                {
                    return true;
                }

                if(object.ReferenceEquals(x, null))
                {
                    return false;
                }

                if(object.ReferenceEquals(y, null))
                {
                    return false;
                }

                if(x.GetType() != y.GetType())
                {
                    return false;
                }

                return x.hash == y.hash;
            }

            /// <summary>
            /// Returns a hash code for this instance.
            /// </summary>
            /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
            /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
            public int GetHashCode(VariableFileHash obj)
            {
                return obj.hash;
            }
        }
    }
}