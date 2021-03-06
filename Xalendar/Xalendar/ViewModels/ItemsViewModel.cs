﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Xalendar.Models;
using Xalendar.Views;

namespace Xalendar.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Event> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public Array TypesEvent { get; set; }
        public DateTime Date { get; set; }
        public ItemsViewModel()
        {
            Date = DateTime.Now;
            TypesEvent = Enum.GetValues(typeof(TypeEvent));
            Title = "Event";
            Items = new ObservableCollection<Event>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            //DataStore.DeleteAll();
            MessagingCenter.Subscribe<NewItemPage, Event>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Event;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);

                INotification notification = DependencyService.Get<INotification>();
                if (notification != null)
                {
                    notification.Show(newItem.Id, newItem.Title, newItem.TypeEvt.ToString(), newItem.Date);
                }
            });
        }

        async internal void Search(DateTime date, TypeEvent? selectedItem)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var items = await DataStore.SearchAsync(date, selectedItem);
                Items.Clear();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.SearchAsync(DateTime.Now,null);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal async void Delete(Event model)
        {
            Items.Remove(model);
            await DataStore.DeleteItemAsync(model.Id);
            INotification notification = DependencyService.Get<INotification>();
            if (notification != null)
            {
                notification.Cancel(model.Id);
            }
        }
    }
}