using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xalendar.Models;
using System.Diagnostics;

namespace Xalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Event Item { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Array TypesEvent{ get; set; }
      
        public NewItemPage()
        {
            InitializeComponent();
            Time = DateTime.Now.TimeOfDay;
            Date = DateTime.Now;
            TypesEvent = Enum.GetValues(typeof(TypeEvent));
            Item = new Event
            {
                Title = "Event Title",
                Description = "Note",
                TypeEvt = TypeEvent.Sport
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            Item.Date = new DateTime(Date.Year, Date.Month, Date.Minute, Time.Hours, Time.Minutes, Time.Seconds, Time.Milliseconds, Date.Kind);
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}