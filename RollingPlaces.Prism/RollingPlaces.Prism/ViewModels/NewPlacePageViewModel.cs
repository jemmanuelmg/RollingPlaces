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
        private readonly IFilesHelper _filesHelper;
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
        private ImageSource _image1;
        private ImageSource _image2;
        private ImageSource _image3;
        private ImageSource _image4;
        private MediaFile _file1;
        private MediaFile _file2;
        private MediaFile _file3;
        private MediaFile _file4;
        private PlaceResponse _placeResponse;
        private DelegateCommand _addNewPlaceCommand;
        private DelegateCommand _showMapCommand;
        private DelegateCommand _changeImageCommand1;
        private DelegateCommand _changeImageCommand2;
        private DelegateCommand _changeImageCommand3;
        private DelegateCommand _changeImageCommand4;

        public NewPlacePageViewModel(INavigationService navigationService, IGeolocatorService geolocatorService, IApiService apiService, IFilesHelper filesHelper)
            : base(navigationService)
        {
            _apiService = apiService;
            _navigationService = navigationService;
            _geolocatorService = geolocatorService;
            _filesHelper = filesHelper;
            Categories = new ObservableCollection<PlaceCategory>(CombosHelper.GetPlaceCategories2());
            Cities = new ObservableCollection<PlaceCity>(CombosHelper.GetPlaceCities());
            Category = null;
            City = null;
            ActivateDetails = false;
            ActivateMap = true;
            Image1 = App.Current.Resources["UrlNoImage"].ToString();
            Image2 = App.Current.Resources["UrlNoImage"].ToString();
            Image3 = App.Current.Resources["UrlNoImage"].ToString();
            Image4 = App.Current.Resources["UrlNoImage"].ToString();
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

        public DelegateCommand ChangeImageCommand1 => _changeImageCommand1 ?? (_changeImageCommand1 = new DelegateCommand(ChangeImageAsync1));
        public DelegateCommand ChangeImageCommand2 => _changeImageCommand2 ?? (_changeImageCommand2 = new DelegateCommand(ChangeImageAsync2));
        public DelegateCommand ChangeImageCommand3 => _changeImageCommand3 ?? (_changeImageCommand3 = new DelegateCommand(ChangeImageAsync3));
        public DelegateCommand ChangeImageCommand4 => _changeImageCommand4 ?? (_changeImageCommand4 = new DelegateCommand(ChangeImageAsync4));

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

        public ImageSource Image1
        {
            get => _image1;
            set => SetProperty(ref _image1, value);
        }

        public ImageSource Image2
        {
            get => _image2;
            set => SetProperty(ref _image2, value);
        }

        public ImageSource Image3
        {
            get => _image3;
            set => SetProperty(ref _image3, value);
        }

        public ImageSource Image4
        {
            get => _image4;
            set => SetProperty(ref _image4, value);
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

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (!Settings.IsLogin)
            {
                App.Current.MainPage.DisplayAlert(Languages.Error, "Inicia sesión o registrate para agregar nuevos lugares", Languages.Accept);
                _navigationService.NavigateAsync("/RollingPlacesMasterDetailPage/NavigationPage/LoginPage");
            }
        }

        private async void AddNewPlace()
        {
            if (!Settings.IsLogin)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Inicia sesión o registrate para agregar lugares", Languages.Accept);
                await _navigationService.NavigateAsync("/RollingPlacesMasterDetailPage/NavigationPage/HomePage");
            }

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

            byte[] imageArray1 = null;
            if (_file1 != null)
            {
                imageArray1 = _filesHelper.ReadFully(_file1.GetStream());
            }

            byte[] imageArray2 = null;
            if (_file2 != null)
            {
                imageArray2 = _filesHelper.ReadFully(_file2.GetStream());
            }

            byte[] imageArray3 = null;
            if (_file3 != null)
            {
                imageArray3 = _filesHelper.ReadFully(_file3.GetStream());
            }

            byte[] imageArray4 = null;
            if (_file4 != null)
            {
                imageArray4 = _filesHelper.ReadFully(_file4.GetStream());
            }

            PlaceRequest2 placeRequest2 = new PlaceRequest2
            {
                Description = Description,
                Latitude = pin.Position.Latitude,
                Longitude = pin.Position.Longitude,
                Name = Name,
                CategoryId = Category.Id,
                CityId = City.Id,
                User = Guid.Parse(user.Id),
                PictureArray1 = imageArray1,
                PictureArray2 = imageArray2,
                PictureArray3 = imageArray3,
                PictureArray4 = imageArray4
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

        private async void ChangeImageAsync1()
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
                _file1 = null;
                return;
            }

            if (source == "Camara")
            {
                _file1 = await CrossMedia.Current.TakePhotoAsync(
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
                _file1 = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file1 != null)
            {
                Image1 = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file1.GetStream();
                    return stream;
                });
            }
        }

        private async void ChangeImageAsync2()
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
                _file2 = null;
                return;
            }

            if (source == "Camara")
            {
                _file2 = await CrossMedia.Current.TakePhotoAsync(
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
                _file2 = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file2 != null)
            {
                Image2 = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file2.GetStream();
                    return stream;
                });
            }
        }

        private async void ChangeImageAsync3()
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
                _file3 = null;
                return;
            }

            if (source == "Camara")
            {
                _file3 = await CrossMedia.Current.TakePhotoAsync(
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
                _file3 = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file3 != null)
            {
                Image3 = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file3.GetStream();
                    return stream;
                });
            }
        }

        private async void ChangeImageAsync4()
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
                _file4 = null;
                return;
            }

            if (source == "Camara")
            {
                _file4 = await CrossMedia.Current.TakePhotoAsync(
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
                _file4 = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file4 != null)
            {
                Image4 = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file4.GetStream();
                    return stream;
                });
            }
        }
    }




}
