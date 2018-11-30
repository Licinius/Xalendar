using System;

using Xalendar.Models;

namespace Xalendar.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Event Item { get; set; }
        public ItemDetailViewModel(Event item = null)
        {
            Title = item?.Title;
            Item = item;
        }
    }
}
