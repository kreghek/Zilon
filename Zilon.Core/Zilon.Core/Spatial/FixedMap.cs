using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Zilon.Core.Spatial
{
    [PublicAPI]
    public class FixedMap : ITerrainGraph
    {
        private readonly ITerrainNode[,] _nodes;
        private readonly int _mapSize;

        public FixedMap(int mapSize)
        {
            _mapSize = mapSize;

            _nodes = new ITerrainNode[mapSize, mapSize];
            for (var i = 0; i < _mapSize; i++)
            {
                for (var j = 0; j < _mapSize; j++)
                {
                    _nodes[i, j] = new HexNode(i, j);
                }
            }
        }

        public IEnumerable<ITerrainNode> Nodes
        {
            get
            {
                for (var i = 0; i < _mapSize; i++)
                {
                    for (var j = 0; j < _mapSize; j++)
                    {
                        yield return _nodes[i, j];
                    }
                }
            }
        }

        public IEnumerable<ITerrainNode> GetNeighborNodes(ITerrainNode node)
        {
            throw new NotImplementedException();
        }
    }
}
