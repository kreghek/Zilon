using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;

using Zilon.Core.ClientState;

namespace Zilon.Core.Commands
{
    [PublicAPI]
    public class SquadMoveCommand : ISquadMoveCommand
    {
        private readonly ICombatStateManager _combatStateManager;

        [ExcludeFromCodeCoverage]
        public SquadMoveCommand(ICombatStateManager globalState)
        {
            _combatStateManager = globalState;
        }

        public void Execute()
        {
            var squad = _combatStateManager.SelectedSquad.Squad;
            var node = _combatStateManager.HoverNode.Node;
            squad.MoveToNode(node);
        }

        public bool CanExecute()
        {
            if (_combatStateManager.SelectedSquad == null)
            {
                return false;
            }

            return true;
        }
    }
}
