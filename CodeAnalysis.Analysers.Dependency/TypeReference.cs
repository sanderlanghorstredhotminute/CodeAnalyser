namespace CodeAnalysis.Analysers.Dependency
{
    public class TypeReference
    {
        #region Properties

        public string Class { get; set; }
        public string Method { get; set; }
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

        protected bool Equals(TypeReference other)
        {
            return string.Equals(Class, other.Class) && string.Equals(Method, other.Method) && string.Equals(Scope, other.Scope);
        }

        #endregion
    }
}