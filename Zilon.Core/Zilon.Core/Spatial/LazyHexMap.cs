using System.Collections.Generic;

namespace Zilon.Core.Spatial
{
    public sealed class LazyHexMap : ITerrainGraph
    {
        private readonly IDictionary<SegmentKey, ITerrainNode[,]> _segmentDict;
        private readonly int _segmentSize;

        public LazyHexMap(int segmentSize)
        {
            _segmentSize = segmentSize;

            _segmentDict = new Dictionary<SegmentKey, ITerrainNode[,]>();

            CreateInitSegment();
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
            var segmentY = offsetCoords.Y / _segmentSize;
            var localOffsetX = offsetCoords.X % _segmentSize;
            var localOffsetY = offsetCoords.Y % _segmentSize;

            var segmentKey = new SegmentKey(segmentX, segmentY);
            var matrix = _segmentDict[segmentKey];

            var directions = HexHelper.GetOffsetClockwise();
            var currentCubeCoords = HexHelper.ConvertToCube(localOffsetX, localOffsetY);

            for (var i = 0; i < 6; i++)
            {
                var dir = directions[i];
                var neighborCube = new CubeCoords(dir.X + currentCubeCoords.X,
                    dir.Y + currentCubeCoords.Y,
                    dir.Z + currentCubeCoords.Z);

                var neighborOffset = HexHelper.ConvertToOffset(neighborCube);

                if (neighborOffset.X < 0)
                {
                    var segmentMatrix = CreateSegment(segmentX - 1, segmentY);
                    yield return segmentMatrix[_segmentSize - 1, localOffsetY];
                }
                else if (neighborOffset.X >= _segmentSize)
                {
                    var segmentMatrix = CreateSegment(segmentX + 1, segmentY);
                    yield return segmentMatrix[0, localOffsetY];
                }
                else if (neighborOffset.Y < 0)
                {
                    var segmentMatrix = CreateSegment(segmentX, segmentY - 1);
                    yield return segmentMatrix[localOffsetX, _segmentSize - 1];
                }
                else if (neighborOffset.Y >= _segmentSize)
                {
                    var segmentMatrix = CreateSegment(segmentX, segmentY + 1);
                    yield return segmentMatrix[localOffsetX, 0];
                }
                else
                {
                    yield return matrix[localOffsetX, localOffsetY];
                }
            }
        }

        private void CreateInitSegment()
        {
            CreateSegment(0, 0);
        }

        private ITerrainNode[,] CreateSegment(int segmentX, int segmentY)
        {
            var matrix = new ITerrainNode[_segmentSize, _segmentSize];

            for (var i = 0; i < _segmentSize; i++)
            for (var j = 0; j < _segmentSize; j++)
                matrix[i, j] = new HexNode(i, j);

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