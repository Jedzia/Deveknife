// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SoundexExtension.cs" company="EvePanix">
//   Copyright (c) Jedzia 2001-2014, EvePanix. All rights reserved. See the license notes shipped with this source and the GNU GPL.
// </copyright>
//  <author>Jedzia</author>
//  <email>jed69@gmx.de</email>
//  <date>02.03.2014 03:54</date>
// --------------------------------------------------------------------------------------------------------------------

namespace Deveknife.Blades.Overview.Util
{
    using System;
    using System.Text;

    /// <summary>
    /// Soundex is a phonetic algorithm for indexing names by sound, as pronounced in English.
    /// The goal is for homophones to be encoded to the same representation so that they can be matched 
    /// despite minor differences in spelling.[1] The algorithm mainly encodes consonants; a vowel will 
    /// not be encoded unless it is the first letter. Soundex is the most widely known of all phonetic 
    /// algorithms (in part because it is a standard feature of popular database software such as DB2, 
    /// PostgreSQL,[2] MySQL,[3], Ingres, MS SQL Server[4] and Oracle[5]) and is often used (incorrectly) 
    /// as a synonym for "phonetic algorithm".[citation needed] Improvements to Soundex are the basis for 
    /// many modern phonetic algorithms.
    /// </summary>
    public static class SoundexExtension
    {
        /// <summary>
        /// The difference function will match the two soundex strings and return 0 to 4. 0 means Not Matched, 4 means Strongly Matched.
        /// </summary>
        /// <param name="data1">
        /// The first string to compare.
        /// </param>
        /// <param name="data2">
        /// The second string to compare.
        /// </param>
        /// <returns>
        /// an integer value between 0 to 4. 0 means Not Matched, 4 means Strongly Matched.
        /// </returns>
        public static int Difference(this string data1, string data2)
        {
            var result = 0;

            if (data1.Equals(string.Empty) || data2.Equals(string.Empty))
            {
                return 0;
            }

            var soundex1 = Soundex(data1);
            var soundex2 = Soundex(data2);

            if (soundex1.Equals(soundex2))
            {
                result = 4;
            }
            else
            {
                if (soundex1[0] == soundex2[0])
                {
                    result = 1;
                }

                var sub1 = soundex1.Substring(1, 3); // characters 2, 3, 4

                if (soundex2.IndexOf(sub1, StringComparison.Ordinal) > -1)
                {
                    result += 3;
                    return result;
                }

                var sub2 = soundex1.Substring(2, 2); // characters 3, 4

                if (soundex2.IndexOf(sub2, StringComparison.Ordinal) > -1)
                {
                    result += 2;
                    return result;
                }

                var sub3 = soundex1.Substring(1, 2); // characters 2, 3

                if (soundex2.IndexOf(sub3, StringComparison.Ordinal) > -1)
                {
                    result += 2;
                    return result;
                }

                var sub4 = soundex1[1];
                if (soundex2.IndexOf(sub4) > -1)
                {
                    result++;
                }

                var sub5 = soundex1[2];
                if (soundex2.IndexOf(sub5) > -1)
                {
                    result++;
                }

                var sub6 = soundex1[3];
                if (soundex2.IndexOf(sub6) > -1)
                {
                    result++;
                }
            }

            return result;
        }

        /// <summary>
        /// Soundex is a phonetic algorithm for indexing names by sound, as pronounced in English. The goal 
        /// is for homophones to be encoded to the same representation so that they can be matched despite 
        /// minor differences in spelling.
        /// </summary>
        /// <param name="data">
        /// The string to build the soundex of.
        /// </param>
        /// <returns>
        /// the 4 character soundex of a given string.
        /// </returns>
        public static string Soundex(this string data)
        {
            var result = new StringBuilder();

            if (!string.IsNullOrEmpty(data))
            {
                result.Append(char.ToUpper(data[0]));
                var previousCode = string.Empty;

                for (var i = 1; i < data.Length; i++)
                {
                    var currentCode = EncodeChar(data[i]);

                    if (currentCode != previousCode)
                    {
                        result.Append(currentCode);
                    }

                    if (result.Length == 4)
                    {
                        break;
                    }

                    if (!currentCode.Equals(string.Empty))
                    {
                        previousCode = currentCode;
                    }
                }
            }

            if (result.Length < 4)
            {
                result.Append(new string('0', 4 - result.Length));
            }

            return result.ToString();
        }

        private static string EncodeChar(char c)
        {
            switch (char.ToLower(c))
            {
                case 'b':
                case 'f':
                case 'p':
                case 'v':
                    return "1";
                case 'c':
                case 'g':
                case 'j':
                case 'k':
                case 'q':
                case 's':
                case 'x':
                case 'z':
                    return "2";
                case 'd':
                case 't':
                    return "3";
                case 'l':
                    return "4";
                case 'm':
                case 'n':
                    return "5";
                case 'r':
                    return "6";
                default:
                    return string.Empty;
            }
        }
    }
}