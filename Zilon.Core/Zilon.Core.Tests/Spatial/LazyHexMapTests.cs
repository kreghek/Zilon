using System;

using FluentAssertions;

using NUnit.Framework;

using Zilon.Core.Spatial;

namespace Zilon.Core.Tests.Spatial
{
    [TestFixture]
    public class LazyHexMapTests
    {
        [Test]
        public void LazyHexMapTest()
        {
            // ARRANGE
            const int segmentSize = 100;



            // ACT
            Action act = () =>
            {
                new LazyHexMap(segmentSize);
            };


            // ASSERT
            act.Should().NotThrow();
        }
    }
}