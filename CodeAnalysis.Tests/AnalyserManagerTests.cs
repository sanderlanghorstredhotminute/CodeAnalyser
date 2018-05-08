using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeAnalysis.Tests
{
    [TestClass]
    public class AnalyserManagerTests
    {
        #region Methods

        /// <summary>
        /// Gets the recursive analyser test.
        /// </summary>
        [TestMethod]
        public void GetRecursiveAnalyserTest()
        {
            using (var analyser = AnalyserManager.GetRecursiveAnalyser(Directory.GetCurrentDirectory() + "\\Binaries\\RecursionTest.exe"))
            {
                Assert.IsNotNull(analyser);
            }
        }

        #endregion
    }
}