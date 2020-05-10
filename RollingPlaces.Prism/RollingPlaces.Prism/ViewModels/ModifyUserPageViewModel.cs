using Prism.Navigation;
using RollingPlaces.Prism.Helpers;

namespace RollingPlaces.Prism.ViewModels
{
    public class ModifyUserPageViewModel : ViewModelBase
    {
        public ModifyUserPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.ModifyUser;
        }
    }
}
