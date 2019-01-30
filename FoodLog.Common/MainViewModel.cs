using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FoodLog.Common.Annotations;


namespace FoodLog.Common
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

        public ICommand SaveCommand => new AsyncCommand(Save);
        public ICommand RefreshCommand => new AsyncCommand(Refresh);
        public ICommand ForwardCommand => new Command(Forward);
        public ICommand BackCommand => new Command(Back);
        public ICommand ClearCommand => new Command(Clear);
        public ICommand AddCommand => new Command(AddNew);
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
                throw e;
            }
        }

        public void AddNew()
        {
            GoToDate(Entries.OrderByDescending(x => x.Date).First().Date.AddDays(1));
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
                var updatedEntries = Entries.Where(x => x.Updated).ToList();

                foreach (var entry in updatedEntries)
                {
                    await _api.Save(entry);
                    entry.Updated = false;

                    if (!Entries.Contains(SelectedEntryViewModel))
                        Entries.Add(SelectedEntryViewModel);
                }
            }
            catch (Exception e)
            {
                throw e;
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
                throw e;
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
