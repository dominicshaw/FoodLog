using System;
using System.Threading.Tasks;
using App1.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FoodLog.Common;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        readonly MainViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            Messenger.Instance.Register<Notification>("Notification", async n =>
            {
                await DisplayMessage(n);
            });

            Messenger.Instance.Register<Exception>("Exception", async ex => { await DisplayException(ex);});

            BindingContext = _viewModel = new MainViewModel(new ApiWrapper());
        }

        //async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        //{
        //    var item = args.SelectedItem as EntryViewModel;
        //    if (item == null)
        //        return;

        //    await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

        //    // Manually deselect item.
        //    ItemsListView.SelectedItem = null;
        //}
        
        private async Task DisplayMessage(Notification notification)
        {
            if (string.IsNullOrEmpty(notification.Cancel))
                await DisplayAlert(notification.Title, notification.Message, notification.Accept);
            else
                await DisplayAlert(notification.Title, notification.Message, notification.Accept, notification.Cancel);

        }

        private async Task DisplayException(Exception exception)
        {            
            await DisplayAlert("Error", exception.Message, "OK");            
        }

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                //if (_viewModel.Entries.Count == 0)
                await _viewModel.Start();
            }
            catch (Exception ex)
            {
                Reporter.ReportException(ex);
            }
        }
    }
}