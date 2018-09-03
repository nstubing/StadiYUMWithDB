using DocumentDBTodo.Models;
using Xamarin.Forms;
using Plugin.Geolocator;
using Microsoft.Azure.Documents.Spatial;
using System;

namespace DocumentDBTodo
{
	public partial class App : Application
	{
        public static User currentUser { get; set; }
        public static Stadium CurrentStadium { get; set; }
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new Login());
		}

		protected override async void OnStart ()
        {
            QueryManager manager = new QueryManager();
            manager.Stadiums();
            var locator = CrossGeolocator.Current;
            var isAllowed = locator.IsGeolocationAvailable;
            var isEnabled = locator.IsGeolocationEnabled;
            var position = locator.GetPositionAsync();
            var myPosition= await position;
            Microsoft.Azure.Documents.Spatial.Point myPoint = new Microsoft.Azure.Documents.Spatial.Point(myPosition.Latitude, myPosition.Longitude);
            var stadium = manager.GetClosestStadium(myPoint);
            //CurrentStadium = stadium;
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
