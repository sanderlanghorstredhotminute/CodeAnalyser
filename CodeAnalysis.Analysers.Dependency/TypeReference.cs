using System.Collections.Generic;
using System.Linq;

namespace CodeAnalysis.Analysers.Dependency
{
    internal sealed class TypeReference
    {
        #region Properties

        public string Class { get; set; }

        public string Method { get; set; }

        public string Namespace { get; set; }

        public IEnumerable<TypeReference> References { get; set; }

        public string Scope { get; set; }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((TypeReference)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Class != null ? Class.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Method != null ? Method.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Scope != null ? Scope.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"[{nameof(TypeReference)}]{Scope}: {Namespace}.{Class}..{Method} ({References.Count()})";
        }

        protected bool Equals(TypeReference other)
        {
            return string.Equals(Class, other.Class) && string.Equals(Method, other.Method) && string.Equals(Scope, other.Scope);
        }

        #endregion
    }
}