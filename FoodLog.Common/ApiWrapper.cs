using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoodLog.DTOs;
using Newtonsoft.Json;

namespace FoodLog.Common
{
    public class ApiWrapper : IApiWrapper
    {
        private static readonly HttpClient _client = new HttpClient();

        private const string _server = "http://lonhapp02.ttint.com:5000";

        List<EntryViewModel> items;

        public ApiWrapper()
        {
            items = new List<EntryViewModel>();
        }
        public async Task <bool> Delete(EntryViewModel entryViewModel)
        {
            if (entryViewModel.EntryId==0)
                return false;

            var response = await _client.DeleteAsync($"{_server}/api/entries/{entryViewModel.EntryId}");

            return response.IsSuccessStatusCode;

        }

        public async Task<IList<EntryViewModel>> GetEntries(bool forceRefresh = false)
        {
            var uri = $"{_server}/api/entries";

            var result = await Get<List<EntryDTO>>(uri, CancellationToken.None);

            return result.Select(c => EntryMapper.Map(c, new EntryViewModel(), e => e.Updated = false)).ToList();

            //if (forceRefresh||items == null||!items.Any())
            //{
            //    var json = await _client.GetStringAsync($"{_server}/api/entries");

            //    var entryDtos = JsonConvert.DeserializeObject<List<EntryDTO>>(json);

            //    return entryDtos.Select(c => EntryMapper.Map(c, new EntryViewModel(), e => e.Updated = false)).ToList();
            //}

            //return items;
        }
        private static async Task<T> Get<T>(string uri, CancellationToken token)
        {
            var response = await _client.GetAsync(uri, token);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
            }

            Report(response.StatusCode + ": " + uri);

            return default(T);
        }

        public async Task<bool> Save(EntryViewModel entryViewModel)
        {
            var serializedItem = JsonConvert.SerializeObject(EntryMapper.Map(entryViewModel, new EntryDTO()));

            if (entryViewModel.EntryId == 0)               
            {                
                var response = await _client.PostAsync($"{_server}/api/entries", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
            else
            {                                
                var response = await _client.PutAsync($"{_server}/api/entries/{entryViewModel.EntryId}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

                return response.IsSuccessStatusCode;
            }
        }

        private static void Report(string message, [CallerMemberName] string propertyName = null)
        {
            Messenger.Instance.NotifyColleagues("Exception", new Exception("WebAPI Exception: " + message + "; from " + propertyName));
        }
    }
}
