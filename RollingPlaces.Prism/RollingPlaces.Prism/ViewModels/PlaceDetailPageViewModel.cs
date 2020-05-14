using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RollingPlaces.Common.Models;
using RollingPlaces.Prism.Views;

namespace RollingPlaces.Prism.ViewModels
{
    public class PlaceDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private PlaceResponse _place;
        private DelegateCommand _goToAddQualificationCommand;

        public PlaceDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Detalles del del lugar";
        }

        public DelegateCommand GoToAddQualificationCommand => _goToAddQualificationCommand ?? (_goToAddQualificationCommand = new DelegateCommand(GoToAddQualification));

        public PlaceResponse Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Place = parameters.GetValue<PlaceResponse>("place");
        }

        public async void GoToAddQualification()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "place", _place}
            };

            await _navigationService.NavigateAsync(nameof(HomePage), parameters);
        }
    }
}
