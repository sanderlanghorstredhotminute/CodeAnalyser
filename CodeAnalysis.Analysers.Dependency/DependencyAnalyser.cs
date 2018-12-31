using System.Collections.Generic;
using System.Linq;
using CodeAnalysis.Model;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeAnalysis.Analysers.Dependency
{
    public sealed class DependencyAnalyser : IAnalyser
    {
        #region Fields

        private static readonly OpCode[] FilteredOpCodes = {OpCodes.Call, OpCodes.Callvirt, OpCodes.Newobj};

        private static readonly string[] InternalModules = {"Mono.Cecil", "Mono.Cecil.Mdb", "Mono.Cecil.Pdb"};

        #endregion

        #region Properties

        public bool IncludeSystemTypes { get; set; }

        #endregion

        #region Constructors

        public DependencyAnalyser()
        {
            IncludeSystemTypes = false;
        }

        public DependencyAnalyser(bool includeSystemTypes)
        {
            IncludeSystemTypes = includeSystemTypes;
        }

        #endregion

        #region Methods

        public IEnumerable<IAnalyserResult> AnalyseAssembly(IModuleLoader loader)
        {
            var result = new List<DependencyResult>();
            var loadedModules = new List<ModuleDefinition>();
            var unloadedModules = new Queue<string>();
            unloadedModules.Enqueue(null);

            while (unloadedModules.Count > 0)
            {
                var moduleName = unloadedModules.Dequeue();
                var module = loader.GetModule(moduleName);
                if (module == null)
                    continue;

                var deps = GetDependencies(module).ToList();
                result.AddRange(deps);
                loadedModules.Add(module);

                foreach (var scopeName in deps.SelectMany(d => d.Dependents.Select(dd => dd.Scope)).Distinct())
                {
                    if (InternalModules.Contains(scopeName))
                        continue;
                    if (loadedModules.Any(m => m.Name == scopeName))
                        continue;
                    if (unloadedModules.Contains(scopeName))
                        continue;
                    unloadedModules.Enqueue(scopeName);
                }
            }

            return result;
        }

        private IEnumerable<DependencyResult> GetDependencies(ModuleDefinition module)
        {
            foreach (var typeDefinition in module.Types.Where(t => t.IsClass))
            {
                foreach (var methodDefinition in typeDefinition.Methods.Where(m => !m.IsAbstract && m.Body != null))
                {
                    var references = methodDefinition.Body.Instructions
                                                     .Where(i => FilteredOpCodes.Contains(i.OpCode))
                                                     .Select(i => i.Operand).OfType<MethodReference>()
                                                     .Where(mr => mr.DeclaringType != typeDefinition
                                                                  && (IncludeSystemTypes || !mr.DeclaringType.Scope.Name.StartsWith("System"))
                                                                  && !InternalModules.Contains(mr.DeclaringType.Scope.Name))
                                                     .ToList();
                    if (!references.Any())
                        continue;

                    yield return new DependencyResult
                    {
                        Type = new TypeReference
                        {
                            Scope = typeDefinition.Scope.Name,
                            Class = typeDefinition.Name,
                            Method = methodDefinition.Name
                        },
                        Dependents = references.Select(reference => new TypeReference
                        {
                            Scope = $"{reference.DeclaringType.Scope.Name.Replace(".dll", "")}.dll",
                            Class = reference.DeclaringType.Name,
                            Method = reference.Name
                        }).ToList()
                    };
                }
            }
        }

        #endregion
    }
}