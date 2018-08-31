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
	public partial class AdminHome : ContentPage
	{
        QueryManager manager;
		public AdminHome ()
		{
			InitializeComponent ();
            manager = new QueryManager();
            GetButton();
		}
        public void GetButton()
        {
            if(App.CurrentStadium.IsOpen==0)
            {
                OrderToggle2.Text = "Allow Orders";
                OrderToggle2.BackgroundColor = Xamarin.Forms.Color.Green;
            }
            else
            {
                OrderToggle2.Text = "End Orders";
                OrderToggle2.BackgroundColor = Xamarin.Forms.Color.Red;
            }

        }
        public void OrderToggle()
        {
            manager.GetOrderToggle();
            GetButton();
        }
        public void View_Sales()
        {

        }
	}
}