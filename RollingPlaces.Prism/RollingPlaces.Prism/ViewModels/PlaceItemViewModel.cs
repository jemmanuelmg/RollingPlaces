using Prism.Commands;
using Prism.Navigation;
using RollingPlaces.Prism.Views;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Models;
using RollingPlaces.Prism.Views;

namespace RollingPlaces.Prism.ViewModels
{
    public class PlaceItemViewModel : PlaceResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectTravel2Command;

        public PlaceItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectTravel2Command => _selectTravel2Command ?? (_selectTravel2Command = new DelegateCommand(SelectTravel2Async));

        private async void SelectTravel2Async()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "travel", this }
            };

            await _navigationService.NavigateAsync(nameof(PlaceDetailPage), parameters);
        }
    }
}