using Prism.Commands;
using Prism.Navigation;
using System.Text.RegularExpressions;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;

namespace RollingPlaces.Prism.ViewModels
{
    public class PlaceHistoryPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private PlaceResponse _place;
        private DelegateCommand _checkNameCommand;

        public PlaceHistoryPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _apiService = apiService;
            Title = "Place Information";
        }

        public PlaceResponse Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }

        public string Name { get; set; }

        public DelegateCommand CheckNameCommand => _checkNameCommand ?? (_checkNameCommand = new DelegateCommand(CheckNameAsync));

        private async void CheckNameAsync()
        {
            if (string.IsNullOrEmpty(Name))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a name.",
                    "Accept");
                return;
            }

            /*Regex regex = new Regex(@"^([A-Za-z]{3}\d{3})$");
            if (!regex.IsMatch(Name))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "This place doesn't exist",
                    "Accept");
                return;
            }*/

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetPlaceAsync(Name, url, "api", "/Places");
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Place = (PlaceResponse)response.Result;

            int a = 1;
            int b = 1;
            int c = 1;
        }
    }
}