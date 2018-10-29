using Zilon.Core.Players;
using Zilon.Core.Spatial;

namespace Zilon.Core.Combat
{
    public interface ICombatSquad
    {
        ITerrainNode Node { get; }
        void MoveToNode(ITerrainNode targetNode);
        ICombatPerson[] Persons { get; }
        void UseSkill(ICombatSquad targetSquad);
        Player Player { get; }
    }
}
