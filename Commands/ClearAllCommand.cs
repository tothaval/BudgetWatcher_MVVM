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
