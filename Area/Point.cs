using System;

namespace Area
{
    /// <summary>
    /// Represents a point in 2D space.
    /// </summary>
    /// <remarks>Values of this struct are immutable.</remarks>
    public struct Point : IEquatable<Point>
    {
        public readonly double X;
        public readonly double Y;

        /// <summary>
        /// Constructs a <c>Point</c> with given coordinates.
        /// </summary>
        /// <param name="x">Coordinate on X axis.</param>
        /// <param name="y">Coordinate on Y axis.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
        public bool Equals(Point other)
        {
            return (X == other.X) && (Y == other.Y);
        }
    }
}