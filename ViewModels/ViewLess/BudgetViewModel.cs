/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  BudgetViewModel : BaseViewModel
 *  
 */
using BudgetManagement;
using BudgetWatcher.Enums;
using BudgetWatcher.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Budget = BudgetWatcher.Models.Budget;



namespace BudgetWatcher.ViewModels.ViewLess
{
    public partial class BudgetViewModel : ObservableObject
    {

        // Properties & Fields
        #region Properties & Fields
        public DateTime Begin
        {
            get { return _Budget.Begin; }
            set
            {
                _Budget.Begin = value;
                OnPropertyChanged(nameof(Begin));
                OnPropertyChanged(nameof(NumberOfDays));
                OnPropertyChanged(nameof(DaysLeftPercentage));
            }
        }


        private Budget _Budget;
        public Budget GetBudget => _Budget;

        private BudgetManagement.Budget TestBudget { get; set; }


        public double BudgetPerDay => GetBudgetPerDay();


        public double CurrentBalance => _Budget.CurrentBalance;


        // percentage didn't work out, change this and related stuff to DaysLeft
        public double DaysLeftPercentage => GetDaysLeftPercentage();


        public DateTime End
        {
            get { return _Budget.End; }
            set
            {
                _Budget.End = value + TimeSpan.FromDays(1) - TimeSpan.FromSeconds(1);

                OnPropertyChanged(nameof(End));
                OnPropertyChanged(nameof(NumberOfDays));
                OnPropertyChanged(nameof(DaysLeftPercentage));
            }
        }


        public double Expenses
        {
            get { return _Budget.Expenses; }
            set
            {
                _Budget.Expenses = value;

                OnPropertyChanged(nameof(BudgetPerDay));
                OnPropertyChanged(nameof(CurrentBalance));
                OnPropertyChanged(nameof(Expenses));
                OnPropertyChanged(nameof(GainExpenseBrush));
            }
        }


        public Brush GainExpenseBrush
        {
            get
            {
                if (CurrentBalance > 0.0)
                {
                    return (SolidColorBrush)Application.Current.Resources["GainBrush"];
                }
                else if (CurrentBalance == 0.0)
                {
                    return (SolidColorBrush)Application.Current.Resources["BackgroundBrush"];
                }

                return (SolidColorBrush)Application.Current.Resources["ExpenseBrush"];
            }
        }


        public double Gains
        {
            get { return _Budget.Gains; }
            set
            {
                _Budget.Gains = value;

                OnPropertyChanged(nameof(BudgetPerDay));

                OnPropertyChanged(nameof(CurrentBalance));

                OnPropertyChanged(nameof(GainExpenseBrush));

                OnPropertyChanged(nameof(Gains));
            }
        }


        public double InitialBudget
        {
            get { return _Budget.InitialBudget; }
            set
            {
                _Budget.InitialBudget = value;
                OnPropertyChanged(nameof(GainExpenseBrush));
                OnPropertyChanged(nameof(CurrentBalance));
                OnPropertyChanged(nameof(InitialBudget));
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


        public int NumberOfDays => (End - Begin).Days;


        private DispatcherTimer dispatcherTimer;
        #endregion


        // Collections
        #region Collections

        public ObservableCollection<BudgetItem> BudgetChanges
        {
            get { return _Budget.BudgetChanges; }
            set
            {
                _Budget.BudgetChanges = value;
                OnPropertyChanged(nameof(BudgetChanges));
            }
        }


        [ObservableProperty]
        private ObservableCollection<BudgetItemViewModel> _BudgetItemViewModels;      

        #endregion


        // Constructors
        #region Constructors

        public BudgetViewModel(Budget budget)
        {
            _Budget = budget;

            BudgetItemViewModels = new ObservableCollection<BudgetItemViewModel>();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMinutes(5);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();


            OnPropertyChanged(nameof(BudgetItemViewModels));

            CreateBudgetItemViewModels();

        }
               


        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(DaysLeftPercentage));
            OnPropertyChanged(nameof(BudgetPerDay));
        }

        #endregion


        // Methods
        #region Methods

        public void AddBudgetItem(BudgetItem budgetItem)
        {
            BudgetChanges.Insert(0, budgetItem);

            CreateBudgetItemViewModel(budgetItem);

            ////testing purposes
            //TestBudget = new BudgetManagement.Budget()
            //{
            //    BudgetPeriodStart = _Budget.Begin,
            //    BudgetPeriodEnd = _Budget.End,
            //    InitialBudget = (decimal)_Budget.InitialBudget,
            //};

            //bool test = budgetItem.Type.Equals(BudgetTypes.Expense);

            //TestBudget.AddBudgetChange(new BudgetManagement.BudgetChange()
            //{
            //    Price = (decimal)budgetItem.Sum,
            //    Quantity = budgetItem.Quantity,
            //    Item = budgetItem.Item,
            //    IsExpense = test,
            //    BudgetChangeDate = DateTime.Now,
            //    Identifier = BudgetChangeModule.GetBudgetChangeIdentifier()
            //});


            //TestBudget.AddBudgetChange(new BudgetManagement.BudgetChange()
            //{
            //    Price = 125m,
            //    Quantity = 15,
            //    Item = "sunday icecream",
            //    IsExpense = true,
            //    BudgetChangeDate = DateTime.Now,
            //    Identifier = BudgetChangeModule.GetBudgetChangeIdentifier()

            //});

            //TestBudget.AddBudgetChange(new BudgetManagement.BudgetChange()
            //{
            //    Price = 15.15m,
            //    Quantity = 3,
            //    Item = "gardening mini job",
            //    IsExpense = false,
            //    BudgetChangeDate = DateTime.Now,
            //    Identifier = BudgetChangeModule.GetBudgetChangeIdentifier()

            //});

            //MessageBox.Show(TestBudget.GetCurrentBalance().ToString());

            //TestBudget.BudgetPeriodStart = _Budget.Begin;

        }


        private void Calculate()
        {
            Expenses = 0.0;
            Gains = 0.0;

            foreach (BudgetItemViewModel item in BudgetItemViewModels)
            {
                if (item.Type == Enums.BudgetTypes.Expense)
                {
                    Expenses += item.Result;
                }
                else if (item.Type == Enums.BudgetTypes.Gain)
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

                Recalculate();
            }
        }


        public void CreateBudgetItemViewModel(BudgetItem budgetItem)
        {
            BudgetItemViewModel budgetItemViewModel = new BudgetItemViewModel(budgetItem, this);

            budgetItemViewModel.ValueChange += BudgetItemViewModel_ValueChange;

            BudgetItemViewModels.Insert(0, budgetItemViewModel);

        }


        private void CreateBudgetItemViewModels()
        {
            BudgetItemViewModels = new ObservableCollection<BudgetItemViewModel>();

            foreach (BudgetItem item in BudgetChanges)
            {
                CreateBudgetItemViewModel(item);
            }

            OnPropertyChanged(nameof(BudgetItemViewModels));
        }


        private double GetBudgetPerDay()
        {
            return CurrentBalance / DaysLeftPercentage;
        }


        /// <summary>
        /// Calculates the number of days left in the budget period.
        /// if double value is < -0.0, returns 0.0, else value
        /// period begins 0:00 Begin date and ends 23:59 End date.
        /// 
        /// Percentage may work well with values below 1 day, but
        /// above the large percentage numbers may distract the user, dunno.
        /// /// </summary>
        /// <returns>double (End - DateTime.Now).TotalDays </returns>
        private double GetDaysLeftPercentage()
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
            Calculate();
            OnPropertyChanged(nameof(NumberOfDays));
            OnPropertyChanged(nameof(DaysLeftPercentage));
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


            Recalculate();

            OnPropertyChanged(nameof(BudgetItemViewModels));
        }


        public void UpdateGainExpenseBrush()
        {
            OnPropertyChanged(nameof(GainExpenseBrush));

            foreach (BudgetItemViewModel item in BudgetItemViewModels)
            {
                item.UpdateGainExpenseBrush();
            }
        } 

        #endregion


        // Events
        #region Events

        private void BudgetItemViewModel_ValueChange(object? sender, EventArgs e)
        {
            Recalculate();
        } 

        #endregion

    }
}
// EOF