using System;
using JetBrains.Annotations;
using Zilon.Core.Dices;

namespace Zilon.Core.Combat
{
    [PublicAPI]
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
            var lastHitPoints = HitPoints;
            HitPoints -= value;

            var isDead = lastHitPoints > 0 && HitPoints <= 0;
            DoTakeDamage(value, isDead);
        }

        public void UseSkill(ICombatSquad targetSquad)
        {
            var rolledEnemyPersonIndex = _dice.Roll(0, targetSquad.Persons.Length - 1);
            var rolledPerson = targetSquad.Persons[rolledEnemyPersonIndex];

            rolledPerson.TakeDamage(2);
        }

        public event EventHandler<TakeDamageEventArgs> TakenDamage;

        private void DoTakeDamage(int value, bool isDead)
        {
            var eventArgs = new TakeDamageEventArgs(value, isDead);
            TakenDamage?.Invoke(this, eventArgs);
        }
    }
}