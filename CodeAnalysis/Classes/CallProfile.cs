using System.Linq;
using Mono.Cecil;

namespace CodeAnalysis.Classes
{
    internal sealed class CallProfile
    {
        #region Properties

        /// <summary>
        /// Gets or sets the called.
        /// </summary>
        /// <value>
        /// The called.
        /// </value>
        public MethodReference Called { get; set; }

        /// <summary>
        /// Gets or sets the caller.
        /// </summary>
        /// <value>
        /// The caller.
        /// </value>
        public MethodDefinition Caller { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public TypeDefinition Type { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var profile = obj as CallProfile;
            if (profile != null)
            {
                return profile.Type == Type && profile.Caller == Caller && profile.Called == Called;
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return Type.GetHashCode() + Caller.GetHashCode() + Called.GetHashCode();
        }

        #endregion
    }
}