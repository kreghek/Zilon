using System;
using System.Collections.Generic;

namespace Zilon.Core.Spatial
{
    public sealed class LazyHexMap : ITerrainGraph
    {
        private readonly IDictionary<SegmentKey, ITerrainNode[,]> _segmentDict;
        private readonly int _segmentSize;

        public LazyHexMap(int segmentSize)
        {
            if (segmentSize % 2 != 0)
            {
                throw new ArgumentException("Аргумент должен быть нечтётным", nameof(segmentSize));
            }

            _segmentSize = segmentSize;

            _segmentDict = new Dictionary<SegmentKey, ITerrainNode[,]>();

            CreateSegment(0, 0);
        }

        public LazyHexMap(int segmentSize, OffsetCoords[] startCoords)
        {
            if (segmentSize % 2 != 0)
            {
                throw new ArgumentException("Аргумент должен быть нечтётным", nameof(segmentSize));
            }

            _segmentSize = segmentSize;

            _segmentDict = new Dictionary<SegmentKey, ITerrainNode[,]>();

            foreach (var offsetCoord in startCoords)
            {
                CreateSegment(offsetCoord.X, offsetCoord.Y);
            }
        }

        public IEnumerable<ITerrainNode> Nodes
        {
            get
            {
                foreach (var segmentKeyValue in _segmentDict)
                    for (var i = 0; i < _segmentSize; i++)
                    for (var j = 0; j < _segmentSize; j++)
                        yield return segmentKeyValue.Value[i, j];
            }
        }

        public IEnumerable<ITerrainNode> GetNeighborNodes(ITerrainNode node)
        {
            var offsetCoords = node.Offset;
            var segmentX = offsetCoords.X / _segmentSize;
            if (offsetCoords.X < 0)
            {
                segmentX--;
            }

            var segmentY = offsetCoords.Y / _segmentSize;
            if (offsetCoords.Y < 0)
            {
                segmentY--;
            }

            var localOffsetX = NormalizeNeighborCoord(offsetCoords.X % _segmentSize);
            var localOffsetY = NormalizeNeighborCoord(offsetCoords.Y % _segmentSize);

            var segmentKey = new SegmentKey(segmentX, segmentY);
            var matrix = _segmentDict[segmentKey];

            var directions = HexHelper.GetOffsetClockwise();
            var currentCubeCoords = HexHelper.ConvertToCube(localOffsetX, localOffsetY);

            for (var i = 0; i < 6; i++)
            {
                var dir = directions[i];
                var neighborLocalCube = new CubeCoords(dir.X + currentCubeCoords.X,
                    dir.Y + currentCubeCoords.Y,
                    dir.Z + currentCubeCoords.Z);

                var neighborLocalOffset = HexHelper.ConvertToOffset(neighborLocalCube);

                var neighborSegmentX = segmentX;
                var neighborSegmentY = segmentY;

                if (neighborLocalOffset.X < 0)
                {
                    neighborSegmentX--;
                }
                else if (neighborLocalOffset.X >= _segmentSize)
                {
                    neighborSegmentX++;
                }

                if (neighborLocalOffset.Y < 0)
                {
                    neighborSegmentY--;
                }
                else if (neighborLocalOffset.Y >= _segmentSize)
                {
                    neighborSegmentY++;
                }

                if (neighborSegmentX == segmentX &&
                    neighborSegmentY == segmentY)
                {
                    yield return matrix[neighborLocalOffset.X, neighborLocalOffset.Y];
                }
                else
                {
                    var segmentMatrix = CreateSegment(neighborSegmentX, neighborSegmentY);
                    var neighborX = NormalizeNeighborCoord(neighborLocalOffset.X);
                    var neighborY = NormalizeNeighborCoord(neighborLocalOffset.Y);

                    yield return segmentMatrix[neighborX, neighborY];
                }
            }
        }

        private int NormalizeNeighborCoord(int neighborX)
        {
            if (neighborX < 0)
            {
                neighborX += _segmentSize;
            }
            else if (neighborX >= _segmentSize)
            {
                neighborX -= _segmentSize;
            }

            return neighborX;
        }

        private ITerrainNode[,] CreateSegment(int segmentX, int segmentY)
        {
            var matrix = new ITerrainNode[_segmentSize, _segmentSize];

            for (var i = 0; i < _segmentSize; i++)
            for (var j = 0; j < _segmentSize; j++)
                matrix[i, j] = new HexNode(i + segmentX * _segmentSize, j + segmentY * _segmentSize);

            var key = new SegmentKey(segmentX, segmentY);
            _segmentDict[key] = matrix;
            return matrix;
        }

        private struct SegmentKey
        {
            // ReSharper disable once MemberCanBePrivate.Local
            public readonly int X;

            // ReSharper disable once MemberCanBePrivate.Local
            public readonly int Y;

            public SegmentKey(int x, int y)
            {
                X = x;
                Y = y;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is SegmentKey)) return false;

                var key = (SegmentKey) obj;
                return X == key.X &&
                       Y == key.Y;
            }

            public override int GetHashCode()
            {
                var hashCode = 1502939027;
                hashCode = hashCode * -1521134295 + X.GetHashCode();
                hashCode = hashCode * -1521134295 + Y.GetHashCode();
                return hashCode;
            }
        }
    }
}