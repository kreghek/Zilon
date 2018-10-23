using System.Collections.Generic;

namespace Zilon.Core.Spatial
{
    public class LazyHexMap : ITerrainGraph
    {
        private readonly int _segmentSize;
        private readonly IDictionary<SegmentKey, ITerrainNode[,]> _segmentDict;

        public LazyHexMap(int segmentSize)
        {
            _segmentSize = segmentSize;

            _segmentDict = new Dictionary<SegmentKey, ITerrainNode[,]>();

            CreateInitSegment();
        }

        private void CreateInitSegment()
        {
            var matrix = new ITerrainNode[_segmentSize, _segmentSize];

            for (var i = 0; i < _segmentSize; i++)
            {
                for (var j = 0; j < _segmentSize; j++)
                {
                    matrix[i, j] = new HexNode();
                }
            }

            var key = new SegmentKey(0, 0);
            _segmentDict[key] = matrix;
        }

        public IEnumerable<ITerrainNode> Nodes
        {
            get
            {
                foreach (var segmentKeyValue in _segmentDict)
                {
                    for (var i = 0; i < _segmentSize; i++)
                    {
                        for (var j = 0; j < _segmentSize; j++)
                        {
                            yield return segmentKeyValue.Value[i, j];
                        }
                    }
                }
            }
        }

        public IEnumerable<ITerrainNode> GetNeighborNodes(ITerrainNode node)
        {
            throw new System.NotImplementedException();
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
                if (!(obj is SegmentKey))
                {
                    return false;
                }

                var key = (SegmentKey)obj;
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