using System;
using System.Collections.Generic;

namespace Area
{
    /// <summary>
    /// Represents a <see cref="SimplePolygon"/> builder.
    /// </summary>
    /// <example>
    /// <code>
    /// var simplePolygonBuilder = new SimplePolygonBuilder();
    /// var simplePolygon = simplePolygonBuilder
    ///     .Add(x: 0, y: 0)
    ///     .Add(x: 3, y: 0)
    ///     .Add(x: 0, y: 4)
    ///     .Close();
    /// </code>
    /// </example>
    public sealed class SimplePolygonBuilder
    {
        private readonly List<Point> _points = new List<Point>();

        /// <summary>
        /// Adds a point to a polygon.
        /// </summary>
        /// <param name="point">Next point in a polygon.</param>
        /// <returns>Instance of this object (for method chaining)</returns>
        /// <exception cref="InvalidOperationException">Thrown if addition of a point
        /// will result in an invalid simple polygon.</exception>
        /// <seealso cref="SimplePolygonBuilder.Add(double, double)"/>
        public SimplePolygonBuilder Add(Point point)
        {
            if (!IsValidWithNewPoint(point))
            {
                throw new InvalidOperationException(
                    $"Can not add point ({point.X}, {point.Y}) - simple polygons can't have intersections");
            }

            _points.Add(point);

            return this;
        }

        /// <summary>
        /// Adds a point to a polygon.
        /// </summary>
        /// <param name="x">X coordinate of a point.</param>
        /// <param name="y">Y coordinate of a point.</param>
        /// <returns>Instance of this object (for method chaining)</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if addition of a point will result in an invalid simple polygon.
        /// </exception>
        /// <seealso cref="SimplePolygonBuilder.Add(Point)"/>
        public SimplePolygonBuilder Add(double x, double y)
        {
            return Add(new Point(x, y));
        }

        /// <summary>
        /// Closes and returns a constructed polygon.
        /// </summary>
        /// <returns>Instance of <see cref="SimplePolygon"/> constructed from provided points.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if closing a polygon will result in an invalid simple polygon.
        /// </exception>
        public SimplePolygon Close()
        {
            if (_points.Count < 3)
            {
                throw new InvalidOperationException("Can not form a valid shape.");
            }
            
            Add(_points[0]);

            return new SimplePolygon(_points.ToArray());
        }

        private bool IsValidWithNewPoint(Point point)
        {
            switch (_points.Count)
            {
                case 0:
                    return true;
                case 1:
                case 2:
                    return !point.Equals(_points[0]);
            }

            var line = (start: _points[_points.Count - 1], end: point);

            for (var i = 0; i < _points.Count - 1; i++)
            {
                var segment = (start: _points[i], end: _points[i + 1]);

                if (Intersects(line.start, line.end, segment.start, segment.end))
                {
                    if (line.start.Equals(segment.end) || line.end.Equals(segment.start))
                    {
                        continue;
                    }
                    return false;
                }
            }

            return true;
        }

        private static int Turn(Point p1, Point p2, Point p3)
        {
            var a = (p3.Y - p1.Y) * (p2.X - p1.X);
            var b = (p2.Y - p1.Y) * (p3.X - p1.X);

            return (a > b + double.Epsilon) ? 1 : (a + double.Epsilon < b) ? -1 : 0;
        }

        private static bool Intersects(Point p1, Point p2, Point p3, Point p4)
        {
            return (Turn(p1, p3, p4) != Turn(p2, p3, p4)) && (Turn(p1, p2, p3) != Turn(p1, p2, p4));
        }
    }
}