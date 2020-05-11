using Prism.Navigation;
using RollingPlaces.Prism.Helpers;

namespace RollingPlaces.Prism.ViewModels
{
    public class ReportPageViewModel : ViewModelBase
    {
        public ReportPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Languages.ReportanIncident;
        }
    }
}
