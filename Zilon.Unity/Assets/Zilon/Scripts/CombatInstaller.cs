using Zenject;

using Zilon.Core.ClientState;
using Zilon.Core.Commands;
using Zilon.Core.Dices;

public class CombatInstaller : MonoInstaller<CombatInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICommandManager>().To<QueueCommandManager>().AsSingle();
        Container.Bind<ICombatStateManager>().To<CombatStateManager>().AsSingle();
        Container.Bind<ISquadMoveCommand>().To<SquadMoveCommand>().AsSingle();
        Container.Bind<IDice>().To<Dice>().AsSingle();
    }
}