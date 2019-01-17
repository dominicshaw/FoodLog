using System;
using System.ComponentModel.DataAnnotations;
using FoodLog.DTOs;

namespace FoodLog.Api.Models
{
    public class Entry : IEntry
    {
        [Key]
        public int EntryId { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(200)]
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }
        public string SnacksDrinks { get; set; }
        public bool Dairy { get; set; }
        public bool Gluten { get; set; }
        public int Rating { get; set; }
        public bool Alcohol { get; set; }
        public bool Caffeine { get; set; }
        public bool FattyFood { get; set; }
        public bool Spice { get; set; }
        public bool OnionsPulses { get; set; }
        public bool Exercise { get; set; }
    }
}
