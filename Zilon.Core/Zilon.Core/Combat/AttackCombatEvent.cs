using System;
using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;

namespace Zilon.Core.Combat
{
    [PublicAPI]
    public class AttackCombatEvent : ICombatEvent
    {
        [ExcludeFromCodeCoverage]
        public AttackCombatEvent(ICombatPerson sender,
            ICombatPerson target,
            bool targetIsDead,
            int damage)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            TargetIsDead = targetIsDead;
            Damage = damage;
        }

        public ICombatPerson Sender { get; }

        public ICombatPerson Target { get; }

        public bool TargetIsDead { get; }

        public int Damage { get; }
    }
}
