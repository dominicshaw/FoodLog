using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using FoodLog.Common;
using FoodLog.Wpf.Api;
using FoodLog.Wpf.Properties;

namespace FoodLog.Wpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ApiWrapper _api;

        public ObservableCollection<EntryViewModel> Entries { get; } = new ObservableCollection<EntryViewModel>();

        private EntryViewModel _selectedEntryViewModel;
        public EntryViewModel SelectedEntryViewModel
        {
            get { return _selectedEntryViewModel;}
            set
            {
                _selectedEntryViewModel = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EntryDate));
            }
        }
        
        public DateTime EntryDate
        {
            get => _selectedEntryViewModel?.Date ?? DateTime.Now.Date;

            set => GoToDate(value);
        }

        public  ICommand SaveCommand => new AsyncCommand(Save);
        public ICommand RefreshCommand => new AsyncCommand(Refresh);
        public DelegateCommand ForwardCommand => new DelegateCommand(Forward);
        public DelegateCommand BackCommand => new DelegateCommand(Back);
        public DelegateCommand ClearCommand => new DelegateCommand(Clear);
        public ICommand DeleteCommand => new AsyncCommand(Delete);

        public MainViewModel(ApiWrapper api)
        {
            _api = api;
        }

        public async Task Start()
        {
            try
            {
                await Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException != null ? e.InnerException.Message : e.Message, "Food Diary",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
            
        }

        public void Forward()
        {
            GoToDate(SelectedEntryViewModel.Date.AddDays(1));                               
        }

        public void Back()
        {
            GoToDate(SelectedEntryViewModel.Date.AddDays(-1));
        }

        public void GoToDate(DateTime dt)
        {
            var shortDate = dt.Date;

            var entry = Entries.FirstOrDefault(x => DateTime.Compare(x.Date.Date, shortDate) == 0);

            if (entry == null)
            {
                SelectedEntryViewModel = new EntryViewModel(shortDate);
                Entries.Add(SelectedEntryViewModel);
            }
            else
            {
                SelectedEntryViewModel = entry;
            }
        }

        public async Task Refresh()
        {
            Entries.Clear();

            foreach (var e in await _api.GetEntries())
                Entries.Add(e);

            if (Entries.Count > 0)
            {
                SelectedEntryViewModel = Entries.OrderByDescending(x => x.Date).First();
            }
        }

        public async Task Save()
        {
            try
            {
                var updatedEntries = Entries.Where(x => x.Updated);

                foreach (var entry in updatedEntries)
                {
                    await _api.Save(entry);
                    entry.Updated = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    string.Format("Error saving record {0}{1}{2}", Environment.NewLine,
                        Environment.NewLine, e.Message), "Save Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            
        }

        public async Task Delete()
        {
            try
            {
                var toDeleteEntries = Entries.Where(x => x.ToDelete);

                foreach (var entry in toDeleteEntries)
                {
                    await _api.Delete(entry);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    string.Format("Error deleting record {0}{1}{2}", Environment.NewLine,
                        Environment.NewLine, e.Message), "Delete Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            
        }

        public void Clear()
        {
            SelectedEntryViewModel = new EntryViewModel(SelectedEntryViewModel.Date);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
