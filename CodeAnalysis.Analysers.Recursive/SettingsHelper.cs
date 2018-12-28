using System.Configuration;
using System.Linq;
using CodeAnalysis.Analysers.Recursive.ConfigSection;

namespace CodeAnalysis.Analysers.Recursive
{
    internal static class SettingsHelper
    {
        #region Properties

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <value>
        /// The configuration section.
        /// </value>
        public static RecursiveAnalyserSection ConfigSection => ConfigurationManager.GetSection("CodeAnalysis/RecursiveAnalyser") as RecursiveAnalyserSection;

        /// <summary>
        /// Gets the maximum recursive level.
        /// </summary>
        /// <value>
        /// The maximum recursive level.
        /// </value>
        public static int MaxRecursiveLevel => ConfigSection?.MaxRecursionDepth ?? 10;

        #endregion
    }
}