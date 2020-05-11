using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RollingPlaces.Common.Models;
using RollingPlaces.Prism.Helpers;


namespace RollingPlaces.Prism.ViewModels
{
    public class RollingPlacesMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public RollingPlacesMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_airport_shuttle",
                    PageName = "HomePage",
                       Title = Languages.Home

                },
                new Menu
                {
                    Icon = "ic_local_taxi",
                    PageName = "PlaceHistoryPage",
                     Title = Languages.SeePlaceHistory
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                   Title = Languages.ModifyUser
                },
                new Menu
                {
                    Icon = "ic_report",
                    PageName = "ReportPage",
                   Title = Languages.ReportanIncident
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                   Title = Languages.LogIn
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
