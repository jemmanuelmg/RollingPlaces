/*using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RollingPlaces.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RollingPlaces.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.Home;
        }
    }
}
*/

using Prism.Commands;
using Prism.Navigation;
using RollingPlaces.Common.Helpers;
using RollingPlaces.Prism.Views;

namespace RollingPlaces.Prism.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _newPlaceCommand;
        private DelegateCommand _findPlaceCommand;
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Rolling Places";
        }

        public DelegateCommand NewPlaceCommand => _newPlaceCommand ?? (_newPlaceCommand = new DelegateCommand(NewPlaceAsync));
        public DelegateCommand FindPlaceCommand => _findPlaceCommand ?? (_findPlaceCommand = new DelegateCommand(GetPlaceAsync));
        private async void NewPlaceAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(NewPlacePage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
        }

        private async void GetPlaceAsync()
        {
            if (Settings.IsLogin)
            {
                await _navigationService.NavigateAsync(nameof(PlaceHistoryPage));
            }
            else
            {
                await _navigationService.NavigateAsync(nameof(LoginPage));
            }
        }
    }
}
