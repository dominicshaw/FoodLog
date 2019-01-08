namespace FoodLog.DTOs
{
    public static class EntryMapper
    {
        public static T Map<T>(IEntry from, T to) where T : IEntry
        {
            to.EntryId          = from.EntryId;
            to.Date        = from.Date;
            to.Description = from.Description;
            to.Dairy       = from.Dairy;
            to.Gluten      = from.Gluten;
            to.Rating      = from.Rating;

            return to;
        }
    }
}