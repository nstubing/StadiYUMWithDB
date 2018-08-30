using DocumentDBTodo.Models;
using Xamarin.Forms;

namespace DocumentDBTodo
{
	public partial class App : Application
	{
        public static User currentUser { get; set; }
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new Login());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
