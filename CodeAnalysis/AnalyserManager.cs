using System;
using System.IO;
using System.Linq;
using CodeAnalysis.Analysers;
using CodeAnalysis.Interfaces;

namespace CodeAnalysis
{
    public static class AnalyserManager
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Gets the recursive analyser.
        /// </summary>
        /// <param name="modulepath">The modulepath.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">modulepath</exception>
        /// <exception cref="System.InvalidOperationException">file does not exist</exception>
        public static IAnalyser GetRecursiveAnalyser(string modulepath)
        {
            if (string.IsNullOrWhiteSpace(modulepath))
            {
                throw new ArgumentNullException(nameof(modulepath));
            }
            if (!File.Exists(modulepath))
            {
                throw new InvalidOperationException("file does not exist");
            }
            return new RecursiveAnalyser(modulepath);
        }

        #endregion

        #endregion
    }
}