﻿using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace Zilon.Core.Dices
{
    public class Roll
    {
        [ExcludeFromCodeCoverage]
        public Roll(int dice, int count) : this(dice, count, null)
        {

        }

        [JsonConstructor]
        [ExcludeFromCodeCoverage]
        public Roll(int dice, int count, RollModifiers modifiers)
        {
            Dice = dice;
            Count = count;
            Modifiers = modifiers;
        }

        public int Dice { get; }

        public int Count { get; }

        public RollModifiers Modifiers { get; }
    }
}
