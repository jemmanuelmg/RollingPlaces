using Prism.Commands;
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
