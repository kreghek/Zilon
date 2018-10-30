using System.Diagnostics.CodeAnalysis;

using JetBrains.Annotations;

using Zilon.Core.ClientState;

namespace Zilon.Core.Commands
{
    [PublicAPI]
    public class ArmyMoveCommand: IArmyModeCommand
    {
        private readonly IGlobalStateManager _globalState;

        [ExcludeFromCodeCoverage]
        public ArmyMoveCommand(IGlobalStateManager globalState)
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
