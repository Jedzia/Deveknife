// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobParameters.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>31.07.2015 15:23</date>
// <summary>
//   Provides parameter information for the <see cref="Job" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Deveknife.Blades.FileManager.Jobs
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Provides parameter information for the <see cref="Job"/> class.
    /// </summary>
    public class JobParameters
    {
        // : IDictionary<string, string>
        private readonly Dictionary<string, string> parameterList;

        /// <summary>
        /// Initializes a new instance of the <see cref="JobParameters"/> class.
        /// </summary>
        public JobParameters()
        {
            this.parameterList = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the parameter list.
        /// </summary>
        /// <value>The parameter list.</value>
        public Dictionary<string, string> ParameterList
        {
            get
            {
                return this.parameterList;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <returns>
        /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException"/>, and a set operation creates a new element with the specified key.
        /// </returns>
        /// <param name="key">The key of the value to get or set.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception><exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key"/> does not exist in the collection.</exception>
        public string this[string key]
        {
            get
            {
                return this.parameterList[key];
            }

            set
            {
                this.parameterList[key] = value;
            }
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary"/> object.
        /// </summary>
        /// <param name="key">The <see cref="T:System.Object"/> to use as the key of the element to add. </param><param name="value">The <see cref="T:System.Object"/> to use as the value of the element to add. </param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null. </exception><exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.IDictionary"/> object. </exception><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IDictionary"/> is read-only.-or- The <see cref="T:System.Collections.IDictionary"/> has a fixed size. </exception>
        public void Add(object key, object value)
        {
            ((IDictionary)this.parameterList).Add(key, value);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains the specified key.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.Dictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool ContainsKey(string key)
        {
            return this.parameterList.ContainsKey(key);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var pair in this.ParameterList)
            {
                sb.Append(pair.Key);
                sb.Append(": ");
                sb.Append(pair.Value);
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}