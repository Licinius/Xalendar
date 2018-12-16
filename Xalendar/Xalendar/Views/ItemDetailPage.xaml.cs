using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xalendar.Models;
using Xalendar.ViewModels;

namespace Xalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Event
            {
                Title = "I AM ERROR",
                Description = "GO AWAY !"
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}