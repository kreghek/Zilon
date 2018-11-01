using System.Collections.Generic;

using Zilon.Core.Common;

namespace Zilon.Core.Combat
{
    public interface ICombatService
    {
        IEnumerable<ICombatEvent> UseSkill(ICombatSquad squad, ICombatSquad target);

        IEntityManager<ICombatSquad> SquadManager { get; }
    }
}