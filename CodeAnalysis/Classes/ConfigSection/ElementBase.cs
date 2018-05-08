using System.Configuration;
using System.Linq;

namespace CodeAnalysis.Classes.ConfigSection
{
    public abstract class ElementBase : ConfigurationElement
    {
        #region Properties

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public abstract string Key { get; set; }

        #endregion
    }
}