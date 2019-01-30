using System.Windows;
using FoodLog.Common;
using MainViewModel = FoodLog.Common.MainViewModel;

namespace FoodLog.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow = new MainWindow(new MainViewModel(new ApiWrapper()));
            MainWindow.Show();
        }
    }
}
