using BudgetWatcher.ViewModels.ViewLess;

namespace BudgetWatcher.Navigation
{
    internal class NavigationStore
    {
        private BaseViewModel _baseViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _baseViewModel; }
            set
            {
                _baseViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

    }
}
// EOF