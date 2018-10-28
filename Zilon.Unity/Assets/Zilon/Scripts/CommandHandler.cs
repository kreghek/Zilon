using UnityEngine;

using Zenject;

using Zilon.Core.Commands;

public class CommandHandler : MonoBehaviour
{
    [Inject] private ICommandManager _commandManager;

    void Update()
    {
        _commandManager.ExecuteAllCommands();
    }
}
