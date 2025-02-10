/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  BudgetChangeViewModel : BaseViewModel
 * 
 *  viewmodel for BudgetChangeView
 *  
 *  allows for editing of BudgetViewModel
 *  
 *  is encapsulated within a MainViewModel
 */
using BudgetWatcher.Commands;
using BudgetWatcher.Models;
using BudgetWatcher.Utility;
using BudgetWatcher.ViewModels.ViewLess;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace BudgetWatcher.ViewModels
{
    public partial class BudgetChangeViewModel : ObservableObject
    {

        // Properties & Fields
        #region Properties & Fields

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(BudgetItemViewModels))]
        private BudgetViewModel _BudgetViewModel;

        #endregion


        // Collections
        #region Collections

        public ObservableCollection<BudgetItemViewModel> BudgetItemViewModels
        {
            get
            {
                if (BudgetViewModel != null)
                {
                    return BudgetViewModel.BudgetItemViewModels;
                }
                else
                {
                    return new ObservableCollection<BudgetItemViewModel>();
                }

            }

            set
            {
                BudgetViewModel.BudgetItemViewModels = value;
                OnPropertyChanged(nameof(BudgetItemViewModels));
            }
        }


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(BudgetItemViewModels))]
        private ObservableCollection<BudgetViewModel> _Budgets;

        #endregion


        // Constructors
        #region Constructors

        // passing arguments to constructor with di
        // one way: https://stackoverflow.com/questions/37744637/how-can-i-pass-a-runtime-parameter-as-part-of-the-dependency-resolution
        // 2nd way: https://stackoverflow.com/questions/53884417/net-core-di-ways-of-passing-parameters-to-constructor

        public BudgetChangeViewModel()
        {
            string budgetFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\budgets\\";

            if (!Directory.Exists(budgetFolder))
            {
                Directory.CreateDirectory(budgetFolder);
            }

            Budgets = new Persistance().DeSerializeNotes();

            SelectFirst();

            OnPropertyChanged(nameof(BudgetViewModel));
            OnPropertyChanged(nameof(Budgets));
            OnPropertyChanged(nameof(BudgetItemViewModels));
        }

        #endregion


        // Methods
        #region Methods

        public void AddBudget(BudgetViewModel budgetViewModel)
        {
            Budgets.Insert(0, budgetViewModel);

            SelectFirst();
            OnPropertyChanged(nameof(BudgetViewModel));
            OnPropertyChanged(nameof(Budgets));
            OnPropertyChanged(nameof(BudgetItemViewModels));
        }


        [RelayCommand]
        private void AddExpense(object? parameter)
        {
            BudgetViewModel.AddBudgetItem(
                new BudgetItem()
                {
                    Interval = Enums.BudgetIntervals.Once,
                    Type = Enums.BudgetTypes.Expense
                });
        }


        [RelayCommand]
            private void AddGain(object? parameter)
        {
            BudgetViewModel.AddBudgetItem(
                new BudgetItem()
                {
                    Interval = Enums.BudgetIntervals.Once,
                    Type = Enums.BudgetTypes.Gain
                });
        }


        [RelayCommand]
            private void ClearAll(object? parameter)
        {
            BudgetViewModel.Clear();
        }


        [RelayCommand]
        private void LeftPress(object? parameter)
        {
            ContextMenuCommandsHandler.DragMoveMainWindow(parameter);
        }


        [RelayCommand]
            private void RemoveItem(object? parameter)
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
                        BudgetViewModel.RemoveBudgetItem(item.GetBudgetItem);
                    }
                }
            }
        }


        public void RemoveBudget(BudgetViewModel budgetViewModel)
        {
            Budgets.Remove(budgetViewModel);

            SelectFirst();
            OnPropertyChanged(nameof(BudgetViewModel));
            OnPropertyChanged(nameof(Budgets));
            OnPropertyChanged(nameof(BudgetItemViewModels));
        }


        private void SelectFirst()
        {
            if (Budgets.Count > 0)
            {
                BudgetViewModel = Budgets.First();
            }
        }

        public void UpdateGainExpenseBrush()
        {
            BudgetViewModel.UpdateGainExpenseBrush();
        }

        #endregion


    }
}
// EOF