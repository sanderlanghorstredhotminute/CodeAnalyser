using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CodeAnalysis.Model
{
    interface IAssemblyAnalyser : IDisposable
    {
        #region Methods

        void AddAnalyser(IAnalyser analyser);
        IEnumerable<IAnalyserResult> GetResults();
        Task<IEnumerable<IAnalyserResult>> GetResultsAsync(IProgress<int> progress, CancellationToken ct);

        #endregion
    }
}