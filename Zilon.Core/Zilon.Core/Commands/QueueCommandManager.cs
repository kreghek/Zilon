using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Zilon.Core.Commands
{
    public class QueueCommandManager : ICommandManager
    {
        private readonly Queue<ICommand> _queue;

        [ExcludeFromCodeCoverage]
        public QueueCommandManager()
        {
            _queue = new Queue<ICommand>();
        }

        public void ExecuteAllCommands()
        {
            while (_queue.Any())
            {
                var command = _queue.Dequeue();
                command.Execute();
            }
        }

        public void RegisterCommand(ICommand command)
        {
            _queue.Enqueue(command);
        }
    }
}
