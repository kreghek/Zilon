using System.Collections.Generic;

using Zilon.Core.Dices;

namespace Zilon.Core.Combat
{
    public class CombatService : ICombatService
    {
        private readonly IDice _dice;

        public CombatService(IDice dice)
        {
            _dice = dice;
        }

        public IEnumerable<ICombatEvent> UseSkill(ICombatSquad squad, ICombatSquad target)
        {
            var eventList = new List<ICombatEvent>();
            foreach (var person in squad.Persons)
            {
                if (person.HitPoints > 0)
                {
                    continue;
                }

                var personEvents = UseSkillByPerson(person, target);
                eventList.AddRange(personEvents);
            }

            return eventList;
        }

        private IEnumerable<ICombatEvent> UseSkillByPerson(ICombatPerson person, ICombatSquad targetSquad)
        {
            var eventList = new List<ICombatEvent>();

            var rolledEnemyPersonIndex = _dice.Roll(0, targetSquad.Persons.Length - 1);
            var rolledPerson = targetSquad.Persons[rolledEnemyPersonIndex];

            var damage = _dice.Roll(2);

            rolledPerson.TakeDamage(damage);

            var attackEvent = new AttackCombatEvent(person, rolledPerson, rolledPerson.HitPoints <= 0, damage);

            eventList.Add(attackEvent);

            return eventList;
        }
    }
}
