using System.Linq;
using CodeAnalysis.Analysers.Dependency.Result;
using CodeAnalysis.Model;

namespace CodeAnalysis.Analysers.Dependency
{
    public sealed class DependencyResult : IAnalyserResult
    {
        #region Properties

        public AssemblyDefinition Definition { get; set; }
        public string Details => Result;
        public bool Documented => false;
        public string Result => string.Concat(Definition.Name, ": ", string.Join(", ", Definition.Classes.SelectMany(c => c.Methods.SelectMany(m => m.Dependencies))));

        #endregion
    }
}