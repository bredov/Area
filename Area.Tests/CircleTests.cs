using System;
using Xunit;

namespace Area.Tests
{
    public class CircleTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        [InlineData(-0.1)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.NaN)]
        public void Circle_ShouldThrowArgumentExceptionForInvalidRadius(double radius)
        {
            Assert.Throws<ArgumentException>("radius", () => new Circle(radius));
        }

        [Theory]
        [InlineData(1, Math.PI)]
        [InlineData(2, Math.PI * 4)]
        [InlineData(double.PositiveInfinity, double.PositiveInfinity)]
        public void Area_ShouldCalculate(double radius, double expected)
        {
            var circle = new Circle(radius);
            var actual = circle.Area();

            Assert.Equal(expected, actual);
        }
    }
}