using System;
using System.Xml.XPath;

namespace Area
{
    /// <summary>
    /// Defines triangle shape that implements <see cref="IArea"/> interface.
    /// </summary>
    /// <remarks>Values of this type are immutable.</remarks>
    public struct Triangle : IArea
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;

        /// <summary>
        /// Constructs a triangle with given sides.
        /// </summary>
        /// <param name="a">Side of a triangle.</param>
        /// <param name="b">Side of a triangle.</param>
        /// <param name="c">Side of a triangle.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if any of the provided sides invalid (less or equal to zero or NaN)
        /// or if it is impossible to create a triangle with given sides.
        /// </exception>
        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || double.IsNaN(a))
            {
                throw new ArgumentException("Side of a triangle must be a valid positive number.", nameof(a));
            }

            if (b <= 0 || double.IsNaN(b))
            {
                throw new ArgumentException("Side of a triangle must be a valid positive number", nameof(b));
            }

            if (c <= 0 || double.IsNaN(c))
            {
                throw new ArgumentException("Side of a triangle must be a valid positive number", nameof(c));
            }

            if ((a + b <= c) || (a + c <= b) || (b + c <= a))
            {
                throw new ArgumentException("Impossible to create a triangle with given sides");
            }

            // To simplify AreaRight method and IsRight property we order sides in a way that
            // hypotenuse (the largest side) always end up in the variable _c.
            var sides = new[] {a, b, c};
            Array.Sort(sides);

            _a = sides[0];
            _b = sides[1];
            _c = sides[2];
        }

        /// <value>True if this triangle is a right triangle.</value>
        public bool IsRight => _a * _a + _b * _b == _c * _c;
        
        /// <inheritdoc cref="IArea.Area"/>
        public double Area()
        {
            return IsRight ? AreaRight() : AreaHeron();
        }

        private double AreaRight()
        {
            return _a * _b / 2;
        }

        private double AreaHeron()
        {
            var s = (_a + _b + _c) / 2;

            return Math.Sqrt(s * (s - _a) * (s - _b) * (s - _c));
        }
    }
}