using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App1.Models;
using FoodLog.DTOs;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public EntryDTO Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new EntryDTO
            {
                Date = DateTime.Now,
                Breakfast = "This is an item description.",
                Dairy = false,
                Gluten = true,
                Rating = 3
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}