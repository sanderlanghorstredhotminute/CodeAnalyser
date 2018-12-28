using System.Configuration;
using System.Linq;

namespace CodeAnalysis.Analysers.Recursive.ConfigSection
{
    public sealed class DocumentedElement : ElementBase
    {
        #region Fields

        private string _key;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        [ConfigurationProperty("from", IsRequired = true)]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public override string Key
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_key))
                    return $"{From}=>{To}";
                return _key;
            }
            set { _key = value; }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        [ConfigurationProperty("to", IsRequired = true)]
        public string To { get; set; }

        #endregion
    }
}