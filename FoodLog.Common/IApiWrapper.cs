using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodLog.Common
{
    public interface IApiWrapper
    {
        Task<bool> Delete(EntryViewModel entryViewModel);
        Task<IList<EntryViewModel>> GetEntries(bool forcerefresh = false);
        Task<bool> Save(EntryViewModel entryViewModel);
    }
}