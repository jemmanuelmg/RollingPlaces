using Prism.Navigation;
using RollingPlaces.Prism.Helpers;

namespace RollingPlaces.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.LogIn;
        }
    }
}
