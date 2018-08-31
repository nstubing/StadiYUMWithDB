using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DocumentDBTodo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutEmployeeMaster : ContentPage
    {
        public ListView ListView;

        public LogoutEmployeeMaster()
        {
            InitializeComponent();

            BindingContext = new LogoutEmployeeMasterViewModel();
            ListView = MenuItemsListView;
        }

        class LogoutEmployeeMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<LogoutEmployeeMenuItem> MenuItems { get; set; }
            
            public LogoutEmployeeMasterViewModel()
            {
                MenuItems = new ObservableCollection<LogoutEmployeeMenuItem>(new[]
                {
                    new LogoutEmployeeMenuItem { Id = 0, Title = "Logout" },

                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}