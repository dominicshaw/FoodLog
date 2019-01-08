using System.Windows;
using FoodLog.Wpf.Api;
using FoodLog.Wpf.ViewModels;
using Microsoft.Rest;

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

            MainWindow = new MainWindow(new MainViewModel(new ApiWrapper(new FoodLogClient.FoodLogClient(new BasicAuthenticationCredentials()))));
            MainWindow.Show();
        }
    }
}
