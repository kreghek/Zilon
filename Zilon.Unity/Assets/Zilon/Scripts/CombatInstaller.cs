using Zenject;

using Zilon.Core.ClientState;
using Zilon.Core.Combat;
using Zilon.Core.Commands;
using Zilon.Core.Common;
using Zilon.Core.Dices;
// ReSharper disable CheckNamespace

public class CombatInstaller : MonoInstaller<CombatInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICommandManager>().To<QueueCommandManager>().AsSingle();
        Container.Bind<ICombatStateManager>().To<CombatStateManager>().AsSingle();
        Container.Bind<ISquadMoveCommand>().To<SquadMoveCommand>().AsSingle();
        Container.Bind<ISquadAttackCommand>().To<SquadAttackCommand>().AsSingle();
        Container.Bind<IDice>().To<Dice>().AsSingle();
        Container.Bind<ICombatEventBus>().To<CombatEventBus>().AsSingle();
        Container.Bind<ICombatService>().To<CombatService>().AsSingle();
        Container.Bind<IEntityManager<ICombatSquad>>().To<EntityManager<ICombatSquad>>().AsSingle();
        Container.Bind<ISkillUsageRandomSource>().To<SkillUsageRandomSource>().AsSingle();
    }
}