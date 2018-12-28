using System;
using System.Collections.Generic;
using System.Linq;
using CodeAnalysis.Analysers.Recursive;
using CodeAnalysis.Model;

namespace CodeAnalysis.CLI
{
    class Program
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Handles the analysers.
        /// </summary>
        private static void HandleAnalysers()
        {
            var results = new List<IAnalyserResult>();
            using (var analyser = new AssemblyAnalyser(@"C:\Workspaces\Valk\VanDerValk Platform\Valk.Businesslayer\bin\Debug\Valk.Businesslayer.dll"))
            {
                analyser.AddAnalyser(new RecursiveAnalyser());
                results.AddRange(analyser.GetResults());
            }
            if (results.Any())
            {
                foreach (var analyserResult in results)
                {
                    Console.WriteLine($"{analyserResult.Result}: {analyserResult.Details}");
                }
            }
        }

        static void Main(string[] args)
        {
            HandleAnalysers();
            Console.WriteLine("Press the any key");
            Console.ReadKey();
        }

        #endregion

        #endregion
    }
}