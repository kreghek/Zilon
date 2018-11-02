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
            IAttackResult result)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Result = result ?? throw new ArgumentNullException(nameof(result));
        }

        public ICombatPerson Sender { get; }

        public ICombatPerson Target { get; }

        public IAttackResult Result { get; }
    }
}
