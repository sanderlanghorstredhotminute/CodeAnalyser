using System;
using System.Collections.Generic;
using System.Linq;
using CodeAnalysis;
using CodeAnalysis.Interfaces;

namespace CodeAnalyser.CLI
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
            using (var analyser = AnalyserManager.GetRecursiveAnalyser(@"C:\Workspaces\Valk\VanDerValk Platform\Valk.Businesslayer\bin\Debug\Valk.Businesslayer.dll"))
            {
                results.AddRange(analyser.AnalyseAssembly());
            }
            if (results.Any())
            {
                foreach (var analyserResult in results)
                {
                    Console.WriteLine($"{analyserResult.Result}: {analyserResult.Details}");
                }
            }
        }

        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            //fetch arguments
            //analyse
            HandleAnalysers();
            Console.WriteLine("Done... press any key to exit.");
            Console.ReadKey();
        }

        #endregion

        #endregion
    }
}