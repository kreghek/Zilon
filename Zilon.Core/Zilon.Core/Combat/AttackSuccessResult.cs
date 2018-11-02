namespace Zilon.Core.Combat
{
    public class AttackSuccessResult: IAttackResult
    {
        public AttackSuccessResult(int damage, bool targetIsDead)
        {
            Damage = damage;
            TargetIsDead = targetIsDead;
        }

        public int Damage { get; }

        public bool TargetIsDead { get; }
    }
}
