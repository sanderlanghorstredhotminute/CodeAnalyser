using System.Configuration;

namespace CodeAnalysis.Analysers.Recursive.ConfigSection
{
    public sealed class ConfigCollection<T> : ConfigurationElementCollection where T : ElementBase, new()
    {
        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="T" /> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="T" />.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return (T)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified section.
        /// </summary>
        /// <param name="section">The section.</param>
        public void Add(T section)
        {
            BaseAdd(section);
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// Removes the specified section.
        /// </summary>
        /// <param name="section">The section.</param>
        public void Remove(T section)
        {
            BaseRemove(section.Key);
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)element).Key;
        }

        #endregion
    }
}