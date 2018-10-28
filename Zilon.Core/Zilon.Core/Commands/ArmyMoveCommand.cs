using Zilon.Core.ClientState;

namespace Zilon.Core.Commands
{
    public class ArmyMoveCommand: IArmyModeCommand
    {
        private readonly IGlobalState _globalState;

        public ArmyMoveCommand(IGlobalState globalState)
        {
            _globalState = globalState;
        }

        public void Execute()
        {
            var army = _globalState.SelectedArmy.Army;
            var node = _globalState.HoverNode.Node;
            army.MoveTo(node);
        }

        public bool CanExecute()
        {
            if (_globalState.SelectedArmy == null)
            {
                return false;
            }

            return true;
        }
    }
}
