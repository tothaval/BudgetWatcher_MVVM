using BudgetWatcher.ViewModels;
using BudgetWatcher.ViewModels.ViewLess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetWatcher.Commands
{
    public class RemoveBudgetCommand : BaseCommand
    {

        private readonly BudgetChangeViewModel _BudgetChangeViewModel;


        public RemoveBudgetCommand(BudgetChangeViewModel budgetChangeViewModel)
        {
                _BudgetChangeViewModel = budgetChangeViewModel;
        }

        public override void Execute(object? parameter)
        {

            IList selection = (IList)parameter;

            if (selection != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Do you want to delete selected budget(s)?",
                    "Remove Budget Item(s)", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var selected = selection.Cast<BudgetViewModel>().ToArray();

                    foreach (var item in selected)
                    {
                        _BudgetChangeViewModel.RemoveBudget(item);
                    }
                }
            }
        }
    }
}
