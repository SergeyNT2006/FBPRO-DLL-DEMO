using System;
using System.Collections.Generic;
using System.IO;

namespace BENCHMARK
{
    public static class ReportGenerator
    {
        public static string GenerateHtml(
            List<AccuracyResult> accuracyResults,
            PerfResult perfResult,
            AccumulationResult accumResult,
            DateTime runTime)
        {
            bool allAccuracyPassed = true;
            foreach (var r in accuracyResults)
                if (!r.Passed) { allAccuracyPassed = false; break; }

            var html = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""utf-8"">
  <title>FBPRO Benchmark Report</title>
  <style>
    body {{ font-family: 'Segoe UI', sans-serif; margin: 2em; max-width: 900px; }}
    h1 {{ color: #1a5276; }}
    h2 {{ color: #2874a6; margin-top: 1.5em; }}
    .slogan {{ font-size: 1.2em; color: #2e86ab; margin: 1em 0; }}
    table {{ border-collapse: collapse; width: 100%; margin: 1em 0; }}
    th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
    th {{ background: #3498db; color: white; }}
    tr:nth-child(even) {{ background: #f9f9f9; }}
    .pass {{ color: #27ae60; font-weight: bold; }}
    .fail {{ color: #e74c3c; font-weight: bold; }}
    .speedup {{ font-size: 1.5em; color: #27ae60; font-weight: bold; }}
    .meta {{ color: #7f8c8d; font-size: 0.9em; margin-top: 2em; }}
  </style>
</head>
<body>
  <h1>FBPRO Benchmark Report</h1>
  <p class=""slogan"">FBPRO - Fast Projection Without Trigonometry!</p>
  <p class=""meta"">Generated: {runTime:yyyy-MM-dd HH:mm:ss}</p>

  <h2>1. Accuracy Tests</h2>
  <p>Overall: <span class=""{(allAccuracyPassed ? "pass" : "fail")}"">{(allAccuracyPassed ? "PASS" : "FAIL")}</span></p>
  <table>
    <tr><th>Test</th><th>Result</th><th>Details</th></tr>
";

            foreach (var r in accuracyResults)
            {
                string status = r.Passed ? "PASS" : "FAIL";
                string cls = r.Passed ? "pass" : "fail";
                html += $@"    <tr>
      <td>{Escape(r.TestName)}</td>
      <td class=""{cls}"">{status}</td>
      <td>{Escape(r.Message)} (max diff: {r.MaxDiff:E2})</td>
    </tr>
";
            }

            html += $@"  </table>

  <h2>2. Accumulation Test (sequential chaining)</h2>
  <p>Chain of {accumResult.ChainLength:N0} steps: projection point from each step is used as input for the next.</p>
  <table>
    <tr><th>Metric</th><th>Value</th></tr>
    <tr><td>Max height diff (FBPRO vs Trig)</td><td>{accumResult.MaxHeightDiff:E2}</td></tr>
    <tr><td>Final point distance</td><td>{accumResult.FinalPointDiff:E2}</td></tr>
    <tr><td>Final height diff</td><td>{accumResult.FinalHeightDiff:E2}</td></tr>
    <tr><td>Final FBPRO (x, y, h)</td><td>({accumResult.FinalXFBPRO:F6}, {accumResult.FinalYFBPRO:F6}, {accumResult.FinalHFBPRO:F6})</td></tr>
    <tr><td>Final Trig (x, y, h)</td><td>({accumResult.FinalXTrig:F6}, {accumResult.FinalYTrig:F6}, {accumResult.FinalHTrig:F6})</td></tr>
  </table>
  <p><strong>Conclusion:</strong> {Escape(accumResult.Conclusion)}</p>

  <h2>3. Performance Benchmark</h2>
  <table>
    <tr><th>Method</th><th>Time (ms)</th><th>Per call (ns)</th></tr>
    <tr><td>Trig (atan2/sin/cos)</td><td>{perfResult.MsTrig:N0}</td><td>{perfResult.MsPerCallTrig:F2}</td></tr>
    <tr><td>FBPRO (dot product)</td><td>{perfResult.MsFBPRO:N0}</td><td>{perfResult.MsPerCallFBPRO:F2}</td></tr>
  </table>
  <p>Iterations: {perfResult.BenchIterations:N0} | Warmup: {perfResult.WarmupIterations:N0}</p>
  <p class=""speedup"">Speedup: {perfResult.Speedup:F2}x (FBPRO is faster)</p>
  <p>Sample height — Trig: {perfResult.SampleHeightTrig:F6}, FBPRO: {perfResult.SampleHeightFBPRO:F6}</p>

  <h2>4. Summary</h2>
  <ul>
    <li>Accuracy: {(allAccuracyPassed ? "All tests passed" : "Some tests failed")}</li>
    <li>Accumulation: {Escape(accumResult.Conclusion)}</li>
    <li>Performance: FBPRO is ~{perfResult.Speedup:F1}x faster than trig-based projection</li>
  </ul>

  <p class=""meta"">FBPRO - Fast Projection Without Trigonometry! | Benchmark suite</p>
</body>
</html>";

            return html;
        }

        private static string Escape(string s)
        {
            return s?.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;") ?? "";
        }

        public static void WriteHtml(string path, string html)
        {
            File.WriteAllText(path, html);
        }
    }
}
