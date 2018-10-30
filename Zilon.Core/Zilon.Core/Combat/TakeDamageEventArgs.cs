using System;

using JetBrains.Annotations;

namespace Zilon.Core.Combat
{
    [PublicAPI]
    public sealed class TakeDamageEventArgs: EventArgs
    {
        public TakeDamageEventArgs(int value, bool dead)
        {
            Value = value;
            Dead = dead;
        }

        [PublicAPI]
        public int Value { get; }

        [PublicAPI]
        public bool Dead { get; }
    }
}