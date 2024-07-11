using BudgetWatcher.Models;
using BudgetWatcher.ViewModels;
using BudgetWatcher.ViewModels.ViewLess;

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
// EOF