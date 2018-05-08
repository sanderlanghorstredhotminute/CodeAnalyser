using System.Linq;
using System.Text;

namespace CodeAnalysis.Classes
{
    internal sealed class RecursiveResult : AnalyserResultBase
    {
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