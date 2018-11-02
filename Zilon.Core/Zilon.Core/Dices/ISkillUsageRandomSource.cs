namespace Zilon.Core.Dices
{
    public interface ISkillUsageRandomSource
    {
        int RollEfficient(Roll roll);
        int RollPersonIndex(int personCount);
    }
}
