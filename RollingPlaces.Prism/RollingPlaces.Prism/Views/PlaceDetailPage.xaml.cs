using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using RollingPlaces.Common.Services;
using Xamarin.Forms.Maps;
using Syncfusion.SfCarousel.XForms;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace RollingPlaces.Prism.Views
{
    public partial class PlaceDetailPage : ContentPage
    {
        private readonly IGeolocatorService _geolocatorService;
        private static PlaceDetailPage _instance;

        public PlaceDetailPage(IGeolocatorService geolocatorService)
        {
            InitializeComponent();
            _instance = this;
            _geolocatorService = geolocatorService;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MoveMapToCurrentPositionAsync();
        }


        public static PlaceDetailPage GetInstance()
        {
            return _instance;
        }

        public void AddPin(Position position, string address, string label, PinType pinType)
        {
            MyMap.Pins.Add(new Pin
            {
                Address = address,
                Label = label,
                Position = position,
                Type = pinType
            });
        }

        private async void MoveMapToCurrentPositionAsync()
        {
            bool isLocationPermision = await CheckLocationPermisionsAsync();

            if (isLocationPermision)
            {
                MyMap.IsShowingUser = true;

                await _geolocatorService.GetLocationAsync();
                if (_geolocatorService.Latitude != 0 && _geolocatorService.Longitude != 0)
                {
                    Position position = new Position(
                        _geolocatorService.Latitude,
                        _geolocatorService.Longitude);
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                        position,
                        Distance.FromKilometers(.5)));
                }
            }
        }

        public void PopulateImagesToCarousel()
        {
            ObservableCollection<SfCarouselItem> carouselItems = new ObservableCollection<SfCarouselItem>();
            carouselItems.Add(new SfCarouselItem() { ImageName = "photo1.png" });
            carouselItems.Add(new SfCarouselItem() { ImageName = "photo2.png" });
            carouselItems.Add(new SfCarouselItem() { ImageName = "photo3.png" });
            carouselItems.Add(new SfCarouselItem() { ImageName = "photo4.png" });
            carouselItems.Add(new SfCarouselItem() { ImageName = "photo5.png" });
            carouselItems.Add(new SfCarouselItem() { ImageName = "photo6.png" });
            carousel.ItemsSource = carouselItems;
        }

        private async Task<bool> CheckLocationPermisionsAsync()
        {
            PermissionStatus permissionLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            PermissionStatus permissionLocationAlways = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            PermissionStatus permissionLocationWhenInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            bool isLocationEnabled = permissionLocation == PermissionStatus.Granted || permissionLocationAlways == PermissionStatus.Granted || permissionLocationWhenInUse == PermissionStatus.Granted;
            if (isLocationEnabled)
            {
                return true;
            }
            await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            permissionLocation = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            permissionLocationAlways = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationAlways);
            permissionLocationWhenInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            return permissionLocation == PermissionStatus.Granted || permissionLocationAlways == PermissionStatus.Granted || permissionLocationWhenInUse == PermissionStatus.Granted;
        }

    }
}
