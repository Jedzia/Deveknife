// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Guard.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2015, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
// <author>Jedzia</author>
// <email>jed69@gmx.de</email>
//  <date>13.10.2015 11:31</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq.Expressions;

    /// <summary>
    /// Expression style argument checking.
    /// </summary>
    [DebuggerStepThrough]
    public static class Guard
    {
        private const string ArgumentCannotBeEmpty = "Argument cannot be null or empty";

        private const string CannotEqual = "Value cannot equal the default";

        private const string TypeNotImplementInterface = "{0} does not implement {1}";

        private const string TypeNotInheritFromType = "{0} does not inherite from {1}";

        /// <summary>
        /// Determines whether this instance [can be assigned] with the specified reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="typeToAssign">The type to assign.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <exception cref="ArgumentException"><c>ArgumentException</c>.</exception>
        public static void CanBeAssigned(
            [NotNull] Expression<Func<object>> reference,
            [NotNull] Type typeToAssign,
            [NotNull] Type targetType)
        {
            if (!targetType.IsAssignableFrom(typeToAssign))
            {
                if (targetType.IsInterface)
                {
                    throw new ArgumentException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            TypeNotImplementInterface,
                            typeToAssign,
                            targetType),
                        Guard.GetParameterName(reference));
                }

                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        TypeNotInheritFromType,
                        typeToAssign,
                        targetType),
                    Guard.GetParameterName(reference));
            }
        }

        /// <summary>
        /// Ensures the given <paramref name="value"/> is not the <c>default</c>.
        /// Throws <see cref="ArgumentException"/> otherwise.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="reference">The reference.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentException">Value cannot equal the default</exception>
        public static void NotDefault<T>([NotNull] Expression<Func<T>> reference, [NotNull] T value)
        {
            if (value.Equals(default(T)))
            {
                throw new ArgumentException(CannotEqual, Guard.GetParameterName(reference));
            }
        }

        /// <summary>
        /// Ensures the given <paramref name="value" /> is not <c>null</c>.
        /// Throws <see cref="ArgumentNullException" /> otherwise.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="reference">The reference.</param>
        /// <param name="value">The value.</param>
        /// <returns> the checked value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is <c>null</c>.</exception>
        [NotNull]
        public static T NotNull<T>([NotNull][NoEnumerationAttribute] Expression<Func<T>> reference, [CanBeNull] T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(Guard.GetParameterName(reference));
            }

            return value;
        }

        /* Fuck, does not work
        /// <summary>
        /// Ensures the given <paramref name="value" /> is not <c>null</c>.
        /// Throws <see cref="ArgumentNullException" /> otherwise.
        /// </summary>
        /// <typeparam name="T">The type of the parameter.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parameterName"/> is <see langword="null" />.</exception>
        public static void NotNull<T>(T value, [CallerMemberName] string parameterName = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
        */

        /// <summary>
        /// Ensures the given string <paramref name="value" /> is not <c>null</c> or empty.
        /// Throws <see cref="ArgumentNullException" /> in the first case, or
        /// <see cref="ArgumentException" /> in the latter.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <param name="value">The value.</param>
        /// <returns>the checked input string.</returns>
        /// <exception cref="ArgumentException">Argument cannot be <c>null</c> or empty</exception>
        [NotNull]
        public static string NotNullOrEmpty([NotNull] Expression<Func<string>> reference, [CanBeNull] string value)
        {
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            Guard.NotNull<string>(reference, value);
            // ReSharper disable once PossibleNullReferenceException
            if (value.Length == 0)
            {
                throw new ArgumentException(ArgumentCannotBeEmpty, Guard.GetParameterName(reference));
            }

            return value;
        }

        /// <summary>
        /// Checks an argument to ensure it is in the specified range excluding the edges.
        /// </summary>
        /// <typeparam name="T">Type of the argument to check, it must be an <see cref="IComparable"/> type.
        /// </typeparam>
        /// <param name="reference">The expression containing the name of the argument.</param>
        /// <param name="value">The argument value to check.</param>
        /// <param name="from">The minimum allowed value for the argument.</param>
        /// <param name="to">The maximum allowed value for the argument.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c></c> is out of range.</exception>
        public static void NotOutOfRangeExclusive<T>(
            [NotNull] Expression<Func<T>> reference,
            [CanBeNull] T value,
            [NotNull] T from,
            [NotNull] T to)
            where T : IComparable
        {
            if (value != null && (value.CompareTo(from) <= 0 || value.CompareTo(to) >= 0))
            {
                throw new ArgumentOutOfRangeException(Guard.GetParameterName(reference));
            }
        }

        /// <summary>
        /// Checks an argument to ensure it is in the specified range including the edges.
        /// </summary>
        /// <typeparam name="T">Type of the argument to check, it must be an <see cref="IComparable"/> type.
        /// </typeparam>
        /// <param name="reference">The expression containing the name of the argument.</param>
        /// <param name="value">The argument value to check.</param>
        /// <param name="from">The minimum allowed value for the argument.</param>
        /// <param name="to">The maximum allowed value for the argument.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c></c> is out of range.</exception>
        public static void NotOutOfRangeInclusive<T>(
            [NotNull] Expression<Func<T>> reference,
            [CanBeNull] T value,
            [NotNull] T from,
            [NotNull] T to)
            where T : IComparable
        {
            if (value != null && (value.CompareTo(from) < 0 || value.CompareTo(to) > 0))
            {
                throw new ArgumentOutOfRangeException(Guard.GetParameterName(reference));
            }
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        /// <param name="reference">The reference to the parameter.</param>
        /// <returns>The name of the parameter.</returns>
        private static string GetParameterName(LambdaExpression reference)
        {
            var member = (MemberExpression)reference.Body;
            return member.Member.Name;
        }
    }
}