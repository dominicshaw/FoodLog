using System;
using System.Windows;
using DevExpress.Xpf.Core;
using FoodLog.Common;
using MainViewModel = FoodLog.Common.MainViewModel;

namespace FoodLog.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        private readonly MainViewModel _model;

        public MainWindow(MainViewModel model)
        {
            _model = model;
            InitializeComponent();

            Messenger.Instance.Register<Notification>("Notification", DisplayMessage);

            Messenger.Instance.Register<Exception>("Exception", DisplayException);

            DataContext = _model;

            Loaded += MainWindow_Loaded;
        }
        
        private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                await _model.Start();
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }
        }

        private void DisplayMessage(Notification notification)
        {
            DXMessageBox.Show(notification.Message, notification.Title, MessageBoxButton.OK, MessageBoxImage.Information);            
        }

        private void DisplayException(Exception exception)
        {
            DXMessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
