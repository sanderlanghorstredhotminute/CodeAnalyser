using System.Linq;
using Mono.Cecil;

namespace CodeAnalysis.Classes.Helpers
{
    internal static class MonoCecilExtensions
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="definition">The definition.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        internal static string ToString(this TypeDefinition definition)
        {
            return $"{definition.Namespace}{definition.Name}";
        }

        #endregion

        #endregion
    }
}