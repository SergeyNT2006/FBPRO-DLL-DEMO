using System;
using System.Collections.Generic;
using FBPRO;

namespace BENCHMARK
{
    public class AccuracyResult
    {
        public bool Passed;
        public string TestName;
        public int TotalCases;
        public int PassedCases;
        public double MaxDiff;
        public string Message;
    }

    public static class AccuracyTest
    {
        private const double Epsilon = 1e-10;

        public static List<AccuracyResult> RunAll()
        {
            var results = new List<AccuracyResult>();

            results.Add(RunFBPROvsTrig());
            results.Add(RunKnownCases());
            results.Add(RunRandomConsistency());

            return results;
        }

        private static AccuracyResult RunFBPROvsTrig()
        {
            var data = DataGenerator.GenerateRandom(10000);
            double maxDiff = 0;
            int passed = 0;

            foreach (var tc in data)
            {
                double fbpro = ProjectionHelper.HeightToSegment(
                    tc.Point.X, tc.Point.Y,
                    tc.Segment.Start.X, tc.Segment.Start.Y,
                    tc.Segment.End.X, tc.Segment.End.Y);

                double trig = TrigProjection.HeightToSegment(
                    tc.Point.X, tc.Point.Y,
                    tc.Segment.Start.X, tc.Segment.Start.Y,
                    tc.Segment.End.X, tc.Segment.End.Y);

                double diff = Math.Abs(fbpro - trig);
                if (diff <= Epsilon) passed++;
                if (diff > maxDiff) maxDiff = diff;
            }

            return new AccuracyResult
            {
                Passed = maxDiff <= Epsilon,
                TestName = "FBPRO vs Trig (10k random)",
                TotalCases = data.Count,
                PassedCases = passed,
                MaxDiff = maxDiff,
                Message = maxDiff <= Epsilon
                    ? $"OK: max diff = {maxDiff:E2}"
                    : $"FAIL: max diff = {maxDiff:E2} > {Epsilon:E0}"
            };
        }

        private static AccuracyResult RunKnownCases()
        {
            var data = DataGenerator.GenerateKnown();
            var expected = new[] { 0.0, 0.0, 58.137767, 10.0, 5.0 };
            int passed = 0;
            double maxDiff = 0;

            for (int i = 0; i < data.Count; i++)
            {
                double h = ProjectionHelper.HeightToSegment(
                    data[i].Point.X, data[i].Point.Y,
                    data[i].Segment.Start.X, data[i].Segment.Start.Y,
                    data[i].Segment.End.X, data[i].Segment.End.Y);

                double exp = i < expected.Length ? expected[i] : 0;
                double diff = Math.Abs(h - exp);
                if (diff <= Epsilon || (exp > 0 && diff / exp < 1e-6)) passed++;
                if (diff > maxDiff) maxDiff = diff;
            }

            return new AccuracyResult
            {
                Passed = passed == data.Count,
                TestName = "Known geometric cases",
                TotalCases = data.Count,
                PassedCases = passed,
                MaxDiff = maxDiff,
                Message = passed == data.Count
                    ? $"OK: all {data.Count} cases match"
                    : $"FAIL: {passed}/{data.Count} passed"
            };
        }

        private static AccuracyResult RunRandomConsistency()
        {
            var data = DataGenerator.GenerateRandom(5000, 123);
            double maxDiff = 0;
            int passed = 0;

            foreach (var tc in data)
            {
                double fbpro = ProjectionHelper.HeightToSegment(
                    tc.Point.X, tc.Point.Y,
                    tc.Segment.Start.X, tc.Segment.Start.Y,
                    tc.Segment.End.X, tc.Segment.End.Y);

                double trig = TrigProjection.HeightToSegment(
                    tc.Point.X, tc.Point.Y,
                    tc.Segment.Start.X, tc.Segment.Start.Y,
                    tc.Segment.End.X, tc.Segment.End.Y);

                double diff = Math.Abs(fbpro - trig);
                if (diff <= Epsilon) passed++;
                if (diff > maxDiff) maxDiff = diff;
            }

            return new AccuracyResult
            {
                Passed = maxDiff <= Epsilon,
                TestName = "Random consistency (5k, seed=123)",
                TotalCases = data.Count,
                PassedCases = passed,
                MaxDiff = maxDiff,
                Message = maxDiff <= Epsilon
                    ? $"OK: FBPRO and Trig match, max diff = {maxDiff:E2}"
                    : $"FAIL: max diff = {maxDiff:E2}"
            };
        }
    }
}
