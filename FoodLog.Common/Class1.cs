using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FoodLog.DTOs;
using Newtonsoft.Json;

namespace FoodLog.Common
{
    public class ApiWrapper : IApiWrapper
    {
        HttpClient client;
        List<EntryViewModel> items;
        public ApiWrapper()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://lonhapp02:5000");

            items = new List<EntryViewModel>();
        }
        public async Task <bool> Delete(EntryViewModel entryViewModel)
        {
            if (entryViewModel.EntryId==0)
                return false;

            var response = await client.DeleteAsync($"api/entries/{entryViewModel.EntryId}");

            return response.IsSuccessStatusCode;

        }

        public async Task<IList<EntryViewModel>> GetEntries(bool forceRefresh = false)
        {
            if (forceRefresh||items == null||!items.Any())
            {
                var json = await client.GetStringAsync($"api/entries");

                var entryDtos = await Task.Run(() => JsonConvert.DeserializeObject<List<EntryDTO>>(json));

                return entryDtos.Select(c => EntryMapper.Map(c, new EntryViewModel(), e => e.Updated = false)).ToList();
            }

            return items;
        }

        public async Task<bool> Save(EntryViewModel entryViewModel)
        {
            var serializedItem = JsonConvert.SerializeObject(EntryMapper.Map(entryViewModel, new EntryDTO()));

            if (entryViewModel.EntryId == 0)               
            {                
                var response = await client.PostAsync($"api/entries", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            else
            {                                
                var response = await client.PutAsync($"api/entries/{entryViewModel.EntryId}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
        }
    }
}
