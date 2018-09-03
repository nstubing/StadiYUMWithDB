using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DocumentDBTodo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentDBTodo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConcessionOrderDetails : ContentPage
    {
        QueryManager manager;
        public ObservableCollection<Item> Items { get; set; }
        public Order myOrder { get; set; }

        public ConcessionOrderDetails(Order order)
        {
            InitializeComponent();
            manager = new QueryManager();
            this.myOrder = order;
			MyListView.ItemsSource = order.Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        public async void CompleteOrder()
        {
            manager.CompleteOrder(myOrder);
            await DisplayAlert("Completed", "", "OK");
            await Navigation.PopAsync();
        }
    }
}
