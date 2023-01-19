using System;

namespace FastWildcard
{
    public class MatchSettings
    {
        private const StringComparison DefaultStringComparison = StringComparison.Ordinal;

        /// <summary>
        /// Case rules to use when comparing matching characters.
        /// Defaults to <see cref="StringComparison.Ordinal" />.
        /// </summary>
        public StringComparison StringComparison { get; set; }

        public MatchSettings()
        {
            StringComparison = DefaultStringComparison;
        }
    }
}
