using System;
using CodeAnalysis.Model;
using Mono.Cecil;

namespace CodeAnalysis
{
    internal class ModuleLoader : IModuleLoader
    {
        #region Fields

        private ModuleDefinition _assembly;
        private DefaultAssemblyResolver _resolver;

        #endregion

        #region Properties

        private string Path { get; }

        #endregion

        #region Constructors

        public ModuleLoader(string path)
        {
            Path = path;
            _resolver = new DefaultAssemblyResolver();
            _resolver.AddSearchDirectory(System.IO.Path.GetDirectoryName(Path));
            _assembly = ModuleDefinition.ReadModule(Path, new ReaderParameters {AssemblyResolver = _resolver});
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            _resolver.Dispose();
            _resolver = null;
            _assembly.Dispose();
            _assembly = null;
        }

        public ModuleDefinition GetModule()
        {
            return _assembly;
        }

        public ModuleDefinition GetModule(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return _assembly;

            try
            {
                var moduleName = name.ToLower().Replace(".dll", "").Replace(".exe", "");
                var assembly = _resolver.Resolve(new AssemblyNameDefinition(moduleName, new Version(0, 0)));
                return assembly?.MainModule;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}