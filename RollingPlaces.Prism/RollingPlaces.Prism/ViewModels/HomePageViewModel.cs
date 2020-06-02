using Prism.Commands;
using Prism.Navigation;
using RollingPlaces.Common.Helpers;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
using RollingPlaces.Prism.Views;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace RollingPlaces.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _newPlaceCommand;
        private DelegateCommand _findPlaceCommand;

        public HomePageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            LoadAllPlacesOnMap();
            Title = "Rolling Places";
        }

        public DelegateCommand NewPlaceCommand => _newPlaceCommand ?? (_newPlaceCommand = new DelegateCommand(NewPlaceAsync));

        public DelegateCommand FindPlaceCommand => _findPlaceCommand ?? (_findPlaceCommand = new DelegateCommand(GetPlaceAsync));

        private async void NewPlaceAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(NewPlacePage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
        }

        private async void GetPlaceAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(PlaceHistoryPage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
        }

        public async void LoadAllPlacesOnMap()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);
                return;
            }

            PlaceRequest placeRequest = new PlaceRequest()
            {
                Keywords = "",
                CategoryId = 777,
                CityId = 1
            };

            Response response = await _apiService.GetPlacesAsync(url, "api", "/places/GetPlaces", placeRequest);

            List<PlaceResponse> places = (List<PlaceResponse>)response.Result;
            if (places.Count != 0)
            {
                foreach (PlaceResponse place in places)
                {
                    Position placePosition = new Position(place.Latitude, place.Longitude);
                    Geocoder geoCoder = new Geocoder();
                    IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(placePosition);
                    List<string> addresses = new List<string>(sources);
                    if (addresses.Count == 0)
                    {
                        HomePage.GetInstance().AddPin(placePosition, "Cl. 54 #86-197 a 86-37", place.Name, PinType.Place);
                    }
                    else
                    {
                        HomePage.GetInstance().AddPin(placePosition, addresses[0], place.Name, PinType.Place);
                    }
                    
                }
            }

        }
    }
}
