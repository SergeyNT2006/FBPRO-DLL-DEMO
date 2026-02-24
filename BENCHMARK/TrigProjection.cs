using System;

namespace BENCHMARK
{
    /// <summary>
    /// Reference implementation using atan2/sin/cos for comparison with FBPRO.
    /// </summary>
    public static class TrigProjection
    {
        public static double HeightToSegment(
            double ptX, double ptY,
            double lineStartX, double lineStartY,
            double lineEndX, double lineEndY)
        {
            double dx = lineEndX - lineStartX;
            double dy = lineEndY - lineStartY;
            double angle = Math.Atan2(dy, dx);
            double ux = Math.Cos(angle);
            double uy = Math.Sin(angle);

            double vx = ptX - lineStartX;
            double vy = ptY - lineStartY;
            double t = vx * ux + vy * uy;

            double projX = lineStartX + t * ux;
            double projY = lineStartY + t * uy;

            double hx = ptX - projX;
            double hy = ptY - projY;
            return Math.Sqrt(hx * hx + hy * hy);
        }

        public static void ProjectionPoint(
            double ptX, double ptY,
            double lineStartX, double lineStartY,
            double lineEndX, double lineEndY,
            out double projX, out double projY)
        {
            double dx = lineEndX - lineStartX;
            double dy = lineEndY - lineStartY;
            double angle = Math.Atan2(dy, dx);
            double ux = Math.Cos(angle);
            double uy = Math.Sin(angle);

            double vx = ptX - lineStartX;
            double vy = ptY - lineStartY;
            double t = vx * ux + vy * uy;

            projX = lineStartX + t * ux;
            projY = lineStartY + t * uy;
        }
    }
}
