using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xalendar.Models;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;
using Xalendar.Services;
using Plugin.Permissions.Abstractions;
using System.Diagnostics;
using Plugin.Media;
using System.IO;
using Plugin.LocalNotifications;
using Xamarin.Essentials;

namespace Xalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Event Item { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public Array TypesEvent { get; set; }

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

        async void Take_Picture(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Events",
                    Name = $"{DateTime.UtcNow}.jpg"
                };

                // Take a photo of the business receipt.
                var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                if (file != null)
                {
                    Item.Photo = Utils.ReadStream(file.GetStream());
                }
                PhotoImage.Source = ImageSource.FromStream(() => { return new MemoryStream(Item.Photo); });
            }

        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            Item.Date = new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds, Time.Milliseconds);
            MessagingCenter.Send(this, "AddItem", Item);
            Item.TypeEvt = (TypeEvent) PickerEvent.SelectedItem;
            INotification notification = DependencyService.Get<INotification>();
            if(notification != null)
            {
                notification.Show(Item.Title, Item.TypeEvt + " [" + Item.Date.ToString("dd/MM/yyyy") + "]",Item.TypeEvt.ToString(),Item.Date);
            }
             await Navigation.PopModalAsync();
        }

        async void Geolocate(object sender, EventArgs e)
        {
            try { 
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                    return;

                ((Button)sender).IsEnabled = false;
                var locator = CrossGeolocator.Current;

                ((Button)sender).Text = "Getting gps...";
                locator.DesiredAccuracy = 100;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null, true);

                if (position == null)
                {
                    ((Button)sender).Text = "We can't find you, try again :(";
                    ((Button)sender).IsEnabled = true;
                    return;
                }
                else
                {
                    ((Button)sender).Text = string.Format("Lat: {0} Long: {1}",position.Latitude, position.Longitude);
                    Item.Latitude = position.Latitude;
                    Item.Longitude = position.Longitude;
                    ((Button)sender).IsEnabled = false;
                    return;
                }
               

            }
			catch (Exception ex)
			{
                Debug.Write(ex.ToString());
				await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
                ((Button)sender).Text = "Click on me to know your location";
                ((Button)sender).IsEnabled = true;
            }

        }

    }


}