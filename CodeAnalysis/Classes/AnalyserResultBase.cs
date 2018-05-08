using System.Linq;
using CodeAnalysis.Interfaces;

namespace CodeAnalysis.Classes
{
    internal abstract class AnalyserResultBase : IAnalyserResult
    {
        #region Properties

        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string Details { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IAnalyserResult" /> is documented. Thus ignored.
        /// </summary>
        /// <value>
        /// <c>true</c> if documented; otherwise, <c>false</c>.
        /// </value>
        public bool Documented { get; protected set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public string Result { get; protected set; }

        #endregion
    }
}