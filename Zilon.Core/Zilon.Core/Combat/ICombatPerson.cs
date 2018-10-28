namespace Zilon.Core.Combat
{
    public interface ICombatPerson
    {
        int HitPoints { get; }
        void TakeDamage(int value);
    }
}
