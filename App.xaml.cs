using BudgetWatcher.Resources;
using BudgetWatcher.Utility;
using BudgetWatcher.ViewModels;
using BudgetWatcher.ViewModels.ViewLess;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace BudgetWatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private BudgetChangeViewModel _BudgetChangeViewModel;

        /// <summary>
        /// für dependency injection den budget manager auf dieser ebene instanzieren und laden,
        /// dann per di an mainviewmodel übergeben
        /// </summary>
        public App()
        {
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
            RegisterResources();

            CommunityToolkit.Mvvm.DependencyInjection.Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<MainWindow>()
                    .AddSingleton<BudgetChangeViewModel>()
                    .AddSingleton<MainViewModel>()
                    .BuildServiceProvider()
                    );

            // Microsoft Dependency Injection
            //ServiceCollection services = new ServiceCollection();

            //services.AddSingleton<MainWindow>();

            //services.AddSingleton<BudgetChangeViewModel>();

            //services.AddSingleton<MainViewModel>();

            //ServiceProvider provider = services.BuildServiceProvider();

            MainWindow mainWindow = Ioc.Default.GetService<MainWindow>()!;

            mainWindow.DataContext = Ioc.Default.GetService<MainViewModel>();

            _BudgetChangeViewModel = Ioc.Default.GetService<BudgetChangeViewModel>()!;

            mainWindow.Show();
            base.OnStartup(e);
        }


        private void RegisterResources()
        {
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

        }
    }
}
// EOF