using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentDBTodo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : MasterDetailPage
    {
        public MainMenu()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainMenuMenuItem;
            if (item == null)
                return;

            string newPageTitle = item.Title;
            if (newPageTitle == "Concessions")
            {
                Detail= new NavigationPage(new ListConcessions());

            }
            else if (newPageTitle == "Cart")
            {

                Detail= new NavigationPage(new ListCart());
            }
            else if (newPageTitle == "Orders")
            {
                Detail= new NavigationPage(new ListOrders());
            }
            else if (newPageTitle == "Account")
            {
                Detail=new NavigationPage(new AccountPage());
            }
            else if (newPageTitle == "Log Out")
            {
                Application.Current.MainPage = new NavigationPage(new Login());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new Login());
            }
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}