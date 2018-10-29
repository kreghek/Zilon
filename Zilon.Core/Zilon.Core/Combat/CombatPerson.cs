using System;

using Zilon.Core.Dices;

namespace Zilon.Core.Combat
{
    public class CombatPerson : ICombatPerson
    {
        private readonly IDice _dice;

        public CombatPerson(IDice dice)
        {
            _dice = dice ?? throw new ArgumentNullException(nameof(dice));

            HitPoints = 10;
        }

        public int HitPoints { get; private set; }

        public void TakeDamage(int value)
        {
            HitPoints -= value;
        }

        public void UseSkill(ICombatSquad targetSquad)
        {
            var rolledEnemyPersonIndex = _dice.Roll(0, targetSquad.Persons.Length);
            var rolledPerson = targetSquad.Persons[rolledEnemyPersonIndex];

            rolledPerson.TakeDamage(1);
        }
    }
}
