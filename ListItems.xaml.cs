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
    public partial class ListItems : ContentPage
    {
        public ObservableCollection<Item> Items { get; set; }
        QueryManager manager;
        public ListItems(Concession thisConscession)
        {
            InitializeComponent();

            Items =new ObservableCollection<Item>( thisConscession.Items);
            manager = new QueryManager();
			
			MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemTapped = (Item)MyListView.SelectedItem;
            await manager.AddItemToCart(itemTapped);
            await DisplayAlert(itemTapped.Name, "Has been added to your cart", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
