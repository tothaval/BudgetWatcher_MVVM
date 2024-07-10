using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
