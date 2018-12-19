using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xalendar.Models;
using Xalendar.Views;
using Xalendar.ViewModels;
using XLabs.Forms.Behaviors;

namespace Xalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        RelayGesture reset { get; set; }
          
        public ItemsPage()
        {
            InitializeComponent();
            reset = new RelayGesture((x,y) => { Picker.SelectedItem = null; });
            BindingContext = viewModel = new ItemsViewModel();
        }

        async void Search(object sender,EventArgs e)
        {
            if (Picker.SelectedItem != null)
                viewModel.Search(DatePicker.Date, (TypeEvent)Picker.SelectedItem);
            else
                viewModel.Search(DatePicker.Date,null);
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Event;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}