using System;
using System.Collections.Generic;
using FBPRO;

namespace BENCHMARK
{
    public class AccumulationResult
    {
        public int ChainLength;
        public double MaxHeightDiff;
        public double FinalPointDiff;
        public double FinalHeightDiff;
        public double FinalXFBPRO, FinalYFBPRO, FinalHFBPRO;
        public double FinalXTrig, FinalYTrig, FinalHTrig;
        public string Conclusion;
    }

    public static class AccumulationTest
    {
        private const int DefaultChainLength = 100_000;
        private const double Epsilon = 1e-10;

        public static AccumulationResult Run(int chainLength = DefaultChainLength)
        {
            var segments = CreateSegmentCycle();
            int segCount = segments.Count;

            double pxFBPRO = 100, pyFBPRO = 200;
            double pxTrig = 100, pyTrig = 200;

            double hFBPRO = 0, hTrig = 0;
            double maxHeightDiff = 0;

            for (int i = 0; i < chainLength; i++)
            {
                var seg = segments[i % segCount];

                hFBPRO = ProjectionHelper.HeightToSegment(pxFBPRO, pyFBPRO,
                    seg.Start.X, seg.Start.Y, seg.End.X, seg.End.Y);
                ProjectionHelper.ProjectionPoint(pxFBPRO, pyFBPRO,
                    seg.Start.X, seg.Start.Y, seg.End.X, seg.End.Y,
                    out double fxFBPRO, out double fyFBPRO);
                pxFBPRO = fxFBPRO;
                pyFBPRO = fyFBPRO;

                hTrig = TrigProjection.HeightToSegment(pxTrig, pyTrig,
                    seg.Start.X, seg.Start.Y, seg.End.X, seg.End.Y);
                TrigProjection.ProjectionPoint(pxTrig, pyTrig,
                    seg.Start.X, seg.Start.Y, seg.End.X, seg.End.Y,
                    out double fxTrig, out double fyTrig);
                pxTrig = fxTrig;
                pyTrig = fyTrig;

                double hDiff = Math.Abs(hFBPRO - hTrig);
                if (hDiff > maxHeightDiff) maxHeightDiff = hDiff;
            }

            double finalPointDiff = Math.Sqrt(
                (pxFBPRO - pxTrig) * (pxFBPRO - pxTrig) +
                (pyFBPRO - pyTrig) * (pyFBPRO - pyTrig));
            double finalHeightDiff = Math.Abs(hFBPRO - hTrig);

            string conclusion;
            if (maxHeightDiff <= Epsilon && finalPointDiff <= Epsilon)
                conclusion = "No error accumulation detected. FBPRO and Trig differ within rounding tolerance.";
            else if (maxHeightDiff < 1e-6 && finalPointDiff < 1e-6)
                conclusion = "Minor divergence. Both methods show comparable behavior over long chains.";
            else
                conclusion = "Divergence observed. Consider analysis for accuracy-critical scenarios.";

            return new AccumulationResult
            {
                ChainLength = chainLength,
                MaxHeightDiff = maxHeightDiff,
                FinalPointDiff = finalPointDiff,
                FinalHeightDiff = finalHeightDiff,
                FinalXFBPRO = pxFBPRO, FinalYFBPRO = pyFBPRO, FinalHFBPRO = hFBPRO,
                FinalXTrig = pxTrig, FinalYTrig = pyTrig, FinalHTrig = hTrig,
                Conclusion = conclusion
            };
        }

        private static List<Segment2D> CreateSegmentCycle()
        {
            var segs = new List<Segment2D>();
            var rng = new Random(42);
            for (int i = 0; i < 20; i++)
            {
                double range = 500;
                var start = new Point2D(rng.NextDouble() * range, rng.NextDouble() * range);
                var end = new Point2D(rng.NextDouble() * range, rng.NextDouble() * range);
                double lenSq = (end.X - start.X) * (end.X - start.X) + (end.Y - start.Y) * (end.Y - start.Y);
                if (lenSq > 1) segs.Add(new Segment2D(start, end));
            }
            if (segs.Count == 0)
                segs.Add(new Segment2D(new Point2D(0, 0), new Point2D(100, 50)));
            return segs;
        }
    }
}
