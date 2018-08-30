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
	public partial class SeatInput : ContentPage
	{
        QueryManager manager;
		public SeatInput ()
		{
			InitializeComponent ();
            manager = new QueryManager();
		}
        public async Task SeatEntered(object sender, EventArgs e)
        {
            App.currentUser.CurrentSection = Int32.Parse(SectionEntry.Text);
            App.currentUser.Seat = Int32.Parse(SeatEntry.Text);
            await manager.SeatEnteredAsync(App.currentUser);
            Application.Current.MainPage = new MainMenu();
        }
	}
}