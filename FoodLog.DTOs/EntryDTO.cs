using System;
using System.Runtime.Serialization;

namespace FoodLog.DTOs
{
    public class EntryDTO : IEntry
    {
        [DataMember(Name = "id")]
        public int EntryId { get; set; }
        [DataMember(Name = "dt")]
        public DateTime Date { get; set; }
        [DataMember(Name = "bk")]
        public string Breakfast { get; set; }
        [DataMember(Name = "lnch")]
        public string Lunch { get; set; }
        [DataMember(Name = "dnr")]
        public string Dinner { get; set; }
        [DataMember(Name = "sd")]
        public string SnacksDrinks { get; set; }
        [DataMember(Name = "da")]
        public bool Dairy { get; set; }
        [DataMember(Name = "gl")]
        public bool Gluten { get; set; }
        [DataMember(Name = "r")]
        public int Rating { get; set; }
        [DataMember(Name = "al")]
        public bool Alcohol { get; set; }
        [DataMember(Name = "ca")]
        public bool Caffeine { get; set; }
        [DataMember(Name = "ff")]
        public bool FattyFood { get; set; }
        [DataMember(Name = "sp")]
        public bool Spice { get; set; }
        [DataMember(Name = "op")]
        public bool OnionsPulses { get; set; }
        [DataMember(Name = "ex")]
        public bool Exercise { get; set; }
    }
}
