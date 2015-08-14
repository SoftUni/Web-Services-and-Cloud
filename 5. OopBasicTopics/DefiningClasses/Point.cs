namespace DefiningClasses
{
    using System;

    public class Point
    {
        private int xCoord;
        private int yCoord;

        public Point(int x, int y)
        {
            this.xCoord = x;
            this.yCoord = y;
        }

        public double CalcDistance(Point p)
        {
            return Math.Sqrt(
              (p.xCoord - this.xCoord) * (p.xCoord - this.xCoord) +
              (p.yCoord - this.yCoord) * (p.yCoord - this.yCoord));
        }
    }
}
