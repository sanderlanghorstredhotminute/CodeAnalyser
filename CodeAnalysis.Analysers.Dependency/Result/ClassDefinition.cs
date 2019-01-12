using System.Collections.Generic;
using System.Linq;

namespace CodeAnalysis.Analysers.Dependency.Result
{
    public sealed class ClassDefinition
    {
        #region Properties

        public AssemblyDefinition Assembly { get; }
        public IEnumerable<MethodDefinition> Methods { get; set; }
        public string Name { get; }
        public string Namespace { get; }

        #endregion

        #region Constructors

        public ClassDefinition(AssemblyDefinition assembly, string ns, string name)
        {
            Assembly = assembly;
            Namespace = ns;
            Name = name;
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj is ClassDefinition && Equals((ClassDefinition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Assembly != null ? Assembly.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Namespace != null ? Namespace.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"[{nameof(ClassDefinition)}]{Name}";
        }

        private bool Equals(ClassDefinition other)
        {
            return string.Equals(Assembly, other.Assembly) && string.Equals(Name, other.Name) && string.Equals(Namespace, other.Namespace);
        }

        #endregion
    }
}