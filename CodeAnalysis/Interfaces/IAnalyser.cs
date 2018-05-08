using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeAnalysis.Interfaces
{
    public interface IAnalyser : IDisposable
    {
        #region Methods

        /// <summary>
        /// Analyses the assembly.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAnalyserResult> AnalyseAssembly();

        #endregion
    }
}