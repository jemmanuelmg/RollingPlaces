using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Helpers;
using RollingPlaces.Prism.Views;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace RollingPlaces.Prism.ViewModels
{
    public class NewPlacePageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private INavigationService _navigationService;
        private readonly IGeolocatorService _geolocatorService;
        private string _source;
        private string _buttonLabel;
        private bool _isRunning;
        private bool _isEnabled;
        private bool _activateMap;
        private bool _activateDetails;
        private string _showMapButtonText;
        private ObservableCollection<PlaceCategory> _categories;
        private ObservableCollection<PlaceCity> _cities;
        private PlaceCategory _category;
        private PlaceCity _city;
        private ImageSource _image;
        private MediaFile _file;
        private PlaceResponse _placeResponse;
        private DelegateCommand _addNewPlaceCommand;
        private DelegateCommand _showMapCommand;
        private DelegateCommand _changeImageCommand;

        public NewPlacePageViewModel(INavigationService navigationService, IGeolocatorService geolocatorService, IApiService apiService)
            : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            _geolocatorService = geolocatorService;
            Categories = new ObservableCollection<PlaceCategory>(CombosHelper.GetPlaceCategories2());
            Cities = new ObservableCollection<PlaceCity>(CombosHelper.GetPlaceCities());
            Category = null;
            City = null;
            ActivateDetails = false;
            ActivateMap = true;
            Image = App.Current.Resources["UrlNoImage"].ToString();
            ShowMapButtonText = "Añadir Detalles";
            Title = "Añadir nuevo lugar";
        }

        public PlaceResponse PlaceResponse
        {
            get => _placeResponse;
            set => SetProperty(ref _placeResponse, value);
        }

        public DelegateCommand AddNewPlaceCommand => _addNewPlaceCommand ?? (_addNewPlaceCommand = new DelegateCommand(AddNewPlace));

        public DelegateCommand ShowMapCommand => _showMapCommand ?? (_showMapCommand = new DelegateCommand(ShowMap));

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public string Name { get; set; }

        public string Description { get; set; }

        public PlaceCategory Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public ObservableCollection<PlaceCategory> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public PlaceCity City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public ObservableCollection<PlaceCity> Cities
        {
            get => _cities;
            set => SetProperty(ref _cities, value);
        }

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
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

        public bool ActivateMap
        {
            get => _activateMap;
            set => SetProperty(ref _activateMap, value);
        }

        public bool ActivateDetails
        {
            get => _activateDetails;
            set => SetProperty(ref _activateDetails, value);
        }

        public string ShowMapButtonText
        {
            get => _showMapButtonText;
            set => SetProperty(ref _showMapButtonText, value);
        }

        private async void AddNewPlace()
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
            Pin pin = NewPlacePage.GetInstance().GetSelectedPosition();
            PlaceRequest2 placeRequest2 = new PlaceRequest2
            {
                Description = Description,
                Latitude = pin.Position.Latitude,
                Longitude = pin.Position.Longitude,
                Name = Name,
                CategoryId = Category.Id,
                CityId = City.Id,
                User = Guid.Parse(user.Id)
            };

            Response response = await _apiService.NewPlaceAsync(url, "/api", "/Places", placeRequest2, "bearer", token.Token);

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
                    "Ingrese un nombre para el lugar",
                    Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Description))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Ingrese una descripcion para el lugar",
                    Languages.Accept);
                return false;
            }

            if (City == null)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Seleccione una ciudad",
                    Languages.Accept);
                return false;
            }

            if (Category == null)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Seleccione una categoría",
                    Languages.Accept);
                return false;
            }

            if (NewPlacePage.GetInstance().GetSelectedPosition() == null)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Seleccione un punto en el mapa para continuar",
                    Languages.Accept);
                return false;
            }

            return true;
        }

        public void ShowMap()
        {
            if (ActivateDetails)
            {
                ActivateDetails = false;
                ActivateMap = true;
                ShowMapButtonText = "Añadir Detalles";
            }
            else
            {
                ActivateDetails = true;
                ActivateMap = false;
                ShowMapButtonText = "Ver Ubicación";
            }
        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                "Obtener imagen de:",
                "Cancelar",
                null,
                "Galeria",
                "Camara");

            if (source == "Cancelar")
            {
                _file = null;
                return;
            }

            if (source == "Camara")
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }
    }




}
