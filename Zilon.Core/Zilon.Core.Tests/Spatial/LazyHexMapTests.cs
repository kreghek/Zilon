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
            const int segmentSize = 4;

            var map = new LazyHexMap(segmentSize);
            var zeroOffset = new OffsetCoords(0, 0);
            var zeroNode = map.Nodes.Single(x => x.Offset.Equals(zeroOffset));


            // ACT
            var nodes = map.GetNeighborNodes(zeroNode);
            var nodeArray = nodes.ToArray();



            // ASSERT
            nodeArray[0].Offset.Should().Be(new OffsetCoords());
        }

        /// <summary>
        /// Тест проверяет, что при запросе соседних узлов из центра все узлы из текущего сегмента.
        /// </summary>
        [Test]
        public void GetNeighborNodes_NodeFromSegmentCenter_NodesFromSameSegment()
        {
            // ARRANGE
            const int segmentSize = 4;

            var map = new LazyHexMap(segmentSize);
            var zeroOffset = new OffsetCoords(1, 1);
            var zeroNode = map.Nodes.Single(x => x.Offset.Equals(zeroOffset));


            // ACT
            var nodes = map.GetNeighborNodes(zeroNode);
            var nodeArray = nodes.ToArray();



            // ASSERT
            nodeArray[0].Offset.Should().Be(new OffsetCoords(0, 1));

            nodeArray[1].Offset.Should().Be(new OffsetCoords(1, 2));
            nodeArray[2].Offset.Should().Be(new OffsetCoords(2, 2));

            nodeArray[3].Offset.Should().Be(new OffsetCoords(2, 1));

            nodeArray[4].Offset.Should().Be(new OffsetCoords(2, 0));
            nodeArray[5].Offset.Should().Be(new OffsetCoords(1, 0));
        }

    }
}