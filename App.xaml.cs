using BudgetWatcher.Resources;
using BudgetWatcher.Utility;
using BudgetWatcher.ViewModels;

using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
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


        protected override void OnExit(ExitEventArgs e)
        {
            Persistance persistance = new Persistance();

            persistance.SerializeNotes(_BudgetChangeViewModel.Budgets);
            persistance.SerializeResources();

            Log.CloseAndFlush();

            base.OnExit(e);
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
     .MinimumLevel.Information()
     .WriteTo.Console(Serilog.Events.LogEventLevel.Information)
     .WriteTo.File("bw_log.log", Serilog.Events.LogEventLevel.Information)
     .CreateLogger();

            RegisterResources();

            //ILoggerFactory loggerFactory = LoggerFactory.Create(logger =>
            //{
            //}
            //);

            //ILogger<BudgetChangeViewModel> logger = loggerFactory.CreateLogger<BudgetChangeViewModel>();



            CommunityToolkit.Mvvm.DependencyInjection.Ioc.Default.ConfigureServices(
                new ServiceCollection()

                    .AddSingleton<MainWindow>()
                    .AddSingleton<SetupFieldViewModel>()
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
            _BudgetChangeViewModel.Initialize();

            mainWindow.Show();

            Log.Logger.Information("startup complete");

            base.OnStartup(e);
        }


        private async void RegisterResources()
        {
            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            string filter = "*.xml";
            List<string> files = Directory.GetFiles(folder, filter, SearchOption.TopDirectoryOnly).ToList();


            if (!Directory.EnumerateFiles(folder).Any(f => f.Contains("resources.xml")))
            {
                Application.Current.Resources["Language"] = "English";

                Application.Current.Resources["FS"] = 14.0;
                Application.Current.Resources["FF"] = new FontFamily("Verdana");

                Application.Current.Resources["HFS"] = 14 * 1.25;

                Application.Current.Resources["Button_CornerRadius"] = new CornerRadius(5);

                Application.Current.Resources["VisibilityField_CornerRadius"] = new CornerRadius(5);


                Application.Current.Resources["BackgroundBrush"] = new SolidColorBrush(Colors.White);
                Application.Current.Resources["TextBrush"] = new SolidColorBrush(Colors.Black);
                Application.Current.Resources["HeaderBrush"] = new SolidColorBrush(Colors.YellowGreen);
                Application.Current.Resources["SelectionBrush"] = new SolidColorBrush(Colors.Gray);
                Application.Current.Resources["GainBrush"] = new SolidColorBrush(Colors.Green);
                Application.Current.Resources["ExpenseBrush"] = new SolidColorBrush(Colors.Red);

                //MessageBox.Show(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag).ToString());

                Application.Current.Resources["Culture"] = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

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