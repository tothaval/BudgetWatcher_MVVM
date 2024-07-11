using BudgetWatcher.Models;
using BudgetWatcher.ViewModels.ViewLess;

namespace BudgetWatcher.Commands
{
    public class AddExpenseCommand : BaseCommand
    {
        private BudgetViewModel viewModel {  get; set; }


        public AddExpenseCommand(BudgetViewModel budgetViewModel)
        {
            viewModel = budgetViewModel;                
        }


        public override void Execute(object? parameter)
        {
            viewModel.AddBudgetItem(
                new BudgetItem()
                {
                    Interval = Enums.BudgetIntervals.Once,
                    Type = Enums.BudgetTypes.Expense
                });
        }

        public void UpdateViewModel(BudgetViewModel budgetViewModel)
        {
            viewModel = budgetViewModel;
        }


    }
}
// EOF