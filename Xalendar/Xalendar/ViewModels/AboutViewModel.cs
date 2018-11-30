using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Xalendar.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Notification";
 
        }

        public ICommand OpenWebCommand { get; }
    }
}