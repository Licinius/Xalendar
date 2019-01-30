using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xalendar.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xalendar
{
    public partial class App : Application
    {

        public App(Boolean sport=false, int id=-1)
        {
            InitializeComponent();
            if (!sport)
                MainPage = new MainPage();
            else
                MainPage = new SportPage(id);
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
