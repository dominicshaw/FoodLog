using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using App1.Models;
using App1.Views;
using FoodLog.DTOs;

namespace App1.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<EntryDTO> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<EntryDTO>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, EntryDTO>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as EntryDTO;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                items = items.OrderByDescending(x => x.Date);

                foreach (var item in items)
                {
                    Items.Add(item);
                }                

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}