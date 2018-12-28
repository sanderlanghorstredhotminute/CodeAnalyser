using System.Linq;
using System.Text;
using CodeAnalysis.Model;

namespace CodeAnalysis.Analysers.Recursive
{
    internal sealed class RecursiveResult : IAnalyserResult
    {
        #region Properties

        public string Details { get; }
        public bool Documented { get; }
        public string Result { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RecursiveResult" /> class.
        /// </summary>
        /// <param name="stack">The stack.</param>
        public RecursiveResult(CallerStack stack)
        {
            Result = $"{stack.Base.Type} is recursive";
            var sb = new StringBuilder();
            foreach (var callProfile in stack.Stack)
            {
                sb.Append(callProfile.Caller.Name);
                sb.Append("=>");
                sb.Append(callProfile.Called.Name);
            }
            Details = sb.ToString();
        }

        #endregion
    }
}