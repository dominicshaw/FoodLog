using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FoodLog.Common.Annotations;
using FoodLog.DTOs;

namespace FoodLog.Common
{
    public class EntryViewModel : IEntry, INotifyPropertyChanged
    {
        
        public bool Dairy { get; set; }
        public DateTime Date { get; set; }        
        public string DateString => Date.ToLongDateString();

        private string _breakfast;
        public string Breakfast
        {
            get => _breakfast;
            set
            {
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
                _snacksDrinks = value;
                Updated = true;
            }
        }

        private bool _gluten;
        public bool Gluten { get => _gluten;
            set { _gluten = value;
                Updated = true;
            }
        }
        public int EntryId { get; set; }
        private int _rating;
        public int Rating { get => _rating;
            set { _rating = value;
                Updated = true;
            }
        }
        public bool Updated { get; set; }
        public bool ToDelete { get; set; }
        private bool _alcohol;
        public bool Alcohol { get => _alcohol;
            set { _alcohol = value;
                Updated = true;
            }
        }

        private bool _caffeine;
        public bool Caffeine { get => _caffeine;
            set { _caffeine = value;
                Updated = true;
            }
        }

        private bool _fattyFood;
        public bool FattyFood { get => _fattyFood;
            set { _fattyFood = value;
                Updated = true;
            }
        }

        private bool _spice;
        public bool Spice { get => _spice;
            set { _spice = value;
                Updated = true;
            }
        }

        private bool _onionsPulses;
        public bool OnionsPulses { get => _onionsPulses;
            set { _onionsPulses = value;
                Updated = true;
            }
        }

        private bool _exercise;
        public bool Exercise { get => _exercise;
            set { _exercise = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
