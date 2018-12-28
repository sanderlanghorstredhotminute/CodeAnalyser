using System.Collections.Generic;
using System.Linq;
using CodeAnalysis.Model;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeAnalysis.Analysers.Dependency
{
    public sealed class DependencyAnalyser : IAnalyser
    {
        #region Properties

        public bool IncludeSystemTypes { get; set; }

        #endregion

        #region Methods

        public IEnumerable<IAnalyserResult> AnalyseAssembly(ModuleDefinition assembly)
        {
            var references = assembly.Types
                                     .Where(t => t.IsClass)
                                     .Select(t => new
                                     {
                                         Type = t,
                                         Base = t.DeclaringType,
                                         Calls = t.Methods.Where(m => !m.IsAbstract)
                                                  .SelectMany(m =>
                                                                  m.Body?.Instructions.Where(i => new[] {OpCodes.Call, OpCodes.Callvirt, OpCodes.Newobj}.Contains(i.OpCode)).Select(i => i.Operand)
                                                                   .OfType<MethodReference>()
                                                                   .Where(r => r.DeclaringType != t))
                                                  .Where(c => c != null).ToList()
                                     })
                                     .Where(d => d.Calls.Any())
                                     .ToList();

            if (!IncludeSystemTypes)
                references = references.Where(r => r.Calls.Any(c => !c.DeclaringType.FullName.StartsWith("System"))).ToList();
            foreach (var reference in references)
            {
                yield return new DependencyResult
                {
                    Type = reference.Type.FullName,
                    Dependents = reference.Calls
                                          .Where(c => IncludeSystemTypes || !c.DeclaringType.FullName.StartsWith("System"))
                                          .GroupBy(c => c.DeclaringType)
                                          .Select(g => g.Key.FullName)
                };
            }
        }

        #endregion
    }
}