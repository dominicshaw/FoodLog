using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App1.Models;
using App1.ViewModels;
using FoodLog.DTOs;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new EntryDTO
            {
                Date = DateTime.Now,
                Description = "This is an item description.",
                Dairy = false,
                Gluten = true,
                Rating = 3
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}