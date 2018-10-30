using System;

using JetBrains.Annotations;

using Zilon.Core.Players;
using Zilon.Core.Spatial;

namespace Zilon.Core.Combat
{
    [PublicAPI]
    public class CombatSquad : ICombatSquad
    {
        public CombatSquad(ITerrainNode node, [NotNull] ICombatPerson[] persons, Player player)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Persons = persons ?? throw new ArgumentNullException(nameof(persons));
            Player = player;
        }

        public ITerrainNode Node { get; private set; }

        public void MoveToNode(ITerrainNode targetNode)
        {
            Node = targetNode;
        }

        public void UseSkill(ICombatSquad targetSquad)
        {
            foreach (var person in Persons)
            {
                if (person.HitPoints > 0)
                {
                    person.UseSkill(targetSquad);
                }
            }
        }

        public Player Player { get; }

        public ICombatPerson[] Persons { get; }
    }
}
