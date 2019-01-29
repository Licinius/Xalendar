using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xalendar.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xalendar
{
    public partial class App : Application
    {

        public App(Boolean sport=false)
        {
            InitializeComponent();
            if (!sport)
                MainPage = new MainPage();
            else
                MainPage = new SportPage();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
