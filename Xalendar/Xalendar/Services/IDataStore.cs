using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xalendar.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T Event);
        Task<int> UpdateItemAsync(T Event);
        Task<int> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
