using System.Collections.Generic;
using System.Linq;
using CodeAnalysis.Model;

namespace CodeAnalysis.Analysers.Dependency
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