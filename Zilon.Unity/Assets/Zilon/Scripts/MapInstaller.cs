using Zenject;

using Zilon.Core.ClientState;
using Zilon.Core.Commands;

public class MapInstaller : MonoInstaller<MapInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICommandManager>().To<QueueCommandManager>().AsSingle();
        Container.Bind<IGlobalState>().To<GlobalStateManager>().AsSingle();
        Container.Bind<IArmyModeCommand>().To<ArmyMoveCommand>().AsSingle();
    }
}