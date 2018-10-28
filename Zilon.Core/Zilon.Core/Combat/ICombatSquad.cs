﻿using Zilon.Core.Spatial;

namespace Zilon.Core.Combat
{
    public interface ICombatSquad
    {
        ITerrainNode Node { get; }
        void MoveToNode(ITerrainNode targetNode);
    }
}
