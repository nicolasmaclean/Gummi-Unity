namespace Gummi.Utility.Command
{
    /// <summary>
    /// This interface is necessary to implement the Command Pattern and connect with CommandManager.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Called when this command is performed. This should be non-destructive so 
        /// <see cref="Undo"/> may undo everything performed in <see cref="Execute"/>.
        /// This should be called before <see cref="Undo"/>.
        /// </summary>
        void Execute();

        /// <summary>
        /// Called when this is undone. It will undo everything performed in the 
        /// <see cref="Execute"/> call. this should be called after <see cref="Execute"/>
        /// </summary>
        void Undo();
    }
}