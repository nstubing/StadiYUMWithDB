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
    public partial class MainMenuMaster : ContentPage
    {
        public ListView ListView;

        public MainMenuMaster()
        {
            InitializeComponent();

            BindingContext = new MainMenuMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainMenuMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainMenuMenuItem> MenuItems { get; set; }
            
            public MainMenuMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainMenuMenuItem>(new[]
                {
                    new MainMenuMenuItem { Id = 0, Title = "Concessions" },
                    new MainMenuMenuItem { Id = 1, Title = "Cart" },
                    new MainMenuMenuItem { Id = 2, Title = "Orders" },
                    new MainMenuMenuItem { Id = 3, Title = "Account" },
                    new MainMenuMenuItem { Id = 4, Title = "Log Out" },
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