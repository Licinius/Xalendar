using Plugin.Toast;
using System;
using System.Diagnostics;
using Xalendar.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace Xalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            foreach (TypeEvent TypeEvt in Enum.GetValues(typeof(TypeEvent)))
            {
                this.FindByName<CheckBox>(TypeEvt.ToString()).Checked = Preferences.Get(TypeEvt.ToString(), false);
            }
        }

        async void Save_Clicked(Object sender, EventArgs args)
        {
            foreach (TypeEvent TypeEvt in Enum.GetValues(typeof(TypeEvent)))
            {
                Preferences.Set(TypeEvt.ToString(), this.FindByName<CheckBox>(TypeEvt.ToString()).Checked);
            }
            CrossToastPopUp.Current.ShowToastMessage("Preferences Saved !");
        }
    }
}