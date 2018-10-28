namespace Zilon.Core.Combat
{
    public class CombatPerson : ICombatPerson
    {
        public int HitPoints { get; private set; }

        public void TakeDamage(int value)
        {
            HitPoints -= value;
        }
    }
}
