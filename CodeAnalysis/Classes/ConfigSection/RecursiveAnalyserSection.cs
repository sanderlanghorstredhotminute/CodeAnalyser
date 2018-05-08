using System.Configuration;
using CodeAnalysis.Classes.Helpers;
using System.Linq;

namespace CodeAnalysis.Classes.ConfigSection
{
    public sealed class RecursiveAnalyserSection : ConfigurationSection
    {
        #region Properties

        /// <summary>
        /// Gets or sets the documented.
        /// </summary>
        /// <value>
        /// The documented.
        /// </value>
        [ConfigurationProperty("Documented")]
        [ConfigurationCollection(typeof(ConfigCollection<DocumentedElement>), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public ConfigCollection<DocumentedElement> Documented => (ConfigCollection<DocumentedElement>)this["Documented"];

        /// <summary>
        /// Gets the maximum recursion depth.
        /// </summary>
        /// <value>
        /// The maximum recursion depth.
        /// </value>
        [ConfigurationProperty("MaxRecursionDepth")]
        public int MaxRecursionDepth => this["MaxRecursionDepth"].ToInt(3);

        #endregion
    }
}