using Prism.Commands;
using Prism.Navigation;
using System.Text.RegularExpressions;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace RollingPlaces.Prism.ViewModels
{
    public class PlaceHistoryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private PlaceResponse _place;
        private DelegateCommand _checkNameCommand;
        private bool _isRunning;
        private List<PlaceItemViewModel> _places;

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public PlaceHistoryPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = Languages.PlaceHistory;
        }

        public List<PlaceItemViewModel> Places
        {
            get => _places;
            set => SetProperty(ref _places, value);
        }

        public string Name { get; set; }

        public DelegateCommand CheckNameCommand => _checkNameCommand ?? (_checkNameCommand = new DelegateCommand(GetPlacesAsync));

        private async void GetPlacesAsync()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PlaceError1,
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

            Response response = await _apiService.GetPlaceAsync(Name, url, "api", "/Places");
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