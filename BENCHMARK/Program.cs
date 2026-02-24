using System;
using System.Collections.Generic;

namespace BENCHMARK
{
    class Program
    {
        private const int DefaultIterations = 50_000_000;

        static void Main(string[] args)
        {
            int benchIterations = ParseIterations(args);

            Console.WriteLine("=== FBPRO Benchmark Suite ===\n");
            Console.WriteLine("FBPRO - Fast Projection Without Trigonometry!\n");

            // 1. Accuracy tests
            Console.WriteLine("--- Accuracy Tests ---");
            var accuracyResults = AccuracyTest.RunAll();
            bool allPassed = true;
            foreach (var r in accuracyResults)
            {
                string status = r.Passed ? "PASS" : "FAIL";
                if (!r.Passed) allPassed = false;
                Console.WriteLine($"  {r.TestName}: {status} — {r.Message}");
            }
            Console.WriteLine($"\nOverall: {(allPassed ? "PASS" : "FAIL")}\n");

            // 2. Accumulation test
            Console.WriteLine("--- Accumulation Test ---");
            Console.WriteLine("  Chain: 100,000 steps (output → input)...");
            var accumResult = AccumulationTest.Run();
            Console.WriteLine($"  Max height diff: {accumResult.MaxHeightDiff:E2}");
            Console.WriteLine($"  Final point diff: {accumResult.FinalPointDiff:E2}");
            Console.WriteLine($"  Conclusion: {accumResult.Conclusion}\n");

            // 3. Performance benchmark
            Console.WriteLine("--- Performance Benchmark ---");
            Console.WriteLine($"  Warmup: 500,000 | Bench: {benchIterations:N0} iterations...");
            var perfResult = PerfBenchmark.Run(bench: benchIterations);
            Console.WriteLine($"  Trig (atan2/sin/cos): {perfResult.MsTrig:N0} ms");
            Console.WriteLine($"  FBPRO (dot product):  {perfResult.MsFBPRO:N0} ms");
            Console.WriteLine($"  Speedup: {perfResult.Speedup:F2}x (FBPRO is faster)");
            Console.WriteLine($"  Sample h: trig={perfResult.SampleHeightTrig:F6}, FBPRO={perfResult.SampleHeightFBPRO:F6}\n");

            // 4. HTML report
            var runTime = DateTime.Now;
            string html = ReportGenerator.GenerateHtml(accuracyResults, perfResult, accumResult, runTime);
            string reportPath = "FBPRO_Benchmark_Report.html";
            ReportGenerator.WriteHtml(reportPath, html);
            Console.WriteLine($"--- Report ---");
            Console.WriteLine($"  Saved: {reportPath}\n");

            Console.WriteLine("=== Benchmark complete ===");
        }

        static int ParseIterations(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if ((args[i] == "-i" || args[i] == "--iterations") && i + 1 < args.Length)
                {
                    if (int.TryParse(args[i + 1], out int val) && val > 0)
                        return val;
                }
            }
            return DefaultIterations;
        }
    }
}
