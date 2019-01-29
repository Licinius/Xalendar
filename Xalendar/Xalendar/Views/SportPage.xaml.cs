using Plugin.DeviceSensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SportPage : ContentPage
	{
        int LastReading;
        Boolean FirstTime =true;
		public SportPage ()
		{
			InitializeComponent();
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

        public async void Stop(object sender, EventArgs e)
        {
            CrossDeviceSensors.Current.Pedometer.StopReading();
            Button button = (Button)sender;
            button.Text = "Quit";
            button.Clicked += (s1,e1) =>
            {
                Application.Current.MainPage = new MainPage();

            };

        }




    }
}