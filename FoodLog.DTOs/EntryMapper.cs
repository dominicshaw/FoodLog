using System;

namespace FoodLog.DTOs
{
    public static class EntryMapper
    {
        public static T Map<T>(IEntry from, T to, Action<T> hook = null) where T : IEntry
        {
            to.EntryId          = from.EntryId;
            to.Date             = from.Date;
            to.Breakfast        = from.Breakfast;
            to.Lunch            = from.Lunch;
            to.Dinner           = from.Dinner;
            to.SnacksDrinks     = from.SnacksDrinks;
            to.Dairy            = from.Dairy;
            to.Gluten           = from.Gluten;
            to.Rating           = from.Rating;
            to.Alcohol          = from.Alcohol;
            to.Caffeine         = from.Caffeine;
            to.FattyFood        = from.FattyFood;
            to.Spice            = from.Spice;
            to.OnionsPulses     = from.OnionsPulses;
            to.Exercise         = from.Exercise;

            hook?.Invoke(to);

            return to;
        }
    }
}