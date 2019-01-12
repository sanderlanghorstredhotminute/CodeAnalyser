using System.Collections.Generic;
using System.Linq;

namespace CodeAnalysis.Analysers.Dependency.Result
{
    internal static class Mapper
    {
        #region Methods

        #region Static Methods

        /// <summary>Maps the dependency references to the result objects.</summary>
        /// <param name="references">The references.</param>
        /// <returns></returns>
        internal static IEnumerable<AssemblyDefinition> MapResults(IEnumerable<TypeReference> references)
        {
            var definitions = new List<AssemblyDefinition>();

            var groupings = references.GroupBy(scmg => new {scmg.Scope, scmg.Namespace, scmg.Class, scmg.Method})
                                      .GroupBy(scg => new {scg.Key.Scope, scg.Key.Namespace, scg.Key.Class})
                                      .GroupBy(sg => new {sg.Key.Scope})
                                      .ToList();

            //Define all definitions
            foreach (var scopeGrouping in groupings)
            {
                var assembly = new AssemblyDefinition(scopeGrouping.Key.Scope);
                var classes = new List<ClassDefinition>();

                foreach (var classGrouping in scopeGrouping)
                {
                    var @class = new ClassDefinition(assembly, classGrouping.Key.Namespace, classGrouping.Key.Class);
                    var methods = classGrouping.Select(methodGrouping => new MethodDefinition(@class, methodGrouping.Key.Method)).ToList();

                    @class.Methods = methods;
                    classes.Add(@class);
                }
                assembly.Classes = classes;
                definitions.Add(assembly);
            }

            //link all references to definitions
            foreach (var scopeGrouping in groupings)
            {
                foreach (var classGrouping in scopeGrouping)
                {
                    foreach (var methodGrouping in classGrouping)
                    {
                        //referencing definition;
                        var obj = definitions.First(d => d.Name == scopeGrouping.Key.Scope)
                                             .Classes.First(c => c.Name == classGrouping.Key.Class)
                                             .Methods.First(m => m.Name == methodGrouping.Key.Method);
                        //referenced definition;
                        var dependencies = new List<MethodDefinition>();
                        foreach (var typeReference in methodGrouping.SelectMany(mg => mg.References))
                        {
                            var dependency = definitions.FirstOrDefault(d => d.Name == typeReference.Scope)?
                                .Classes.FirstOrDefault(c => c.Name == typeReference.Class)?
                                .Methods.FirstOrDefault(m => m.Name == typeReference.Method);
                            if (dependency != null)
                                dependencies.Add(dependency);
                        }
                        obj.Dependencies = dependencies;
                    }
                }
            }
            return definitions;
        }

        #endregion

        #endregion
    }
}