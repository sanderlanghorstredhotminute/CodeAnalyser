using System.Collections.Generic;

namespace CodeAnalysis.Analysers.Dependency.Result
{
    public sealed class AssemblyDefinition
    {
        #region Properties

        public IEnumerable<ClassDefinition> Classes { get; set; }
        public string Name { get; }

        #endregion

        #region Constructors

        public AssemblyDefinition(string name)
        {
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
            return obj is AssemblyDefinition && Equals((AssemblyDefinition)obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"[{nameof(AssemblyDefinition)}]{Name}";
        }

        private bool Equals(AssemblyDefinition other)
        {
            return string.Equals(Name, other.Name);
        }

        #endregion
    }
}