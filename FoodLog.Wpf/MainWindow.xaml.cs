using DevExpress.Xpf.Core;
using FoodLog.Wpf.ViewModels;

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

            DataContext = _model;

            Loaded += MainWindow_Loaded;
        }
        
        private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _model.Start();
        }
    }
}
