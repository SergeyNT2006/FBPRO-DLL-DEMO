using System;
using System.Collections.Generic;
using System.Diagnostics;
using FBPRO;

namespace BENCHMARK
{
    public class PerfResult
    {
        public int WarmupIterations;
        public int BenchIterations;
        public long MsTrig;
        public long MsFBPRO;
        public double Speedup;
        public double MsPerCallTrig;
        public double MsPerCallFBPRO;
        public double SampleHeightTrig;
        public double SampleHeightFBPRO;
    }

    public static class PerfBenchmark
    {
        private const int WarmupDefault = 500_000;
        private const int BenchDefault = 50_000_000;

        public static PerfResult Run(int warmup = WarmupDefault, int bench = BenchDefault, int dataSize = 1000)
        {
            var data = DataGenerator.GenerateRandom(dataSize, 777);

            double hTrig = 0, hFBPRO = 0;

            for (int i = 0; i < warmup; i++)
            {
                var tc = data[i % data.Count];
                hTrig = TrigProjection.HeightToSegment(
                    tc.Point.X, tc.Point.Y,
                    tc.Segment.Start.X, tc.Segment.Start.Y,
                    tc.Segment.End.X, tc.Segment.End.Y);
                hFBPRO = ProjectionHelper.HeightToSegment(
                    tc.Point.X, tc.Point.Y,
                    tc.Segment.Start.X, tc.Segment.Start.Y,
                    tc.Segment.End.X, tc.Segment.End.Y);
            }

            var swTrig = Stopwatch.StartNew();
            for (int i = 0; i < bench; i++)
            {
                var tc = data[i % data.Count];
                hTrig = TrigProjection.HeightToSegment(
                    tc.Point.X, tc.Point.Y,
                    tc.Segment.Start.X, tc.Segment.Start.Y,
                    tc.Segment.End.X, tc.Segment.End.Y);
            }
            swTrig.Stop();

            var swFBPRO = Stopwatch.StartNew();
            for (int i = 0; i < bench; i++)
            {
                var tc = data[i % data.Count];
                hFBPRO = ProjectionHelper.HeightToSegment(
                    tc.Point.X, tc.Point.Y,
                    tc.Segment.Start.X, tc.Segment.Start.Y,
                    tc.Segment.End.X, tc.Segment.End.Y);
            }
            swFBPRO.Stop();

            long msTrig = swTrig.ElapsedMilliseconds;
            long msFBPRO = swFBPRO.ElapsedMilliseconds;
            double speedup = msFBPRO > 0 ? (double)msTrig / msFBPRO : 0;

            return new PerfResult
            {
                WarmupIterations = warmup,
                BenchIterations = bench,
                MsTrig = msTrig,
                MsFBPRO = msFBPRO,
                Speedup = speedup,
                MsPerCallTrig = bench > 0 ? (double)msTrig / bench * 1e6 : 0,
                MsPerCallFBPRO = bench > 0 ? (double)msFBPRO / bench * 1e6 : 0,
                SampleHeightTrig = hTrig,
                SampleHeightFBPRO = hFBPRO
            };
        }
    }
}
