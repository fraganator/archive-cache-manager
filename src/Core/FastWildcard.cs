using System;

namespace FastWildcard
{
    public class FastWildcard
    {
        private const char SingleWildcardCharacter = '?';
        private const char MultiWildcardCharacter = '*';

        private static readonly char[] WildcardCharacters = {SingleWildcardCharacter, MultiWildcardCharacter};

        /// <summary>
        /// Returns if the input string <paramref name="str"/> matches the given wildcard pattern <paramref name="pattern"/>.
        /// Uses default <see cref="MatchSettings"/>.
        /// </summary>
        /// <param name="str">Input string to match on</param>
        /// <param name="pattern">Wildcard pattern to match against</param>
        /// <returns>True if a match is found, false otherwise</returns>
        public static bool IsMatch(string str, string pattern)
        {
            return IsMatch(str, pattern, new MatchSettings());
        }

        /// <summary>
        /// Returns if the input string <paramref name="str"/> matches the given wildcard pattern <paramref name="pattern"/>.
        /// Uses the supplied <see cref="MatchSettings"/>.
        /// </summary>
        /// <param name="str">Input string to match on</param>
        /// <param name="pattern">Wildcard pattern to match with</param>
        /// <param name="matchSettings">Match settings to use</param>
        /// <returns>True if a match is found, false otherwise</returns>
        public static bool IsMatch(string str, string pattern, MatchSettings matchSettings)
        {
            // Pattern must contain something
            if (String.IsNullOrEmpty(pattern))
            {
                throw new ArgumentOutOfRangeException(nameof(pattern));
            }

            // Uninitialised string never matches
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            // Multi character wildcard matches everything
            if (pattern == "*")
            {
                return true;
            }

            // Empty string does not match
            if (str.Length == 0)
            {
                return false;
            }

#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
            var strSpan = str.AsSpan();
            var patternSpan = pattern.AsSpan();
#endif

            var strIndex = 0;

            for (var patternIndex = 0; patternIndex < pattern.Length; patternIndex++)
            {
                var patternCh = pattern[patternIndex];
#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
                var patternChSpan = patternSpan.Slice(patternIndex, 1);
#endif

                if (strIndex == str.Length)
                {
                    // At end of pattern for this longer string so always matches '*'
                    if (patternCh == '*' && patternIndex == pattern.Length - 1)
                    {
                        return true;
                    }

                    return false;
                }

                // Character match
#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
                var strCh = strSpan.Slice(strIndex, 1);
                if (patternChSpan.Equals(strCh, matchSettings.StringComparison))
#else
                var strCh = str[strIndex];
                var patternChEqualsStrAtIndex = matchSettings.StringComparison == StringComparison.Ordinal
                    ? patternCh.Equals(strCh)
                    : patternCh.ToString().Equals(strCh.ToString(), matchSettings.StringComparison);
                if (patternChEqualsStrAtIndex)
#endif
                {
                    strIndex++;
                    continue;
                }

                // Single wildcard match
                if (patternCh == '?')
                {
                    strIndex++;
                    continue;
                }

                // No match
                if (patternCh != '*')
                {
                    return false;
                }

                // Multi character wildcard - last character in the pattern
                if (patternIndex == pattern.Length - 1)
                {
                    return true;
                }

                // Match pattern to input string character-by-character until the next wildcard (or end of string if there is none)
                var patternChMatchStartIndex = patternIndex + 1;

                var nextWildcardIndex = pattern.IndexOfAny(WildcardCharacters, patternChMatchStartIndex);
                var patternChMatchEndIndex = nextWildcardIndex == -1
                    ? pattern.Length - 1
                    : nextWildcardIndex - 1;

                var comparisonLength = patternChMatchEndIndex - patternIndex;

#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
                var comparison = patternSpan.Slice(patternChMatchStartIndex, comparisonLength);
                var skipToStringIndex = strSpan.Slice(strIndex).IndexOf(comparison, matchSettings.StringComparison) + strIndex;
#else
                var comparison = pattern.Substring(patternChMatchStartIndex, comparisonLength);
                var skipToStringIndex = str.IndexOf(comparison, strIndex, matchSettings.StringComparison);
#endif

                // Handle repeated instances of the same character at end of pattern
                if (comparisonLength == 1 && nextWildcardIndex == -1)
                {
                    var skipCandidateIndex = 0;
                    while (skipCandidateIndex == 0)
                    {
                        var skipToStringIndexNew = skipToStringIndex + 1;
#if (NETSTANDARD || NETCOREAPP) && !NETSTANDARD1_3 && !NETSTANDARD2_0
                        skipCandidateIndex = strSpan.Slice(skipToStringIndexNew).IndexOf(comparison, matchSettings.StringComparison);
#else
                        skipCandidateIndex = str.IndexOf(comparison, skipToStringIndexNew, matchSettings.StringComparison) - (skipToStringIndexNew);
#endif
                        if (skipCandidateIndex == 0)
                        {
                            skipToStringIndex = skipToStringIndexNew;
                        }
                    }
                }

                if (skipToStringIndex == -1)
                {
                    return false;
                }

                strIndex = skipToStringIndex;
            }

            // Pattern processing completed but rest of input string was not
            if (strIndex < str.Length)
            {
                return false;
            }

            return true;
        }
    }
}
