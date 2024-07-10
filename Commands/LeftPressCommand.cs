using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
