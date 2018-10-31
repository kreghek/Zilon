using System;

using JetBrains.Annotations;

using Zilon.Core.Dices;
using Zilon.Core.NameGeneration;

namespace Zilon.Core.Combat
{
    [PublicAPI]
    public class CombatPerson : ICombatPerson
    {
        private readonly IDice _dice;
        private readonly string _name;

        public CombatPerson(IDice dice, IPersonNameGenerator nameGenerator)
        {
            _dice = dice ?? throw new ArgumentNullException(nameof(dice));

            _name = nameGenerator.CreateName();

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

        public event EventHandler<TakeDamageEventArgs> TakenDamage;

        private void DoTakeDamage(int value, bool isDead)
        {
            var eventArgs = new TakeDamageEventArgs(value, isDead);
            TakenDamage?.Invoke(this, eventArgs);
        }

        public override string ToString()
        {
            return $"{_name}";
        }
    }
}