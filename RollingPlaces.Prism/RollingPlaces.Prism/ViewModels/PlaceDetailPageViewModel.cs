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
        private List<CarouselModel> _imageCollection;
        private List<string> _imageList;

        public PlaceDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            ActivateDetails = true;
            ActivateMap = false;
            ShowMapButtonText = "Ver Ubicación";
            _imageCollection = new List<CarouselModel>();
            _imageList = new List<string>();
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

        public List<CarouselModel> ImageCollection
        {
            get => _imageCollection;
            set => SetProperty(ref _imageCollection, value);
        }

        public List<string> ImageList
        {
            get => _imageList;
            set => SetProperty(ref _imageList, value);
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Place = parameters.GetValue<PlaceResponse>("place");
            Position placePosition = new Position(Place.Latitude, Place.Longitude);
            PlaceDetailPage.GetInstance().AddPin(placePosition, "Calle 54 #86A-35", Place.Name, PinType.Place);
            PlaceDetailPage.GetInstance().PopulateImagesToCarousel();
        }

        public async void GoToAddQualification()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "place", _place}
            };

            await _navigationService.NavigateAsync(nameof(HomePage), parameters);
        }

        /*public void PopulateImagesOfPlace()
        {
            ImageCollection.Add(new CarouselModel("photo1.png"));
            ImageCollection.Add(new CarouselModel("photo2.png"));
            ImageCollection.Add(new CarouselModel("photo3.png"));
            ImageCollection.Add(new CarouselModel("photo4.png"));
            ImageCollection.Add(new CarouselModel("photo5.png"));
            ImageCollection.Add(new CarouselModel("photo6.png"));

            foreach (PhotoResponse photo in Place.Photos)
            {
                ImageCollection.Add(new CarouselModel(photo.PhotoPath));
            }
        }*/

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
