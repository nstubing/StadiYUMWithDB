using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentDBTodo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentDBTodo
{
    public partial class Login : ContentPage
    {
        QueryManager manager;
        public Login()
        {
            InitializeComponent();
            manager = new QueryManager(); 
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            User newLogin = new User() { Username = UsernameInput.Text};
            newLogin.Password = PasswordInput.Text;
            bool nextPage = await manager.LoginEntry(newLogin);
            if(nextPage&&App.currentUser.IsEmployee==0)
            {
                if(App.CurrentStadium.IsOpen==0)
                {
                    DisplayAlert("Orders are closed", "Please wait until the stadium has enabled orders", "OK");
                }
                else
                {
                    Navigation.PushAsync(new SeatInput());
                }
            }
            else if (nextPage && App.currentUser.IsEmployee == 1)
            {
                    Application.Current.MainPage = new LogoutEmployee();
            }
            else if (nextPage && App.currentUser.IsEmployee == 2)
            {
                Application.Current.MainPage = new Logout();
            }
            else
            {
                DisplayAlert("Invalid Password", "Your username and password may be incorrect.", "Try again");

            }
        }
    }
}