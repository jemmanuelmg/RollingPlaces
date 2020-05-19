using Prism.Commands;
using Prism.Navigation;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RollingPlaces.Prism.ViewModels
{
    public class PlaceHistoryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _checkNameCommand;
        private bool _isRunning;
        private List<PlaceItemViewModel> _places;
        private ObservableCollection<PlaceCategory> _categories;
        private ObservableCollection<PlaceCity> _cities;
        private PlaceCategory _category;
        private PlaceCity _city;


        public PlaceHistoryPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Categories = new ObservableCollection<PlaceCategory>(CombosHelper.GetPlaceCategories());
            Cities = new ObservableCollection<PlaceCity>(CombosHelper.GetPlaceCities());
            Category = new PlaceCategory() { Name = "Todas", Id = 777 };
            City = null;
            NoItemsTitle = "";
            NoItemsMessage = "Busca lugares con palabras clave, por ciudad o por categoría.";
            Title = Languages.PlaceHistory;
        }

        public string Keywords { get; set; }

        public string NoItemsTitle { get; set; }

        public string NoItemsMessage { get; set; }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public List<PlaceItemViewModel> Places
        {
            get => _places;
            set => SetProperty(ref _places, value);
        }

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

        public string Name { get; set; }

        public DelegateCommand CheckNameCommand => _checkNameCommand ?? (_checkNameCommand = new DelegateCommand(GetPlacesAsync));

        private async void GetPlacesAsync()
        {
            if (City == null)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Porfavor selecciona una ciudad",
                    Languages.Accept);
                return;
            }

            IsRunning = true;
            string url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);

                return;
            }

            PlaceRequest placeRequest = new PlaceRequest()
            {
                Keywords = Keywords,
                CategoryId = Category.Id,
                CityId = City.Id
            };

            Response response = await _apiService.GetPlacesAsync(url, "api", "/places/GetPlaces", placeRequest);
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);

                return;
            }

            List<PlaceResponse> places = (List<PlaceResponse>)response.Result;
            if (places.Count == 0)
            {
                NoItemsTitle = "Sin resultados";
                NoItemsMessage = "No se encontraron resultados. Introduce otros criterios de búsqueda diferentes.";
            }
            Places = places.Select(t => new PlaceItemViewModel(_navigationService)
            {
                Id = t.Id,
                CreatedDate = t.CreatedDate,
                Name = t.Name,
                Description = t.Description,
                Latitude = t.Latitude,
                Longitude = t.Longitude,
                Qualifications = t.Qualifications,
                Photos = t.Photos,
                User = t.User,
                Category = t.Category,
                City = t.City
            }).ToList();
        }

    }
}