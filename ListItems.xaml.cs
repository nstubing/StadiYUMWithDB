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

        public ListItems(Concession thisConscession)
        {
            InitializeComponent();

            Items =new ObservableCollection<Item>( thisConscession.Items);
           
			
			MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var itemTapped = (Item)MyListView.SelectedItem;

            await DisplayAlert("Item Tapped", itemTapped.Name, "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
