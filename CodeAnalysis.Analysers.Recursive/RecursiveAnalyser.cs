using System.Collections.Generic;
using System.Linq;
using CodeAnalysis.Model;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace CodeAnalysis.Analysers.Recursive
{
    public sealed class RecursiveAnalyser : IAnalyser
    {
        #region Properties

        /// <summary>
        /// Gets or sets the maximum depth.
        /// </summary>
        /// <value>
        /// The maximum depth.
        /// </value>
        internal int MaxDepth { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// </summary>
        public RecursiveAnalyser()
        {
            MaxDepth = SettingsHelper.MaxRecursiveLevel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Analyses the assembly.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAnalyserResult> AnalyseAssembly(ModuleDefinition assembly)
        {
            var calls =
                (from type in assembly.Types
                 from caller in type.Methods
                 where caller != null && caller.Body != null
                 from instruction in caller.Body.Instructions
                 where instruction.OpCode == OpCodes.Call
                 let called = instruction.Operand as MethodReference
                 select new CallProfile {Type = type, Caller = caller, Called = called}).Distinct().ToList();

            var stacks = GetRecursives(calls);

            return stacks.Select(s => new RecursiveResult(s));
        }

        /// <summary>
        /// Gets the recursives.
        /// </summary>
        /// <param name="calls">The calls.</param>
        /// <returns></returns>
        private IEnumerable<CallerStack> GetRecursives(IEnumerable<CallProfile> calls)
        {
            var direct = calls.Where(c => c.Caller == c.Called).ToList();

            var stacks = calls.Select(c => new CallerStack(c)).ToList();
            foreach (var st in stacks.Join(direct, s => s.Base, i => i, (s, i) => new {s, i}))
            {
                st.s.Ended = st.s.Recursive = true;
            }

            var level = 1;
            while (level < MaxDepth)
            {
                foreach (var stack in stacks.Where(s => !s.Recursive && !s.Ended))
                {
                    var next = calls.FirstOrDefault(c => c.Caller == stack.Last.Called);
                    if (next != null)
                    {
                        stack.Add(next);
                    }
                    else
                    {
                        stack.Ended = true;
                    }
                }

                foreach (var recursivestack in stacks.Where(s => !s.Ended && s.Base.Caller == s.Last.Called))
                {
                    recursivestack.Ended = recursivestack.Recursive = true;
                }

                level++;
            }
            return stacks.Where(s => s.Recursive).ToList();
        }

        #endregion
    }
}