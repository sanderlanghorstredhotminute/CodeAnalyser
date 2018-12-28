using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;

namespace CodeAnalysis.Model
{
    public interface IAnalyser
    {
        #region Methods

        /// <summary>
        /// Analyses the assembly.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAnalyserResult> AnalyseAssembly(ModuleDefinition definition);

        #endregion
    }
}