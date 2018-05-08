using System.Linq;

namespace CodeAnalysis.Interfaces
{
    public interface IAnalyserResult
    {
        #region Properties

        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        string Details { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IAnalyserResult" /> is documented. Thus ignored.
        /// </summary>
        /// <value>
        /// <c>true</c> if documented; otherwise, <c>false</c>.
        /// </value>
        bool Documented { get; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        string Result { get; }

        #endregion
    }
}