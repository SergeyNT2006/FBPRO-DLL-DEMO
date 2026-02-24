using System;
using System.Collections.Generic;

namespace BENCHMARK
{
    public struct Point2D
    {
        public double X, Y;
        public Point2D(double x, double y) { X = x; Y = y; }
    }

    public struct Segment2D
    {
        public Point2D Start, End;
        public Segment2D(Point2D start, Point2D end) { Start = start; End = end; }
    }

    public struct TestCase
    {
        public Point2D Point;
        public Segment2D Segment;
        public TestCase(Point2D pt, Segment2D seg) { Point = pt; Segment = seg; }
    }

    public static class DataGenerator
    {
        private const double MinSegmentLength = 1e-6;

        public static List<TestCase> GenerateRandom(int count, int seed = 42)
        {
            var rng = new Random(seed);
            var result = new List<TestCase>(count);
            double range = 1000;

            for (int i = 0; i < count; i++)
            {
                TestCase tc;
                int attempts = 0;
                do
                {
                    var start = new Point2D(rng.NextDouble() * range, rng.NextDouble() * range);
                    var end = new Point2D(rng.NextDouble() * range, rng.NextDouble() * range);
                    var pt = new Point2D(rng.NextDouble() * range, rng.NextDouble() * range);
                    tc = new TestCase(pt, new Segment2D(start, end));
                    attempts++;
                } while (SegmentLength(tc.Segment) < MinSegmentLength && attempts < 100);

                if (SegmentLength(tc.Segment) >= MinSegmentLength)
                    result.Add(tc);
            }

            return result;
        }

        public static List<TestCase> GenerateKnown()
        {
            var result = new List<TestCase>();

            result.Add(new TestCase(
                new Point2D(50, 25),
                new Segment2D(new Point2D(0, 0), new Point2D(100, 50))));

            result.Add(new TestCase(
                new Point2D(100, 50),
                new Segment2D(new Point2D(0, 0), new Point2D(100, 50))));

            result.Add(new TestCase(
                new Point2D(30, 80),
                new Segment2D(new Point2D(0, 0), new Point2D(100, 50))));

            result.Add(new TestCase(
                new Point2D(50, 10),
                new Segment2D(new Point2D(0, 0), new Point2D(100, 0))));

            result.Add(new TestCase(
                new Point2D(5, 50),
                new Segment2D(new Point2D(0, 0), new Point2D(0, 100))));

            return result;
        }

        private static double SegmentLength(Segment2D seg)
        {
            double dx = seg.End.X - seg.Start.X;
            double dy = seg.End.Y - seg.Start.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
