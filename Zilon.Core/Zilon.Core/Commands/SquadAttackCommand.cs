using Zilon.Core.ClientState;

namespace Zilon.Core.Commands
{
    public class SquadAttackCommand : ISquadAttackCommand
    {
        private readonly ICombatStateManager _combatStateManager;

        public SquadAttackCommand(ICombatStateManager combatStateManager)
        {
            _combatStateManager = combatStateManager;
        }

        public void Execute()
        {
            var squad = _combatStateManager.SelectedSquad.Squad;
            var targetSquad = _combatStateManager.HoverSquad.Squad;
            squad.UseSkill(targetSquad);
        }

        public bool CanExecute()
        {
            if (_combatStateManager.SelectedSquad == null)
            {
                return false;
            }

            if (_combatStateManager.HoverSquad == null)
            {
                return false;
            }

            return true;
        }
    }
}
