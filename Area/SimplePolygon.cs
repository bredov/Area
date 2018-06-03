namespace Area
{
    /// <summary>
    /// Defines a simple (non-intersecting) polygon shape that implements <see cref="IArea"/> interface.
    /// </summary>
    /// <remarks>Values of this struct can be constructed with <see cref="SimplePolygonBuilder"/> only and are
    /// immutable.</remarks>
    public struct SimplePolygon : IArea
    {
        private readonly Point[] _points;
        
        internal SimplePolygon(Point[] points)
        {
            _points = points;
        }

        /// <inheritdoc cref="IArea.Area"/>
        public double Area()
        {
            var sum = 0.0;

            for (var i = 0; i < _points.Length - 1; i++)
            {
                sum += _points[i].X * _points[i + 1].Y - _points[i + 1].X * _points[i].Y;
            }

            return sum / 2;
        }
    }
}