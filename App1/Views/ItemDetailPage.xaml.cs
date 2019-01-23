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

        
    }
}