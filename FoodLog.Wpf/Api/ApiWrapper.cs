using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            return response.Body.Select(c => EntryMapper.Map(c, new EntryViewModel(),e=>e.Updated=false)).ToList();
        }

        public async Task Save(EntryViewModel entryViewModel)
        {            

            if (entryViewModel.EntryId == 0)
            {
                var response = await _client.PostEntryWithHttpMessagesAsync(EntryMapper.Map(entryViewModel, new EntryDTO()));
                
                if (!response.Response.IsSuccessStatusCode)
                    throw new ApiException(response.Response.StatusCode);

                entryViewModel.EntryId = response.Body.EntryId;

            }
            else
            {
                var response = await _client.PutEntryWithHttpMessagesAsync(entryViewModel.EntryId, EntryMapper.Map(entryViewModel, new EntryDTO()));

                if (!response.Response.IsSuccessStatusCode)
                    throw new ApiException(response.Response.StatusCode);
            }
            
        }

        public async Task Delete(EntryViewModel entryViewModel)
        {            

            if (entryViewModel.EntryId != 0)
            {
                var response = await _client.DeleteEntryWithHttpMessagesAsync(entryViewModel.EntryId);

                if (!response.Response.IsSuccessStatusCode)
                    throw new ApiException(response.Response.StatusCode);
            }
            
            
        }
    }
}
