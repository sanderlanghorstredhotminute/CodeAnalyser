using System;
using System.Collections.Generic;
using System.Linq;
using CodeAnalysis.Interfaces;

namespace CodeAnalysis.CLI
{
    class Program
    {
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
        static void Main(string[] args)
        {
            HandleAnalysers();
            Console.WriteLine("Press the any key");
            Console.ReadKey();
        }
    }
}
