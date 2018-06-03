using System;
using Xunit;

namespace Area.Tests
{
    public class TriangleTests
    {
        [Theory]
        [InlineData("a", -1, 1, 1)]
        [InlineData("a",  0, 1, 1)]
        [InlineData("a", double.NaN, 1, 1)]
        [InlineData("b", 1, -1, 1)]
        [InlineData("b", 1,  0, 1)]
        [InlineData("b", 1, double.NaN, 1)]
        [InlineData("c", 1, 1, -1)]
        [InlineData("c", 1, 1,  0)]
        [InlineData("c", 1, 1, double.NaN)]
        [InlineData(null, 1, 1, 2)]
        public void Triangle_ShouldThrowArgumentExceptionForInvalidSides(string paramName, double a, double b, double c)
        {
            Assert.Throws<ArgumentException>(paramName, () => new Triangle(a, b, c));
        }

        [Theory]
        [InlineData(1, 1, 1, 0.433)]
        [InlineData(2, 2, 2, 1.732)]
        [InlineData(3, 3, 4, 4.472)]
        public void Area_ShouldCalculate(double a, double b, double c, double expected)
        {
            var triangle = new Triangle(a, b, c);
            
            Assert.Equal(expected, triangle.Area(), precision: 3);
        }
    }
}