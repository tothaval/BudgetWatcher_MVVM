using System.Windows;
using System.Windows.Media;


namespace BudgetWatcher.Commands;


public static class ContextMenuCommandsHandler
{

    public static void CloseMainWindow(object? parameter)
    {
        Application.Current.Shutdown();
    }


    public static void DragMoveMainWindow(object? parameter)
    {
        MainWindow mainWindow = (MainWindow)parameter;

        if (mainWindow != null)
        {
            mainWindow.DragMove();
        }
        else
        {
            Application.Current.MainWindow.DragMove();
        }
    }


    public static void MaximizeMainWindow(object? parameter)
    {
        MainWindow mainWindow = (MainWindow)parameter;


        if (mainWindow.WindowState == WindowState.Normal)
        {
            mainWindow.SizeToContent = SizeToContent.Manual;

            mainWindow.WindowState = WindowState.Maximized;
            mainWindow.Background = (SolidColorBrush)Application.Current.Resources["BackgroundBrush"];
            Application.Current.Resources["MaximizeContextMenuItemHeader"] = "Normalize";
        }
        else
        {
            mainWindow.SizeToContent = SizeToContent.WidthAndHeight;

            mainWindow.WindowState = WindowState.Normal;
            mainWindow.Background = new SolidColorBrush(Colors.Transparent);
            Application.Current.Resources["MaximizeContextMenuItemHeader"] = "Maximize";
        }
    }

    public static void MinimizeMainWindow(object? parameter)
    {
        MainWindow mainWindow = (MainWindow)parameter;

        mainWindow.WindowState = System.Windows.WindowState.Minimized;
    }
}
