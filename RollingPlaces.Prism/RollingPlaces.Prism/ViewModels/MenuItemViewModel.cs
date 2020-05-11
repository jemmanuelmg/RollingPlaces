﻿using RollingPlaces.Common.Models;
using Prism.Commands;
using Prism.Navigation;


namespace RollingPlaces.Prism.ViewModels
{

    public class MenuItemViewModel : Menu
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectMenuCommand;

        public MenuItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectMenuCommand => _selectMenuCommand ?? (_selectMenuCommand = new DelegateCommand(SelectMenuAsync));

        private async void SelectMenuAsync()
        {
            await _navigationService.NavigateAsync($"/RollingPlacesMasterDetailPage/NavigationPage/{PageName}");
        }
    }
}