using System.Collections.Generic;
using System.Linq;

namespace CodeAnalysis.Analysers.Recursive
{
    internal sealed class CallerStack
    {
        #region Properties

        /// <summary>
        /// Gets or sets the base.
        /// </summary>
        /// <value>
        /// The base.
        /// </value>
        public CallProfile Base { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CallerStack" /> is ended.
        /// </summary>
        /// <value>
        /// <c>true</c> if ended; otherwise, <c>false</c>.
        /// </value>
        public bool Ended { get; set; }

        /// <summary>
        /// Gets the last.
        /// </summary>
        /// <value>
        /// The last.
        /// </value>
        public CallProfile Last => Stack.Last();

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CallerStack" /> is recursive.
        /// </summary>
        /// <value>
        /// <c>true</c> if recursive; otherwise, <c>false</c>.
        /// </value>
        public bool Recursive { get; set; }

        /// <summary>
        /// Gets or sets the stack.
        /// </summary>
        /// <value>
        /// The stack.
        /// </value>
        public List<CallProfile> Stack { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CallerStack" /> class.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public CallerStack(CallProfile profile)
        {
            Base = profile;
            Stack = new List<CallProfile> {profile};
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Add(CallProfile profile)
        {
            Stack.Add(profile);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Join(" -> ", Stack);
        }

        #endregion
    }
}