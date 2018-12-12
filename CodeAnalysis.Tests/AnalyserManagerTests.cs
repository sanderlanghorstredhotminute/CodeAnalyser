using System.IO;
using System.Linq;
using CodeAnalysis.Analysers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeAnalysis.Tests
{
    [TestClass]
    public class AnalyserManagerTests
    {
        public string TestBinaryLocation => Directory.GetCurrentDirectory() + "\\Binaries\\RecursionTest.exe";
        #region Methods

        /// <summary>
        /// Gets the recursive analyser test.
        /// </summary>
        [TestMethod]
        public void GetRecursiveAnalyserTest()
        {
            using (var analyser = AnalyserManager.GetRecursiveAnalyser(TestBinaryLocation))
            {
                Assert.IsNotNull(analyser);
            }
        }

        [TestMethod]
        public void Test_DependencyAnalyser_Result()
        {
            using (var analyser = new DependencyAnalyser(TestBinaryLocation))
            {
                var result = analyser.AnalyseAssembly();
                Assert.IsTrue(result.Any());
            }
        }

        #endregion
    }
}