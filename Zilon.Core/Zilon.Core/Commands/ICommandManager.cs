using JetBrains.Annotations;

namespace Zilon.Core.Commands
{
    [PublicAPI]
    public interface ICommandManager
    {
        void RegisterCommand(ICommand command);
        void ExecuteAllCommands();
    }
}
