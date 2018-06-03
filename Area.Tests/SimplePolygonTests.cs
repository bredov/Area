using Xunit;

namespace Area.Tests
{
    public class SimplePolygonTests
    {
        [Fact]
        public void Area_IsSameAsForTriangleWithGivenSides()
        {
            var triangle = new Triangle(3, 4, 5);
            var expected = triangle.Area();

            var polygon = new SimplePolygonBuilder()
                .Add(new Point(0, 0))
                .Add(new Point(4, 0))
                .Add(new Point(0, 3))
                .Close();
            var actual = polygon.Area();
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Area_IsSameForRectangleWithGivenSides()
        {
            var expected = 4 * 3; // Rectangle with sides 4 and 3

            var polygon = new SimplePolygonBuilder()
                .Add(new Point(0, 0))
                .Add(new Point(4, 0))
                .Add(new Point(4, 3))
                .Add(new Point(0, 3))
                .Close();
            var actual = polygon.Area();
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Area_CorrectForNonTrivialShape()
        {
            // Consider rectangle with sliced out right triangle inside.
            // Rectangle with sides 7 and 6, triangle with catheti
            // 3 and 4. Meaning, area is equal 7 * 6 - 3 * 4 / 2
            var expected = 7 * 6 - new Triangle(3, 4, 5).Area();

            var polygon = new SimplePolygonBuilder()
                .Add(x: 0, y: 0)
                .Add(x: 3, y: 0)
                .Add(x: 7, y: 3)
                .Add(x: 7, y: 6)
                .Add(x: 0, y: 6)
                .Close();
            var actual = polygon.Area();
            
            Assert.Equal(expected, actual);
        }
    }
}