using System.Windows;

namespace BudgetWatcher.Commands.ContextMenuCommands
{
    public class CloseCommand : BaseCommand
    {


        public CloseCommand()
        {

        }


        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
// EOF