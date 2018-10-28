using Zenject;

using Zilon.Core.ClientState;
using Zilon.Core.Commands;

public class CombatInstaller : MonoInstaller<CombatInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICommandManager>().To<QueueCommandManager>().AsSingle();
        Container.Bind<ICombatStateManager>().To<CombatStateManager>().AsSingle();
        Container.Bind<ISquadMoveCommand>().To<SquadMoveCommand>().AsSingle();
    }
}