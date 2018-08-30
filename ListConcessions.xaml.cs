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
    public partial class ListConcessions : ContentPage
    {
        public ObservableCollection<Concession> Items { get; set; }
        QueryManager manager;

        public ListConcessions()
        {
            InitializeComponent();
            manager = new QueryManager();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetConcessionList();
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var thisItem = (Concession)MyListView.SelectedItem;
            await Navigation.PushAsync(new ListItems(thisItem));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
        public async Task GetConcessionList()
        {
            var Concesssions =await manager.GetConcessions();
            Items = new ObservableCollection<Concession>(Concesssions);
            MyListView.ItemsSource = Items;
        }
    }
}
