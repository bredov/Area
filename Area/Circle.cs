using System;

namespace Area
{
    /// <summary>
    /// Defines circle shape that implements <see cref="IArea"/> interface.
    /// </summary>
    /// <remarks>Values of this struct are immutable.</remarks>
    public struct Circle : IArea
    {
        private readonly double _radius;

        /// <summary>
        /// Constructs a circle with given radius.
        /// </summary>
        /// <param name="radius">Radius of a circle.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="radius"/> is less or equal to zero or
        /// if radius is NaN</exception>
        /// <seealso cref="Double.NaN"/>
        public Circle(double radius)
        {
            if (radius <= 0 || double.IsNaN(radius))
            {
                throw new ArgumentException("Radius must be a valid positive number.", nameof(radius));
            }

            _radius = radius;
        }
        
        /// <inheritdoc cref="IArea.Area"/>
        public double Area()
        {
            return _radius * _radius * Math.PI;
        }
    }
}