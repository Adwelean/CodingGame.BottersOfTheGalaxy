namespace Core.Utils
{
    using System;

    public class Point
    {
        double x;
        double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double Distance(Point p) => Math.Sqrt(this.ComputeDistance(p));
        public double ComputeDistance(Point p) => ((this.x - p.x) * (this.x - p.x) + (this.y - p.y) * (this.y - p.y));
        public bool IsInRange(Point p, double range) => p != this && this.Distance(p) <= range;

        public double X { get => this.x; set => this.x = value; }
        public double Y { get => this.y; set => this.y = value; }
    }
}
