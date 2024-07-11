/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  MainViewModel : BaseViewModel
 *  
 */
using BudgetWatcher.Commands;
using BudgetWatcher.Commands.ContextMenuCommands;
using BudgetWatcher.Navigation;
using BudgetWatcher.ViewModels.ViewLess;
using System.Windows.Input;

namespace BudgetWatcher.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        
        // Properties & Fields
        #region Properties & Fields

        private readonly BudgetChangeViewModel _BudgetChangeViewModel;
        public BudgetChangeViewModel BudgetChangeViewModel => _BudgetChangeViewModel;


        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;


        private readonly NavigationStore _navigationStore;


        public NoteViewModel NotesField { get; set; }


        public SetupFieldViewModel SetupField { get; }


        private bool _ShowBudget;
        public bool ShowBudget
        {
            get { return _ShowBudget; }
            set
            {
                _ShowBudget = value;
                OnPropertyChanged(nameof(ShowBudget));
            }
        }


        private bool _ShowBudgetOverview;
        public bool ShowBudgetOverview
        {
            get { return _ShowBudgetOverview; }
            set
            {
                _ShowBudgetOverview = value;
                OnPropertyChanged(nameof(ShowBudgetOverview));
            }
        }


        private bool _ShowNotes;
        public bool ShowNotes
        {
            get { return _ShowNotes; }
            set
            {
                _ShowNotes = value;
                OnPropertyChanged(nameof(ShowNotes));
            }
        }


        private bool _ShowSetup;
        public bool ShowSetup
        {
            get { return _ShowSetup; }
            set
            {
                _ShowSetup = value;
                OnPropertyChanged(nameof(ShowSetup));
            }
        } 
        #endregion


        // Commands
        #region Commands

        public ICommand AddBudgetCommand { get; }


        public ICommand CloseCommand { get; }


        public ICommand DuplicateBudgetCommand { get; }


        public ICommand LeftPressCommand { get; }


        public ICommand MaximizeCommand { get; }


        public ICommand MinimizeCommand { get; }


        public ICommand RemoveBudgetCommand { get; } 

        #endregion


        // Constructors
        #region Constructors

        public MainViewModel(NavigationStore navigationStore, BudgetChangeViewModel budgetChangeViewModel)
        {
            _navigationStore = navigationStore;
            _BudgetChangeViewModel = budgetChangeViewModel;

            UpdateNotesField();

            SetupField = new SetupFieldViewModel();


            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _BudgetChangeViewModel.PropertyChanged += _BudgetChangeViewModel_PropertyChanged;

            ShowBudget = true;

            AddBudgetCommand = new AddBudgetCommand(_BudgetChangeViewModel);
            CloseCommand = new CloseCommand();
            LeftPressCommand = new LeftPressCommand();
            MaximizeCommand = new MaximizeCommand();
            MinimizeCommand = new MinimizeCommand();
            RemoveBudgetCommand = new RemoveBudgetCommand(_BudgetChangeViewModel);

            SetupField.GainExpenseColorChange += GainExpenseColorChangeEvent;
        } 

        #endregion



        // Methods
        #region Methods

        private void UpdateNotesField()
        {
            if (_BudgetChangeViewModel.BudgetViewModel != null)
            {
                NotesField = new NoteViewModel(_BudgetChangeViewModel.BudgetViewModel.Note);
            }
        }

        #endregion


        // Events
        #region Events

        private void _BudgetChangeViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateNotesField();

            OnPropertyChanged(nameof(NotesField));
        }


        private void GainExpenseColorChangeEvent(object? sender, EventArgs e)
        {
            _BudgetChangeViewModel.UpdateGainExpenseBrush();
        }


        private void OnCurrentViewModelChanged()
        {
            UpdateNotesField();

            OnPropertyChanged(nameof(CurrentViewModel));
        } 

        #endregion


    }
}
// EOF