using System.IO;
using System.Linq;
using CodeAnalysis.Analysers.Dependency;
using CodeAnalysis.Analysers.Recursive;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeAnalysis.Tests
{
    [TestClass]
    public class AnalyserManagerTests
    {
        #region Properties

        public string TestBinaryLocation => Directory.GetCurrentDirectory() + "\\Binaries\\RecursionTest.exe";

        #endregion

        #region Methods

        [TestMethod]
        public void IntegrationTest()
        {
            using (var analyser = new AssemblyAnalyser(TestBinaryLocation))
            {
                analyser.AddAnalyser(new RecursiveAnalyser());
                analyser.AddAnalyser(new DependencyAnalyser());
                var results = analyser.GetResults();
                Assert.IsNotNull(results);
            }
        }

        #endregion
    }
}