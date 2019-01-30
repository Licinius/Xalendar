using Plugin.DeviceSensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xalendar.Models;
using Xalendar.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SportPage : ContentPage
	{
        int LastReading;
        Boolean FirstTime =true;
        public Event Item { get; private set; }

        public SportPage (int id)
		{
			InitializeComponent();
            if(id != -1)
            {
                InitEvent(id);
            }
            else
            {
                ((Label)titre).Text += "le Sport ! ";
            }
            if (CrossDeviceSensors.Current.Pedometer.IsSupported)
            {
                CrossDeviceSensors.Current.Pedometer.OnReadingChanged += (s, a) =>
                {
                    if (FirstTime)
                    {
                        LastReading = a.Reading;
                        FirstTime = false;
                    }
                    label.Text = (a.Reading - LastReading).ToString();
                };
                LastReading = CrossDeviceSensors.Current.Pedometer.LastReading;
                CrossDeviceSensors.Current.Pedometer.StartReading();

            }
        }

        private async void InitEvent(int id)
        {
            Item = await EventDatabase.Database.GetItemAsync(id);
            ((Label)titre).Text += Item.Title;
        }

        public async void Stop(object sender, EventArgs e)
        {
            CrossDeviceSensors.Current.Pedometer.StopReading();
            Button button = (Button)sender;
            Item.Pas = Int32.Parse(label.Text);
            await EventDatabase.Database.UpdateAsync(Item);
            button.Text = "Quit";
            button.Clicked += (s1,e1) =>
            {
                Application.Current.MainPage = new MainPage();

            };

        }




    }
}