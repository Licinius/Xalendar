using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xalendar.Models;

namespace Xalendar.Services
{
    class EventDatabase : SQLiteAsyncConnection, IDataStore<Event>
    {
        static EventDatabase database;


        public static EventDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new EventDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EventSQLite.db3"));
                }
                return database;
            }
        }

        /**
         *  Construteur privé afin de générer le singleton 
         **/
        private EventDatabase(string path) : base(path)
        {
            this.CreateTableAsync<Event>().Wait();
        }

        public async Task<int> AddItemAsync(Event Event)
        {

            return await Database.InsertAsync(Event);

        }

        public async Task<int> UpdateItemAsync(Event Event)
        {
            return await Database.UpdateAsync(Event);
        }

        public async Task<int> DeleteItemAsync(string id)
        {
            return await Database.DeleteAsync(id);
        }

        public async Task<Event> GetItemAsync(string id)
        {
            return await Database.GetAsync<Event>(id);
        }

        public async Task<IEnumerable<Event>> SearchAsync(DateTime date, TypeEvent? type)
        {
            if (type != null)
            {
                var events= from e in Database.Table<Event>()
                             where e.Date > date
                             where e.TypeEvt == type
                             select e;
                return await events.ToListAsync();
            }
            else
            {
               var events = from e in Database.Table<Event>()
                             where e.Date > date
                             select e;
                return await events.ToListAsync();
            }
  
         
            
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Database.Table<Event>().ToListAsync();
        }
    }
}
