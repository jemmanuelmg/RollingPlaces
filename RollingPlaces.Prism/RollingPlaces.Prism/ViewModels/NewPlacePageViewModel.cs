using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RollingPlaces.Prism.ViewModels
{
    public class NewPlacePageViewModel : ViewModelBase
    {
        public NewPlacePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Add New Place";
        }
    }

}
