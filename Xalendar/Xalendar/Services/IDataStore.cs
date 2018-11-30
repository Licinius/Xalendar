using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xalendar.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T Event);
        Task<bool> UpdateItemAsync(T Event);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
