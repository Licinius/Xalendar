using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xalendar.Models;
using Xalendar.ViewModels;
using System.IO;

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
            PhotoImage.Source = ImageSource.FromStream(() => { return new MemoryStream(viewModel.Item.Photo); });

            if (viewModel.Item.Pas > 0)
            {

                Pas.Text = "Vous avez fait  " + viewModel.Item.Pas + " pas";

            }
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