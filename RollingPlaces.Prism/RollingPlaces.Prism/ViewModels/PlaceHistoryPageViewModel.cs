﻿using Prism.Commands;
using Prism.Navigation;
using System.Text.RegularExpressions;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;

namespace RollingPlaces.Prism.ViewModels
{
    public class PlaceHistoryPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private PlaceResponse _place;
        private DelegateCommand _checkNameCommand;
        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public PlaceHistoryPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {

            _apiService = apiService;
            Title = Languages.PlaceHistory;
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
                await App.Current.MainPage.DisplayAlert(Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);

                return;
            }

            Response response = await _apiService.GetPlaceAsync(Name,url, "api", "/Places");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);

                return;
            }

            Place = (PlaceResponse)response.Result;
        }
    }
}