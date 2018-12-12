using System.Collections.Generic;
using System.Linq;
using CodeAnalysis.Interfaces;

namespace CodeAnalysis.Classes
{
    public sealed class DependencyResult : IAnalyserResult
    {
        #region Properties

        public IEnumerable<string> Dependents { get; set; }
        public string Details { get; }
        public bool Documented { get; }
        public string Result { get; }
        public string Type { get; set; }

        #endregion
    }
}