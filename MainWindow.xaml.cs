using System.Windows;

namespace BudgetWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
    }
}
// EOF