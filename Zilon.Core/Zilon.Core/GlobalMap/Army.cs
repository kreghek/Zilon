using System;
using Zilon.Core.Spatial;

namespace Zilon.Core.GlobalMap
{
    public class Army : IMapEntity
    {
        public Army(ITerrainNode node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
        }

        public ITerrainNode Node { get; private set; }

        public void MoveTo(ITerrainNode targetNode)
        {
            Node = targetNode;
        }
    }
}
