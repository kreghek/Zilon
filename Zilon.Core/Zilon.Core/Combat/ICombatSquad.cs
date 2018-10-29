using Zilon.Core.Players;
using Zilon.Core.Spatial;

namespace Zilon.Core.Combat
{
    public interface ICombatSquad
    {
        ITerrainNode Node { get; }
        ICombatPerson[] Persons { get; }
        Player Player { get; }
        void MoveToNode(ITerrainNode targetNode);
        void UseSkill(ICombatSquad targetSquad);
    }
}