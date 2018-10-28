using System;

using Zilon.Core.Spatial;

namespace Zilon.Core.Combat
{
    public class CombatSquad : ICombatSquad
    {
        public CombatSquad(ITerrainNode node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
        }

        public ITerrainNode Node { get; private set; }

        public void MoveToNode(ITerrainNode targetNode)
        {
            Node = targetNode;
        }
    }
}
