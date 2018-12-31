using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeAnalysis.Model;

namespace CodeAnalysis
{
    public sealed class AssemblyAnalyser : IAssemblyAnalyser
    {
        #region Fields

        private IList<IAnalyser> _analysers = new List<IAnalyser>();
        private IModuleLoader _assemblyLoader;

        #endregion

        #region Properties

        public string Path { get; }

        #endregion

        #region Constructors

        public AssemblyAnalyser(string path)
        {
            _assemblyLoader = new ModuleLoader(path);
        }

        #endregion

        #region Methods

        public void AddAnalyser(IAnalyser analyser)
        {
            if (analyser == null)
                throw new ArgumentNullException(nameof(analyser));
            _analysers.Add(analyser);
        }

        public void Dispose()
        {
            _assemblyLoader.Dispose();
            _assemblyLoader = null;
            _analysers = null;
        }

        public IEnumerable<IAnalyserResult> GetResults()
        {
            var results = new List<IAnalyserResult>();
            foreach (var analyser in _analysers)
            {
                results.AddRange(analyser.AnalyseAssembly(_assemblyLoader));
            }
            return results;
        }

        public async Task<IEnumerable<IAnalyserResult>> GetResultsAsync(IProgress<int> progress, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}