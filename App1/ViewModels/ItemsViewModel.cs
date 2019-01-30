using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using App1.Models;
using App1.Views;
using FoodLog.Common;
using FoodLog.DTOs;
using Command = FoodLog.Common.Command;

namespace App1.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<EntryViewModel> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<EntryViewModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, EntryViewModel>(this, "AddItem", async (obj, item) =>
            {                
                Items.Add(item);
                await DataStore.Save(item);
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
                var items = await DataStore.GetEntries(true);                

                foreach (var item in items.OrderByDescending(x => x.Date))
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