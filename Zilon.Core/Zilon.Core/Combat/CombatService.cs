using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using Zilon.Core.Common;
using Zilon.Core.Dices;

namespace Zilon.Core.Combat
{
    public class CombatService : ICombatService
    {
        private readonly ISkillUsageRandomSource _skillUsageRandomSource;

        public CombatService(IEntityManager<ICombatSquad> squadManager,
            ISkillUsageRandomSource skillUsageRandomSource)
        {
            _skillUsageRandomSource = skillUsageRandomSource;

            SquadManager = squadManager;
        }

        public IEntityManager<ICombatSquad> SquadManager { get; }

        [ItemNotNull]
        public IEnumerable<ICombatEvent> UseSkill(ICombatSquad squad, ICombatSquad target)
        {
            var eventList = new List<ICombatEvent>();
            foreach (var person in squad.Persons)
            {
                if (person.HitPoints <= 0)
                {
                    continue;
                }

                var personEvents = UseSkillByPerson(person, target);
                eventList.AddRange(personEvents);
            }

            foreach (var targetPerson in target.Persons.ToArray())
            {
                if (targetPerson.HitPoints <= 0)
                {
                    target.RemovePerson(targetPerson);
                }
            }

            if (!target.Persons.Any())
            {
                SquadManager.Remove(target);
            }

            return eventList;
        }

        private IEnumerable<ICombatEvent> UseSkillByPerson(ICombatPerson person, ICombatSquad targetSquad)
        {
            var eventList = new List<ICombatEvent>();
            ICombatPerson rolledPerson = SelectTargetPerson(targetSquad);

            IAttackResult attackResult;

            var toHitDice = new Roll(6, 1);
            var toHitRoll = _skillUsageRandomSource.RollToHit(toHitDice);
            const int toHitRollSuccess = 4;
            if (toHitRoll >= toHitRollSuccess)
            {
                var efficientRoll = new Roll(3, 1);
                var damage = _skillUsageRandomSource.RollEfficient(efficientRoll);

                rolledPerson.TakeDamage(damage);

                attackResult = new AttackSuccessResult(damage, rolledPerson.HitPoints <= 0);
            }
            else
            {
                attackResult = new AttackMissResult();
            }

            var attackEvent = new AttackCombatEvent(person, rolledPerson, attackResult);

            eventList.Add(attackEvent);

            return eventList;
        }

        private ICombatPerson SelectTargetPerson(ICombatSquad targetSquad)
        {
            var personCount = targetSquad.Persons.Length;
            var rolledEnemyPersonIndex = _skillUsageRandomSource.RollPersonIndex(personCount);
            var rolledPerson = targetSquad.Persons[rolledEnemyPersonIndex];
            return rolledPerson;
        }
    }
}
