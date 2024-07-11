using BudgetWatcher.ViewModels.ViewLess;

namespace BudgetWatcher.Commands
{
    public class ClearAllCommand : BaseCommand
    {
        private BudgetViewModel viewModel {  get; set; }


        public ClearAllCommand(BudgetViewModel budgetViewModel)
        {
            viewModel = budgetViewModel;
        }


        public override void Execute(object? parameter)
        {
            viewModel.Clear();
        }

        public void UpdateViewModel(BudgetViewModel budgetViewModel)
        {
            viewModel = budgetViewModel;
        }
    }
}
// EOF