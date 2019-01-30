using System;
using FoodLog.DTOs;

namespace FoodLog.Common
{
    public class EntryViewModel : IEntry
    {
        
        public bool Dairy { get; set; }
        public DateTime Date { get; set; }        
        public string DateString => Date.ToLongDateString();


        public string Breakfast
        {
            get => _breakfast;
            set
            {
                if (value == _breakfast) return;
                _breakfast = value;
                Updated = true;                
            }
        }

        private string _lunch;
        public string Lunch
        {
            get => _lunch;
            set
            {
                if (value == _lunch) return;
                _lunch = value;
                Updated = true;
            }
        }
        private string _dinner;
        public string Dinner
        {
            get => _dinner;
            set
            {
                if (value == _dinner) return;
                _dinner = value;
                Updated = true;
            }
        }
        private string _snacksDrinks;
        public string SnacksDrinks
        {
            get => _snacksDrinks;
            set
            {
                if (value == _snacksDrinks) return;
                _snacksDrinks = value;
                Updated = true;
            }
        }

        private bool _gluten;
        public bool Gluten { get => _gluten;
            set
            {
                if (value == _gluten) return;
                _gluten = value;
                Updated = true;
            }
        }
        public int EntryId { get; set; }
        private int _rating;
        public int Rating { get => _rating;
            set
            {
                if (value == _rating) return;
                _rating = value;
                Updated = true;
            }
        }
        public bool Updated { get; set; }
        public bool ToDelete { get; set; }
        private bool _alcohol;
        public bool Alcohol { get => _alcohol;
            set
            {
                if (value == _alcohol) return;
                _alcohol = value;
                Updated = true;
            }
        }

        private bool _caffeine;
        public bool Caffeine { get => _caffeine;
            set
            {
                if (value == _caffeine) return;
                _caffeine = value;
                Updated = true;
            }
        }

        private bool _fattyFood;
        public bool FattyFood { get => _fattyFood;
            set
            {
                if (value == _fattyFood) return;
                _fattyFood = value;
                Updated = true;
            }
        }

        private bool _spice;
        public bool Spice { get => _spice;
            set
            {
                if (value == _spice) return;
                _spice = value;
                Updated = true;
            }
        }

        private bool _onionsPulses;
        public bool OnionsPulses { get => _onionsPulses;
            set
            {
                if (value == _onionsPulses) return;
                _onionsPulses = value;
                Updated = true;
            }
        }

        private bool _exercise;
        private string _breakfast;

        public bool Exercise { get => _exercise;
            set
            {
                if (value == _exercise) return;
                _exercise = value;
                Updated = true;
            }
        }
        public override string ToString()
        {
            return Date.ToLongDateString();
        }

        public EntryViewModel(DateTime dt)
        {
            Date = dt.Date;
        }

        public EntryViewModel()
        {

        }


    }
}
