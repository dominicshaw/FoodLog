using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FoodLog.Wpf.Api;
using FoodLog.Wpf.Properties;

namespace FoodLog.Wpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApiWrapper _api;

        public ObservableCollection<EntryViewModel> Entries { get; } = new ObservableCollection<EntryViewModel>();

        public MainViewModel(ApiWrapper api)
        {
            _api = api;
        }

        public async Task Start()
        {
            foreach(var e in await _api.GetEntries())
                Entries.Add(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
