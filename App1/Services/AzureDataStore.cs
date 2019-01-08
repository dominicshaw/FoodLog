using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App1.Models;
using FoodLog.DTOs;

namespace App1.Services
{
    public class AzureDataStore : IDataStore<EntryDTO>
    {
        HttpClient client;
        IEnumerable<EntryDTO> items;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            items = new List<EntryDTO>();
        }

        public async Task<IEnumerable<EntryDTO>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var json = await client.GetStringAsync($"api/entries");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<EntryDTO>>(json));
            }

            return items;
        }

        public async Task<EntryDTO> GetItemAsync(string id)
        {
            if (id != null)
            {
                var json = await client.GetStringAsync($"api/entries/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<EntryDTO>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(EntryDTO entries)
        {
            if (entries == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(entries);

            var response = await client.PostAsync($"api/entries", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(EntryDTO entries)
        {
            if (entries == null)
                return false;

            var serializedItem = JsonConvert.SerializeObject(entries);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/entries/{entries.EntryId}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            var response = await client.DeleteAsync($"api/entries/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}