using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Helpers;
using Newtonsoft.Json;
using System;
using RollingPlaces.Prism.Views;

namespace RollingPlaces.Prism.ViewModels
{
    public class NewPlacePageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private INavigationService _navigationService;
        private readonly IGeolocatorService _geolocatorService;
        private string _source;
        private string _buttonLabel;
        //private bool _isSecondButtonVisible;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _getAddressCommand;
        private DelegateCommand _startTripCommand;
        private PlaceResponse _placeResponse;

        public NewPlacePageViewModel(INavigationService navigationService, IGeolocatorService geolocatorService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            _geolocatorService = geolocatorService;
            Title = "Add New Place";
            ButtonLabel = "Add Place";
            LoadSourceAsync();
        }

        public PlaceResponse PlaceResponse
        {
            get => _placeResponse;
            set => SetProperty(ref _placeResponse, value);
        }

        public DelegateCommand GetAddressCommand => _getAddressCommand ?? (_getAddressCommand = new DelegateCommand(LoadSourceAsync));

        public DelegateCommand StartTripCommand => _startTripCommand ?? (_startTripCommand = new DelegateCommand(StartTripAsync));

        public string Name { get; set; }

        /*public bool IsSecondButtonVisible
        {
            get => _isSecondButtonVisible;
            set => SetProperty(ref _isSecondButtonVisible, value);
        }
        */
        public string ButtonLabel
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Source
        {
            get => _buttonLabel;
            set => SetProperty(ref _buttonLabel, value);
        }

        private async void LoadSourceAsync()
        {
            await _geolocatorService.GetLocationAsync();
            if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
            {
                Position position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
                Geocoder geoCoder = new Geocoder();
                IEnumerable<string> sources = await geoCoder.GetAddressesForPositionAsync(position);
                List<string> addresses = new List<string>(sources);

                if (addresses.Count > 0)
                {
                    Source = addresses[0];
                }
            }
        }

        private async void StartTripAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);
                return;
            }

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            PlaceRequest placeRequest = new PlaceRequest
            {
                Description = Source,
                Latitude = _geolocatorService.Latitude,
                Longitude = _geolocatorService.Longitude,
                Name = Name,
                User = Guid.Parse(user.Id)
            };

            Response response = await _apiService.NewPlaceAsync(url, "/api", "/Places", placeRequest, "bearer", token.Token);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }
            /*Position _position = new Position(_geolocatorService.Latitude, _geolocatorService.Longitude);
            _placeResponse = (PlaceResponse)response.Result;
            //IsSecondButtonVisible = true;
            ButtonLabel = "Regresar";
            NewPlacePage.GetInstance().AddPin(_position, Source, "", PinType.Place);
            */
           

            IsRunning = false;
            IsEnabled = true;
            await App.Current.MainPage.DisplayAlert("Ok", "Nuevo Lugar agregado correctamente", "Aceptar");
            await _navigationService.NavigateAsync(nameof(HomePage));
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Enter a Name",
                    Languages.Accept);
                return false;
            }

            return true;
        }
    }




}
