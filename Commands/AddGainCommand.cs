using BudgetWatcher.Models;
using BudgetWatcher.ViewModels.ViewLess;

namespace BudgetWatcher.Commands
{
    public class AddGainCommand : BaseCommand
    {
        private BudgetViewModel viewModel { get; set; }


        public AddGainCommand(BudgetViewModel budgetViewModel)
        {
            viewModel = budgetViewModel;
        }

        public void UpdateViewModel(BudgetViewModel budgetViewModel)
        {
            viewModel = budgetViewModel;
        }

        public override void Execute(object? parameter)
        {
            viewModel.AddBudgetItem(
                new BudgetItem()
                {
                    Interval = Enums.BudgetIntervals.Once,
                    Type = Enums.BudgetTypes.Gain
                });
        }
    }
}
// EOF