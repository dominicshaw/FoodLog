using System;

using App1.Models;
using FoodLog.Common;
using FoodLog.DTOs;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public EntryViewModel Item { get; set; }
        public ItemDetailViewModel(EntryViewModel item = null)
        {            
            Title = item?.Date.ToLongDateString();            
            
            Item = item;
        }
    }
}
