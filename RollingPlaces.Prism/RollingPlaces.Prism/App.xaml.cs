using Prism;
using Prism.Ioc;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.ViewModels;
using RollingPlaces.Prism.Views;
using Syncfusion.Licensing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RollingPlaces.Common.Helpers;
using RollingPlaces.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RollingPlaces.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjM1MDA1QDMxMzgyZTMxMmUzME81YmZCTldtMklhcDZQRkVoMThKQUJ2M3FLYVpzTkd4K0FLa1FJRmt4N289");
            InitializeComponent();
            await NavigationService.NavigateAsync("/RollingPlacesMasterDetailPage/NavigationPage/HomePage");
            SyncfusionLicenseProvider.RegisterLicense("MjU0NjcyQDMxMzgyZTMxMmUzMGJ1T1hnaWw4dzJJODVuZm1PaFJYTlIxLy9uNHg4cEh3NCtCRzBJSjd2ZkE9");
            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.Register<IGeolocatorService, GeolocatorService>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<RollingPlacesMasterDetailPage, RollingPlacesMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<PlaceHistoryPage, PlaceHistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportPage, ReportPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RememberPasswordPage, RememberPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ChangePasswordPage, ChangePasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<PlaceDetailPage, PlaceDetailPageViewModel>();
        }
    }
}
