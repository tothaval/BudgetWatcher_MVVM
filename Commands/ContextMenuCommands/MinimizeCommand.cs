using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetWatcher.Commands.ContextMenuCommands
{
    public class MinimizeCommand : BaseCommand
    {
        public override void Execute(object? parameter)
        {
            MainWindow mainWindow = (MainWindow)parameter;

            mainWindow.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
