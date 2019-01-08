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
        [DataMember(Name = "dscr")]
        public string Description { get; set; }
        [DataMember(Name = "da")]
        public bool Dairy { get; set; }
        [DataMember(Name = "gl")]
        public bool Gluten { get; set; }
        [DataMember(Name = "r")]
        public int Rating { get; set; }
    }
}
