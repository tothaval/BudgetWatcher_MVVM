using BudgetWatcher.Models;
using BudgetWatcher.Navigation;
using BudgetWatcher.Resources;
using BudgetWatcher.Utility;
using BudgetWatcher.ViewModels;
using BudgetWatcher.ViewModels.ViewLess;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using static System.Net.WebRequestMethods;
using System.Xml.Serialization;

namespace BudgetWatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private BudgetChangeViewModel _BudgetChangeViewModel;
        private NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();

        }


        private void AddResources()
        {
            new ResourceSet();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            Persistance persistance = new Persistance();

            persistance.SerializeNotes(_BudgetChangeViewModel.Budgets);
            persistance.SerializeResources();

            base.OnExit(e);
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            string budgetFolder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\budgets\\";

            if (!Directory.Exists(budgetFolder))
            {
                Directory.CreateDirectory(budgetFolder);
            }

            ObservableCollection<BudgetViewModel> budgets = new Persistance().DeSerializeNotes();


            _BudgetChangeViewModel = new BudgetChangeViewModel(budgets);

            _navigationStore.CurrentViewModel = _BudgetChangeViewModel;

            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string filter = "*.xml";
            List<string> files = Directory.GetFiles(folder, filter, SearchOption.TopDirectoryOnly).ToList();

            if (!Directory.EnumerateFiles(folder).Any(f => f.Contains("resources.xml")))
            {
                AddResources();
            }


            foreach (string file in files)
            {
                if (file.EndsWith("resources.xml"))
                {
                    var xmlSerializer = new XmlSerializer(typeof(ResourceSet));

                    using (var writer = new StreamReader(file))
                    {
                        try
                        {
                            var member = (ResourceSet)xmlSerializer.Deserialize(writer);

                            member.SetResources();

                            break;

                        }
                        catch (Exception)
                        {
                        }
                    }
                }

            }


            MainWindow mainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore, _BudgetChangeViewModel)
            };

            mainWindow.Show();
            base.OnStartup(e);

        }
    }
}
