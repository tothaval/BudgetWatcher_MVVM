using BudgetWatcher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BudgetWatcher.ViewModels.ViewLess
{
    public class BudgetViewModel : BaseViewModel
    {
        private Budget _Budget;
        public Budget GetBudget => _Budget;

        public int NumberOfDays => (End - Begin).Days;

        public double NumberOfDaysLeft => GetNumberOfDaysLeft();


        public DateTime Begin
        {
            get { return _Budget.Begin; }
            set
            {
                _Budget.Begin = value;
                OnPropertyChanged(nameof(Begin));
                OnPropertyChanged(nameof(NumberOfDays));
                OnPropertyChanged(nameof(NumberOfDaysLeft));
            }
        }


        public DateTime End
        {
            get { return _Budget.End; }
            set
            {
                _Budget.End = value + TimeSpan.FromDays(1) - TimeSpan.FromSeconds(1);

                OnPropertyChanged(nameof(End));
                OnPropertyChanged(nameof(NumberOfDays));
                OnPropertyChanged(nameof(NumberOfDaysLeft));
            }
        }


        public double CurrentBalance => _Budget.CurrentBalance;

        public double InitialBudget
		{
			get { return _Budget.InitialBudget; }
			set
			{
                _Budget.InitialBudget = value;
                OnPropertyChanged(nameof(CurrentBalance));
                OnPropertyChanged(nameof(InitialBudget));
			}
		}

        public double Expenses
        {
            get { return _Budget.Expenses; }
            set
            {
                _Budget.Expenses = value;
                OnPropertyChanged(nameof(CurrentBalance));
                OnPropertyChanged(nameof(Expenses));
            }
        }

        public double Gains
        {
            get { return _Budget.Gains; }
            set
            {
                _Budget.Gains = value;
                OnPropertyChanged(nameof(CurrentBalance));
                OnPropertyChanged(nameof(Gains));
            }
        }


        public Note Note
        {
            get { return _Budget.Note; }
            set
            {
                _Budget.Note = value;
                OnPropertyChanged(nameof(Note));
            }
        }


        public ObservableCollection<BudgetItem> BudgetChanges
        {
            get { return _Budget.BudgetChanges; }
            set
            {
                _Budget.BudgetChanges = value;
                OnPropertyChanged(nameof(BudgetChanges));
            }
        }


        private ObservableCollection<BudgetItemViewModel> _BudgetItemViewModels;
        public ObservableCollection<BudgetItemViewModel> BudgetItemViewModels
        {
            get { return _BudgetItemViewModels; }
            set
            {
                _BudgetItemViewModels = value;
                OnPropertyChanged(nameof(BudgetItemViewModels));
            }
        }




        public BudgetViewModel(Budget budget)
        {
            _Budget = budget;

            BudgetItemViewModels = new ObservableCollection<BudgetItemViewModel>();

            OnPropertyChanged(nameof(BudgetItemViewModels));

            CreateBudgetItemViewModels();

            BudgetItemViewModels.CollectionChanged += BudgetItemViewModels_CollectionChanged;            
        }


        public void AddBudgetItem(BudgetItem budgetItem)
        {
            BudgetChanges.Add(budgetItem);

            CreateBudgetItemViewModel(budgetItem);
        }


        private void CalculateExpenses()
        {
            Expenses = 0.0;

            foreach (BudgetItem item in BudgetChanges)
            {
                if (item.Type == Enums.BudgetTypes.Expense)
                {
                    Expenses += item.Result;
                }
            }
        }


        private void CalculateGains()
        {
            Gains = 0.0;

            foreach (BudgetItem item in BudgetChanges)
            {
                if (item.Type == Enums.BudgetTypes.Gain)
                {
                    Gains += item.Result;
                }
            }
        }


        public void Clear()
        {
            MessageBoxResult result = MessageBox.Show(
                $"Do you want to delete all budget item(s)?",
                "Remove Budget Item(s)", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                BudgetChanges.Clear();
                BudgetItemViewModels.Clear();
            }
        }


        public void CreateBudgetItemViewModel(BudgetItem budgetItem)
        {
            BudgetItemViewModel budgetItemViewModel = new BudgetItemViewModel(budgetItem, this);

            budgetItemViewModel.PropertyChanged += BudgetItemViewModel_PropertyChanged;

            budgetItemViewModel.ValueChange += BudgetItemViewModel_ValueChange;

            BudgetItemViewModels.Add(budgetItemViewModel);

        }


        private void CreateBudgetItemViewModels()
        {
            BudgetItemViewModels = new ObservableCollection<BudgetItemViewModel>();

            foreach (BudgetItem item in BudgetChanges)
            {
                CreateBudgetItemViewModel(item);
            }
        }


        /// <summary>
        /// Calculates the number of days left in the budget period.
        /// if double value is < -0.0, returns 0.0, else value
        /// period begins 0:00 Begin date and ends 23:59 End date.
        /// /// </summary>
        /// <returns>double (End - DateTime.Now).TotalDays </returns>
        private double GetNumberOfDaysLeft()
        {    
            double days = (End - DateTime.Now).TotalDays;

            if (days < 0.0)
            {
                return 0.0;
            }

            return days;
        }


        private void Recalculate()
        {
            CalculateExpenses();
            CalculateGains();
            OnPropertyChanged(nameof(NumberOfDays));
            OnPropertyChanged(nameof(NumberOfDaysLeft));
        }


        public void RemoveBudgetItem(BudgetItem budgetItem)
        {
            BudgetChanges.Remove(budgetItem);

            foreach (BudgetItemViewModel item in BudgetItemViewModels)
            {
                if (item.GetBudgetItem == budgetItem)
                {
                    BudgetItemViewModels.Remove(item);
                    break;
                }
            }
        }


        private void BudgetItemViewModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Recalculate();
        }


        private void BudgetItemViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Recalculate();
        }

        private void BudgetItemViewModel_ValueChange(object? sender, EventArgs e)
        {
            Recalculate();
        }

    }
}
