using UnityEngine;

using Zenject;

using Zilon.Core.Commands;

public class CommandHandler : MonoBehaviour
{
    [Inject] private ICommandManager _commandManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _commandManager.ExecuteAllCommands();
    }
}
