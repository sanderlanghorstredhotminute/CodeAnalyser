using System.Linq;

namespace CodeAnalysis.Classes.Helpers
{
    internal static class ParserHelper
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Converts the string representation of a number to an integer.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int ToInt(this object input, int defaultValue)
        {
            if (input == null)
            {
                return defaultValue;
            }
            int output;
            return int.TryParse(input.ToString(), out output) ? output : defaultValue;
        }
        
        #endregion

        #endregion
    }
}