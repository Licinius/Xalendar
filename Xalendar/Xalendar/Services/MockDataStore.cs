using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xalendar.Models;

namespace Xalendar.Services
{
    public class MockDataStore : IDataStore<Event>
    {
        List<Event> items;

        public MockDataStore()
        {
            items = new List<Event>();
            var mockItems = new List<Event>
            {
                new Event { Id = Guid.NewGuid().ToString(), Title = "Faire les courses", Description="Pense à faire les courses sinon tu ne manges pas " }
               
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Event item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Event item)
        {
            var oldItem = items.Where((Event arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Event arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}