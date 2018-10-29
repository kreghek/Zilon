using System;

namespace Zilon.Core.Combat
{
    public sealed class TakeDamageEventArgs: EventArgs
    {
        public TakeDamageEventArgs(int value, bool dead)
        {
            Value = value;
            Dead = dead;
        }

        public int Value { get; }
        public bool Dead { get; }
    }
}