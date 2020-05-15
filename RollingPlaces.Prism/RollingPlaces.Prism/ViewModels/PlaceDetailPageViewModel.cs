using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RollingPlaces.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using RollingPlaces.Common.Helpers;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
using RollingPlaces.Prism.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using RollingPlaces.Prism.Views;

namespace RollingPlaces.Prism.ViewModels
{
    public class PlaceDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private PlaceResponse _place;
        private bool _activateMap;
        private bool _activateDetails;
        private string _showMapButtonText;
        private DelegateCommand _goToAddQualificationCommand;
        private DelegateCommand _showMapCommand;

        public PlaceDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            ActivateDetails = true;
            ActivateMap = false;
            ShowMapButtonText = "Ver Ubicación";
            Title = "Detalles del del lugar";
        }

        public DelegateCommand GoToAddQualificationCommand => _goToAddQualificationCommand ?? (_goToAddQualificationCommand = new DelegateCommand(GoToAddQualification));

        public DelegateCommand ShowMapCommand => _showMapCommand ?? (_goToAddQualificationCommand = new DelegateCommand(ShowMap));

        public PlaceResponse Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
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
            Place = parameters.GetValue<PlaceResponse>("place");

            Position placePosition = new Position(Place.Latitude, Place.Longitude);
            PlaceDetailPage.GetInstance().AddPin(placePosition, "Calle 54 #86A-35", Place.Name, PinType.Place);
        }

        public async void GoToAddQualification()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "place", _place}
            };

            await _navigationService.NavigateAsync(nameof(HomePage), parameters);
        }

        public void ShowMap()
        {
            if (ActivateDetails) 
            {
                ActivateDetails = false;
                ActivateMap = true;
                ShowMapButtonText = "Ver Detalles";
            } 
            else 
            {
                ActivateDetails = true;
                ActivateMap = false;
                ShowMapButtonText = "Ver Ubicación";
            }
        }
    }
}
