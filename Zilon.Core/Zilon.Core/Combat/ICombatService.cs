using System.Collections.Generic;

namespace Zilon.Core.Combat
{
    public interface ICombatService
    {
        IEnumerable<ICombatEvent> UseSkill(ICombatSquad squad, ICombatSquad target);
    }
}