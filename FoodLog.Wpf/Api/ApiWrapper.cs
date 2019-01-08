using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodLog.DTOs;
using FoodLog.Wpf.FoodLogClient;
using FoodLog.Wpf.ViewModels;

namespace FoodLog.Wpf.Api
{
    public class ApiWrapper
    {
        private readonly IFoodLogClient _client;

        public ApiWrapper(IFoodLogClient client)
        {
            _client = client;
        }

        public async Task<IList<EntryViewModel>> GetEntries()
        {
            var response = await _client.GetEntriesWithHttpMessagesAsync();

            if (!response.Response.IsSuccessStatusCode)
                throw new ApiException(response.Response.StatusCode);

            return response.Body.Select(c => EntryMapper.Map(c, new EntryViewModel())).ToList();
        }
    }
}
