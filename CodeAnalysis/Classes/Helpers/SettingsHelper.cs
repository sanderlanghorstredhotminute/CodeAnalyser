using System.Configuration;
using System.Linq;
using CodeAnalysis.Classes.ConfigSection;

namespace CodeAnalysis.Classes.Helpers
{
    internal static class SettingsHelper
    {
        #region Properties

        /// <summary>
        /// Gets the maximum recursive level.
        /// </summary>
        /// <value>
        /// The maximum recursive level.
        /// </value>
        public static int MaxRecursiveLevel => ConfigSection?.MaxRecursionDepth ?? 10;

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <value>
        /// The configuration section.
        /// </value>
        public static RecursiveAnalyserSection ConfigSection => ConfigurationManager.GetSection("CodeAnalysis/RecursiveAnalyser") as RecursiveAnalyserSection;

        #endregion
    }
}