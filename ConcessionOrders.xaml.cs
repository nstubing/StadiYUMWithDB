using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentDBTodo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentDBTodo
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConcessionOrders : ContentPage
	{
        QueryManager manager;
        public ObservableCollection<Order> Items { get; set; }
        public ConcessionOrders ()
		{
			InitializeComponent ();
            manager = new QueryManager();
            var Orders = manager.GetConcessionsOrders();
            Items = new ObservableCollection<Order>(Orders);
            MyListView.ItemsSource = Items;
		}
        public void Button_Clicked(Object sender, ItemTappedEventArgs e)
        {
            var CompletedOrder = (Order)MyListView.SelectedItem;
            manager.CompleteOrder(CompletedOrder);
            DisplayAlert("Completed", "", "OK");
        }      
	}
}