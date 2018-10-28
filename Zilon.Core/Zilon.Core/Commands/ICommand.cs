namespace Zilon.Core.Commands
{
    /// <summary>
    /// Интерфейс команды.
    /// </summary>
    /// <remarks>
    /// Все действия со стороны клиенты возможно только через команды.
    /// </remarks>
    public interface ICommand
    {
        void Execute();
        bool CanExecute();
    }
}
