using Movies.Models;
using Movies.Services.Navigation;
using Movies.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Movies.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<MenuItem> _menuItems;

        private INavigationService _navigationService;

        public MenuViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ObservableCollection<MenuItem> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        public ICommand ItemSelectedCommand => new Xamarin.Forms.Command<MenuItem>(SelectMenuItem);

        public override Task InitializeAsync(object navigationData)
        {
            InitMenuItems();

            return base.InitializeAsync(navigationData);
        }

        private void InitMenuItems()
        {
            MenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Title = "Discover",
                    MenuItemType = MenuItemType.Discover,
                    ViewModelType = typeof(HomeViewModel),
                    IsEnabled = true
                },
                new MenuItem
                {
                    Title = "Upcoming",
                    MenuItemType = MenuItemType.Upcoming,
                    ViewModelType = typeof(UpcomingViewModel),
                    IsEnabled = true
                },
                new MenuItem
                {
                    Title = "Action",
                    MenuItemType = MenuItemType.Action,
                    ViewModelType = typeof(ActionViewModel),
                    IsEnabled = true
                },
                new MenuItem
                {
                    Title = "Animation",
                    MenuItemType = MenuItemType.Animation,
                    ViewModelType = typeof(AnimationViewModel),
                    IsEnabled = true
                },
                new MenuItem
                {
                    Title = "Adventure",
                    MenuItemType = MenuItemType.Adventure,
                    ViewModelType = typeof(AdventureViewModel),
                    IsEnabled = true
                },
                 new MenuItem
                 {
                     Title = "Comedy",
                     MenuItemType = MenuItemType.Comedy,
                     ViewModelType = typeof(ComedyViewModel),
                     IsEnabled = true
                 },
                new MenuItem
                {
                    Title = "Hornor",
                    MenuItemType = MenuItemType.Horror,
                    ViewModelType = typeof(HorrorViewModel),
                    IsEnabled = true
                }
            };
        }

        private async void SelectMenuItem(MenuItem item)
        {
            if (item.IsEnabled)
            {
                await _navigationService.NavigateToAsync(item.ViewModelType, null);
            }
        }
    }
}