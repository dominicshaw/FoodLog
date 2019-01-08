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
        public string Description { get; set; }
        public bool Dairy { get; set; }
        public bool Gluten { get; set; }
        public int Rating { get; set; }
    }
}
