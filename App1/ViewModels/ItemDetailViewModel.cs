using System;

using App1.Models;
using FoodLog.DTOs;

namespace App1.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public EntryDTO Item { get; set; }
        public ItemDetailViewModel(EntryDTO item = null)
        {            
            Title = item?.Date.ToLongDateString();            
            
            Item = item;
        }
    }
}
