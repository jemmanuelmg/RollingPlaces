using Newtonsoft.Json;
using Prism.Navigation;
using RollingPlaces.Common.Helpers;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace RollingPlaces.Prism.ViewModels
{
    public class RollingPlacesMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private UserResponse _user;
        private static RollingPlacesMasterDetailPageViewModel _instance;
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public RollingPlacesMasterDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _instance = this;
            _navigationService = navigationService;
            _apiService = apiService;
            LoadUser();
            LoadMenus();
        }

        public static RollingPlacesMasterDetailPageViewModel GetInstance()
        {
            return _instance;
        }


        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>();


            menus.Add(new Menu
            {
                Icon = "ic_airport_shuttle",
                PageName = "HomePage",
                Title = Languages.Home
            });


            menus.Add(new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = Settings.IsLogin ? "Logout" : Languages.LogIn
            });

            if (Settings.IsLogin)
            {
                menus.Add(new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                    Title = Languages.ModifyUser
                });
            } 
            else
            {
                menus.Add(new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "RegisterPage",
                    Title = "Registrarse"
                });
            }

            menus.Add(new Menu
            {
                Icon = "ic_place",
                PageName = "PlaceHistoryPage",
                Title = "Buscar Lugares"
            });

            menus.Add(new Menu
            {
                Icon = "ic_report",
                PageName = "ReportPage",
                Title = Languages.ReportanIncident
            });


            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }

        public async void ReloadUser()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            EmailRequest emailRequest = new EmailRequest
            {
                CultureInfo = "es",
                Email = user.Email
            };

            Response response = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            UserResponse userResponse = (UserResponse)response.Result;
            Settings.User = JsonConvert.SerializeObject(userResponse);
            LoadUser();
        }
    }
}
