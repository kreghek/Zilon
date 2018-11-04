using System;

namespace Zilon.Core.Dices
{
    public class SkillUsageRandomSource : ISkillUsageRandomSource
    {
        private readonly IDice _dice;

        public SkillUsageRandomSource(IDice dice)
        {
            _dice = dice ?? throw new ArgumentNullException(nameof(dice));
        }

        public int RollEfficient(Roll dice)
        {
            return RollInner(dice);
        }

        public int RollPersonIndex(int personCount)
        {
            return _dice.Roll(0, personCount - 1);
        }

        public int RollToHit(Roll dice)
        {
            return RollInner(dice);
        }

        private int RollInner(Roll rollData)
        {
            var sum = 0;
            for (var i = 0; i < rollData.Count; i++)
            {
                sum += _dice.Roll(rollData.Dice);
            }

            if (rollData.Modifiers != null)
            {
                sum += rollData.Modifiers.ResultBuff;
            }

            return sum;
        }
    }
}
