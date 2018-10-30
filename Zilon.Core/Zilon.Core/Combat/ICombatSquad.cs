using JetBrains.Annotations;

using Zilon.Core.Players;
using Zilon.Core.Spatial;

namespace Zilon.Core.Combat
{
    public interface ICombatSquad
    {
        [PublicAPI]
        ITerrainNode Node { get; }

        ICombatPerson[] Persons { get; }

        [PublicAPI]
        Player Player { get; }

        void MoveToNode(ITerrainNode targetNode);

        void UseSkill(ICombatSquad targetSquad);
    }
}