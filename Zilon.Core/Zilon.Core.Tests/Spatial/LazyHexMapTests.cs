using System;
using System.Linq;
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


        /// <summary>
        /// Тест проверяет, что при запросе соседних узлов из угла сектора не происходит ошибок.
        /// </summary>
        [Test]
        public void GetNeighborNodesTest()
        {
            // ARRANGE
            const int segmentSize = 100;

            var map = new LazyHexMap(segmentSize);
            var zeroOffset = new OffsetCoords(0, 0);
            var zeroNode = map.Nodes.Single(x => x.Offset.Equals(zeroOffset));


            // ACT
            var nodes = map.GetNeighborNodes(zeroNode);



            // ASSERT
            nodes.Should().HaveCount(6);
        }

    }
}