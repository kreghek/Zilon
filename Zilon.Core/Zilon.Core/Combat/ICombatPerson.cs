using System;

using JetBrains.Annotations;

namespace Zilon.Core.Combat
{
    public interface ICombatPerson
    {
        int HitPoints { get; }

        void TakeDamage(int value);

        [PublicAPI]
        event EventHandler<TakeDamageEventArgs> TakenDamage;
    }
}