using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xalendar.Models;

namespace Xalendar.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T Event);
        Task<int> UpdateItemAsync(T Event);
        Task<int> DeleteItemAsync(string id);
        Task<int> DeleteAll();
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> SearchAsync(DateTime date,TypeEvent? type);
    }
}
