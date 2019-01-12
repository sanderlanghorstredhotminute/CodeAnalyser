using System.Collections.Generic;
using System.Linq;
using CodeAnalysis.Analysers.Dependency.Result;
using CodeAnalysis.Model;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeAnalysis.Analysers.Dependency
{
    public sealed class DependencyAnalyser : IAnalyser
    {
        #region Fields

        private static readonly OpCode[] FilteredOpCodes = {OpCodes.Call, OpCodes.Callvirt, OpCodes.Newobj};

        private static readonly string[] InternalModules = {"Mono.Cecil.dll", "Mono.Cecil.Mdb.dll", "Mono.Cecil.Pdb.dll"};

        #endregion

        #region Properties

        public bool IncludeSelfReferences { get; set; }

        public bool IncludeSystemTypes { get; set; }

        #endregion

        #region Constructors

        public DependencyAnalyser() { }

        public DependencyAnalyser(DependencyOptions options)
        {
            IncludeSystemTypes = options.HasFlag(DependencyOptions.IncludeSystemTypes);
            IncludeSelfReferences = options.HasFlag(DependencyOptions.IncludeSelfReferences);
        }

        #endregion

        #region Methods

        public IEnumerable<IAnalyserResult> AnalyseAssembly(IModuleLoader loader)
        {
            var result = new List<DependencyResult>();
            var references = new List<TypeReference>();
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
                references.AddRange(deps);
                loadedModules.Add(module);

                foreach (var scopeName in deps.SelectMany(d => d.References.Select(dd => dd.Scope)).Distinct())
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

            if (references.Any())
            {
                result = Mapper.MapResults(references)
                               .Select(r => new DependencyResult
                               {
                                   Definition = r
                               }).ToList();
            }

            return result;
        }

        private IEnumerable<TypeReference> GetDependencies(ModuleDefinition module)
        {
            foreach (var typeDefinition in module.Types.Where(t => t.IsClass))
            {
                foreach (var methodDefinition in typeDefinition.Methods.Where(m => !m.IsAbstract && m.Body != null))
                {
                    var references = methodDefinition.Body.Instructions
                                                     .Where(i => FilteredOpCodes.Contains(i.OpCode))
                                                     .Select(i => i.Operand).OfType<MethodReference>()
                                                     .Where(mr => IsValidType(typeDefinition, mr))
                                                     .ToList();
                    if (!references.Any())
                        continue;

                    yield return new TypeReference
                    {
                        Scope = typeDefinition.Scope.Name,
                        Class = typeDefinition.Name,
                        Namespace = typeDefinition.Namespace,
                        Method = methodDefinition.Name,
                        References = references.Select(reference => new TypeReference
                        {
                            Scope = $"{reference.DeclaringType.Scope.Name.Replace(".dll", "")}.dll",
                            Class = reference.DeclaringType.Name,
                            Method = reference.Name
                        }).ToList()
                    };
                }
            }
        }

        private bool IsValidType(TypeDefinition typeDefinition, MethodReference methodReferene)
        {
            if (InternalModules.Contains(methodReferene.DeclaringType.Scope.Name))
                return false;

            if (!IncludeSelfReferences && methodReferene.DeclaringType == typeDefinition)
                return false;

            if (!IncludeSystemTypes && methodReferene.DeclaringType.Scope.Name.StartsWith("System"))
                return false;

            return true;
        }

        #endregion
    }
}