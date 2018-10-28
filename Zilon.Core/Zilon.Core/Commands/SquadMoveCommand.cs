using Zilon.Core.ClientState;

namespace Zilon.Core.Commands
{
    public class SquadMoveCommand : ISquadMoveCommand
    {
        private readonly ICombatStateManager _combatStateManager;

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
