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

        private void Button_Clicked(object sender, EventArgs e)
        {
            User newLogin = new User() { Username = UsernameInput.Text};
            newLogin.Password = PasswordInput.Text;
            bool nextPage = manager.LoginEntry(newLogin);
            if(nextPage)
            {
                Navigation.PushAsync(new SeatInput());
            }
            else
            {
                DisplayAlert("Invalid Password", "Your username and password may be incorrect.", "Try again");

            }
        }
    }
}