/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  BaseViewModel
 *  
 *  implements INotifyPropertyChanged
 */
using System.ComponentModel;

namespace BudgetWatcher.ViewModels.ViewLess
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
// EOF