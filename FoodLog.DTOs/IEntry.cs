using System;

namespace FoodLog.DTOs
{
    public interface IEntry
    {
        int EntryId { get; set; }
        bool Dairy { get; set; }
        DateTime Date { get; set; }
        string Description { get; set; }
        bool Gluten { get; set; }
        int Rating { get; set; }
    }
}