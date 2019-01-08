using System;
using FoodLog.DTOs;

namespace FoodLog.Wpf.ViewModels
{
    public class EntryViewModel : IEntry
    {
        public bool Dairy { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool Gluten { get; set; }
        public int EntryId { get; set; }
        public int Rating { get; set; }
    }
}
