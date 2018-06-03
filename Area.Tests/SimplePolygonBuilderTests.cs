using System;
using System.Collections.Generic;
using Xunit;

namespace Area.Tests
{
    public class SimplePolygonBuilderTests
    {
        [Theory]
        [MemberData(nameof(GetInvalidPolygonsForAdd))]
        public void Add_ThrowInvalidOperationException_ForIntersectingPolygons(params Point[] points)
        {
            var builder = new SimplePolygonBuilder();
            
            Assert.Throws<InvalidOperationException>(() =>
            {
                foreach (var p in points)
                {
                    builder.Add(p);
                }
            });
        }

        [Theory]
        [MemberData(nameof(GetValidPolygons))]
        public void Add_NoThrow_ForCorrectPolygons(params Point[] points)
        {
            var builder = new SimplePolygonBuilder();

            foreach (var p in points)
            {
                builder.Add(p);
            }
        }

        [Theory]
        [MemberData(nameof(GetInvalidPolygonsForClose))]
        public void Close_ThrowInvalidOperationException_ForIntersectingPolygons(params Point[] points)
        {
            var builder = new SimplePolygonBuilder();

            foreach (var p in points)
            {
                builder.Add(p);
            }

            Assert.Throws<InvalidOperationException>(() => builder.Close());
        }

        [Theory]
        [MemberData(nameof(GetValidPolygons))]
        public void Close_NoThrow_ForValidPolygons(params Point[] points)
        {
            var builder = new SimplePolygonBuilder();

            foreach (var p in points)
            {
                builder.Add(p);
            }

            builder.Close();
        }

        public static IEnumerable<object[]> GetValidPolygons()
        {
            yield return new object[] {new Point(0, 0), new Point(1, 0), new Point(2, 1)};
            yield return new object[]
                {new Point(0, 0), new Point(1, 0), new Point(1, 2), new Point(3, 3), new Point(1, 3)};
        }

        public static IEnumerable<object[]> GetInvalidPolygonsForClose()
        {
            yield return new object[] {new Point(0, 0)};
            yield return new object[] { };
            yield return new object[] {new Point(0, 0), new Point(3, 0), new Point(3, 2), new Point(4, 1)};
        }

        public static IEnumerable<object[]> GetInvalidPolygonsForAdd()
        {
            yield return new object[] {new Point(0, 0), new Point(3, 0), new Point(3, 2), new Point(2, -1)};
            yield return new object[] {new Point(0, 0), new Point(0, 0)};
            yield return new object[] {new Point(0, 0), new Point(3, 0), new Point(0, 0)};
            yield return new object[]
                {new Point(0, 0), new Point(3, 0), new Point(3, 2), new Point(4, 1), new Point(1, 1)};
        }
    }
}