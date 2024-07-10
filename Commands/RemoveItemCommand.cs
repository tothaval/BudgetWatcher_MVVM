using BudgetWatcher.Models;
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
    public class RemoveItemCommand : BaseCommand
    {
        private BudgetViewModel viewModel {  get; set; }


        public RemoveItemCommand(BudgetViewModel budgetViewModel)
        {
            viewModel = budgetViewModel;
        }


        public override void Execute(object? parameter)
        {

            IList selection = (IList)parameter;

            if (selection != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Do you want to delete selected budget item(s)?",
                    "Remove Budget Item(s)", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var selected = selection.Cast<BudgetItemViewModel>().ToArray();

                    foreach (var item in selected)
                    {
                       viewModel.RemoveBudgetItem(item.GetBudgetItem);
                    }
                }
            }


        }

        public void UpdateViewModel(BudgetViewModel budgetViewModel)
        {
            viewModel = budgetViewModel;
        }


    }
}