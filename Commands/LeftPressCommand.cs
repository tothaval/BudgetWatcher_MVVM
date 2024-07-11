using System.Windows;

namespace BudgetWatcher.Commands
{
    public class LeftPressCommand : BaseCommand
    {

        public LeftPressCommand()
        {
                
        }


        public override void Execute(object? parameter)
        {
            MainWindow mainWindow = (MainWindow)parameter;

            if (mainWindow != null)
            {
                mainWindow.DragMove();
            }
            else
            {
                Application.Current.MainWindow.DragMove();
            }
            
        }
    }
}
// EOF