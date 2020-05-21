using Prism.Commands;
using Prism.Navigation;
using RollingPlaces.Common.Models;
using RollingPlaces.Prism.Views;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace RollingPlaces.Prism.ViewModels
{
    public class PlaceDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private PlaceResponse _place;
        private bool _activateMap;
        private bool _activateDetails;
        private string _noPhotosMessage;
        private string _showMapButtonText;
        private string _image1;
        private string _image2;
        private string _image3;
        private string _image4;
        private DelegateCommand _goToAddQualificationCommand;
        private DelegateCommand _showMapCommand;

        public PlaceDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            ActivateDetails = true;
            ActivateMap = false;
            Image1 = App.Current.Resources["UrlNoImage"].ToString();
            Image2 = App.Current.Resources["UrlNoImage"].ToString();
            Image3 = App.Current.Resources["UrlNoImage"].ToString();
            Image4 = App.Current.Resources["UrlNoImage"].ToString();
            ShowMapButtonText = "Ver Ubicación";
            Title = "Detalles del del lugar";
        }

        public DelegateCommand GoToAddQualificationCommand => _goToAddQualificationCommand ?? (_goToAddQualificationCommand = new DelegateCommand(GoToAddQualification));

        public DelegateCommand ShowMapCommand => _showMapCommand ?? (_showMapCommand = new DelegateCommand(ShowMap));

        public string Image1
        {
            get => _image1;
            set => SetProperty(ref _image1, value);
        }

        public string Image2
        {
            get => _image2;
            set => SetProperty(ref _image2, value);
        }

        public string Image3
        {
            get => _image3;
            set => SetProperty(ref _image3, value);
        }

        public string Image4
        {
            get => _image4;
            set => SetProperty(ref _image4, value);
        }

        public string NoPhotosMessage
        {
            get => _noPhotosMessage;
            set => SetProperty(ref _noPhotosMessage, value);
        }

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
            PlaceDetailPage.GetInstance().MoveMapToCurrentPositionAsync(Place.Latitude, Place.Longitude);
            List<PhotoResponse> photos = (List <PhotoResponse>) Place.Photos;
            
            int i = 0;
            foreach(PhotoResponse photo in Place.Photos)
            {
                if (i == 0)
                {
                    Image1 = photo.PhotoPath;
                }
                else if (i == 1)
                {
                    Image2 = photo.PhotoPath;
                }
                else if (i == 2)
                {
                    Image3 = photo.PhotoPath;
                }
                else if (i == 3)
                {
                    Image4 = photo.PhotoPath;
                }
                i++;
            }
        }

        public async void GoToAddQualification()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "place", _place}
            };

            await _navigationService.NavigateAsync(nameof(QualificationPage), parameters);
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
