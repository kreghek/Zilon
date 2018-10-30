﻿using System;
using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;

namespace Zilon.Core.Dices
{
    [PublicAPI]
    public class Dice : IDice
    {
        private readonly Random _random;

        [ExcludeFromCodeCoverage]
        public Dice()
        {
            _random = new Random();
        }

        [ExcludeFromCodeCoverage]
        public Dice(int seed)
        {
            _random = new Random(seed);
        }

        [ExcludeFromCodeCoverage]
        public int Roll(int n)
        {
            var rollResult = _random.Next(1, n + 1);
            return rollResult;
        }
    }
}
