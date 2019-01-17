using System;

namespace FoodLog.DTOs
{
    public interface IEntry
    {
        int EntryId { get; set; }
        bool Dairy { get; set; }
        DateTime Date { get; set; }
        string Breakfast { get; set; }
        string Lunch { get; set; }
        string Dinner { get; set; }
        string SnacksDrinks { get; set; }
        bool Gluten { get; set; }
        int Rating { get; set; }
        bool Alcohol { get; set; }
        bool Caffeine { get; set; }
        bool FattyFood { get; set; }
        bool Spice { get; set; }
        bool OnionsPulses { get; set; }
        bool Exercise { get; set; }
    }
    
}