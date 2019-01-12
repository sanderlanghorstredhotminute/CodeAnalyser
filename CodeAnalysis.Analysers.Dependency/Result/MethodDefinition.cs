using System.Collections.Generic;

namespace CodeAnalysis.Analysers.Dependency.Result
{
    public sealed class MethodDefinition
    {
        #region Properties

        public ClassDefinition Class { get; }
        public IEnumerable<MethodDefinition> Dependencies { get; set; }
        public string Name { get; }

        #endregion

        #region Constructors

        public MethodDefinition(ClassDefinition @class, string name)
        {
            Class = @class;
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
            return obj is MethodDefinition && Equals((MethodDefinition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Class != null ? Class.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"[{nameof(MethodDefinition)}]{Name}";
        }

        private bool Equals(MethodDefinition other)
        {
            return Equals(Class, other.Class) && string.Equals(Name, other.Name);
        }

        #endregion
    }
}