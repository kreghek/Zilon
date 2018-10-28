using System;

using JetBrains.Annotations;

using Zilon.Core.Spatial;

namespace Zilon.Core.Combat
{
    public class CombatSquad : ICombatSquad
    {
        public CombatSquad(ITerrainNode node, [NotNull] ICombatPerson[] persons)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Persons = persons ?? throw new ArgumentNullException(nameof(persons));
        }

        public ITerrainNode Node { get; private set; }

        public void MoveToNode(ITerrainNode targetNode)
        {
            Node = targetNode;
        }

        public ICombatPerson[] Persons { get; }
    }
}
