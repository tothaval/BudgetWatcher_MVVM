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
using BudgetWatcher.ViewModels.ViewLess;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BudgetWatcher.ViewModels
{
    public class BudgetChangeViewModel : BaseViewModel
    {

        // Properties & Fields
        #region Properties & Fields

        private BudgetViewModel _BudgetViewModel;
        public BudgetViewModel BudgetViewModel
        {
            get { return _BudgetViewModel; }
            set
            {
                _BudgetViewModel = value;

                OnPropertyChanged(nameof(BudgetViewModel));

                OnPropertyChanged(nameof(BudgetItemViewModels));

                UpdateCommands(BudgetViewModel);
            }
        }

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

                return new ObservableCollection<BudgetItemViewModel>();
            }

            set
            {
                BudgetViewModel.BudgetItemViewModels = value;
                OnPropertyChanged(nameof(BudgetItemViewModels));
            }
        }


        private ObservableCollection<BudgetViewModel> _Budgets;
        public ObservableCollection<BudgetViewModel> Budgets
        {
            get { return _Budgets; }
            set
            {
                _Budgets = value;

                OnPropertyChanged(nameof(Budgets));
                OnPropertyChanged(nameof(BudgetItemViewModels));
            }
        }

        #endregion

        
        // Commands
        #region Commands

        public ICommand AddExpenseCommand { get; private set; }


        public ICommand AddGainCommand { get; private set; }


        public ICommand ClearAllCommand { get; private set; }


        public ICommand LeftPressCommand { get; private set; }


        public ICommand RemoveItemCommand { get; private set; }

        #endregion


        // Constructors
        #region Constructors

        public BudgetChangeViewModel(ObservableCollection<BudgetViewModel> budgets)
        {
            Budgets = budgets;

            SelectFirst();

            AddExpenseCommand = new AddExpenseCommand(BudgetViewModel);
            AddGainCommand = new AddGainCommand(BudgetViewModel);
            ClearAllCommand = new ClearAllCommand(BudgetViewModel);
            LeftPressCommand = new LeftPressCommand();
            RemoveItemCommand = new RemoveItemCommand(BudgetViewModel);

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


        private void UpdateCommands(BudgetViewModel budgetViewModel)
        {
            if (budgetViewModel != null
                && AddExpenseCommand != null
                && AddGainCommand != null
                && ClearAllCommand != null
                && RemoveItemCommand != null)
            {
                ((AddExpenseCommand)AddExpenseCommand).UpdateViewModel(budgetViewModel);
                ((AddGainCommand)AddGainCommand).UpdateViewModel(budgetViewModel);
                ((ClearAllCommand)ClearAllCommand).UpdateViewModel(budgetViewModel);
                ((RemoveItemCommand)RemoveItemCommand).UpdateViewModel(budgetViewModel);
            }
        } 

        #endregion


    }
}
// EOF