using System;
using Mono.Cecil;

namespace CodeAnalysis.Model
{
    public interface IModuleLoader : IDisposable
    {
        #region Methods

        ModuleDefinition GetModule(string name);
        ModuleDefinition GetModule();

        #endregion
    }
}