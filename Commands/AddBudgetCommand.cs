using BudgetWatcher.Models;
using BudgetWatcher.ViewModels;
using BudgetWatcher.ViewModels.ViewLess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetWatcher.Commands
{
    public class AddBudgetCommand : BaseCommand
    {
        private readonly BudgetChangeViewModel _BudgetChangeViewModel;

        public AddBudgetCommand(BudgetChangeViewModel budgetChangeViewModel)
        {
            _BudgetChangeViewModel = budgetChangeViewModel;
                
        }

        public override void Execute(object? parameter)
        {
            _BudgetChangeViewModel.AddBudget(
                new BudgetViewModel(new Budget()));
        }
    }
}
