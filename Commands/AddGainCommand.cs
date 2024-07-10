using BudgetWatcher.Models;
using BudgetWatcher.ViewModels.ViewLess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
