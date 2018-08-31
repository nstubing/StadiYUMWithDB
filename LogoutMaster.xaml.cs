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
    public partial class LogoutMaster : ContentPage
    {
        public ListView ListView;

        public LogoutMaster()
        {
            InitializeComponent();

            BindingContext = new LogoutMasterViewModel();
            ListView = MenuItemsListView;
        }

        class LogoutMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<LogoutMenuItem> MenuItems { get; set; }
            
            public LogoutMasterViewModel()
            {
                MenuItems = new ObservableCollection<LogoutMenuItem>(new[]
                {
                    new LogoutMenuItem { Id = 0, Title = "Logout" },

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