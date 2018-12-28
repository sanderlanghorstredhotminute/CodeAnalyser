using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeAnalysis.Model;
using Mono.Cecil;

namespace CodeAnalysis
{
    public sealed class AssemblyAnalyser : IAssemblyAnalyser
    {
        #region Fields

        private readonly IList<IAnalyser> _analysers = new List<IAnalyser>();
        private ModuleDefinition _assembly;
        private DefaultAssemblyResolver _resolver;

        #endregion

        #region Properties

        public string Path { get; }

        #endregion

        #region Constructors

        public AssemblyAnalyser(string path)
        {
            Path = path;
            LoadAssembly();
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
            _resolver.Dispose();
            _resolver = null;
            _assembly.Dispose();
            _assembly = null;
        }

        public IEnumerable<IAnalyserResult> GetResults()
        {
            var results = new List<IAnalyserResult>();
            foreach (var analyser in _analysers)
            {
                results.AddRange(analyser.AnalyseAssembly(_assembly));
            }
            return results;
        }

        public async Task<IEnumerable<IAnalyserResult>> GetResultsAsync(IProgress<int> progress, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        private void LoadAssembly()
        {
            _resolver = new DefaultAssemblyResolver();
            _resolver.AddSearchDirectory(System.IO.Path.GetDirectoryName(Path));
            _assembly = ModuleDefinition.ReadModule(Path, new ReaderParameters {AssemblyResolver = _resolver});
        }

        #endregion
    }
}