using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using RollingPlaces.Common.Helpers;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
using RollingPlaces.Prism.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace RollingPlaces.Prism.ViewModels
{
    public class QualificationPageViewModel : ViewModelBase
    {

        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IGeolocatorService _geolocatorService;
        private PlaceResponse _place;
        private bool _isRunning;
        private bool _isEnabled;
        private int _qualification;
        private Comment _comment;
        private ObservableCollection<Comment> _comments;
        private QualificationsRequest _qualificationsRequest;
        private string _remark;
        private int _value;
        private DelegateCommand _saveQualificationCommand;
        private DelegateCommand _cancelCommand;

        public QualificationPageViewModel(INavigationService navigationService, IApiService apiService, IGeolocatorService geolocatorService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _geolocatorService = geolocatorService;
            _qualificationsRequest = new QualificationsRequest { Qualifications = new List<QualificationRequest>() };
            Title = Languages.Qualification;
            IsEnabled = true;
            Comments = new ObservableCollection<Comment>(CombosHelper.GetComments());
        }
        public DelegateCommand SaveQualificationCommand => _saveQualificationCommand ?? (_saveQualificationCommand = new DelegateCommand(SaveQualificationAsync));
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(CancelAsync));


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }
        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
        public string Remark
        {
            get => _remark;
            set => SetProperty(ref _remark, value);
        }

        public Comment Comment
        {
            get => _comment;
            set
            {
                Comment comment = value;
                Remark += string.IsNullOrEmpty(Remark) ? $"{comment.Name}" : $", {comment.Name}";
                SetProperty(ref _comment, value);
            }
        }

        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set => SetProperty(ref _comments, value);
        }

        public int Qualification
        {
            get => _qualification;
            set => SetProperty(ref _qualification, value);
        }


        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public PlaceResponse Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }
        private async void CancelAsync()
        {
            await _navigationService.NavigateAsync(nameof(PlaceDetailPage));
        }

        private async void SaveQualificationAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }
            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();
            bool connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);

                return;
            }
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            QualificationsRequest qualificationsRequest = new QualificationsRequest();
            QualificationRequest qualificationRequest = new QualificationRequest();
            qualificationRequest.Value = Qualification;
            qualificationRequest.Comment = Remark;
            qualificationRequest.UserId = Guid.Parse(user.Id);
            qualificationRequest.PlaceId = Place.Id;
            qualificationRequest.CreatedDate = DateTime.Now;
            qualificationsRequest.Qualifications = new List<QualificationRequest>();
            qualificationsRequest.Qualifications.Add(qualificationRequest);

            Response response = await _apiService.AddQualificationAsync(url, "api", "/places/AddQualification", qualificationsRequest, "bearer", token.Token);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, "New added Qualification", Languages.Accept);
            await _navigationService.NavigateAsync("/RollingPlacesMasterDetailPage/NavigationPage/HomePage");
        }

        private async Task<bool> ValidateDataAsync()
        {
            if (Qualification == 0)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Select qualification of the place", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Remark))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Enter the expense comment", Languages.Accept);
                return false;
            }

            return true;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            Place = parameters.GetValue<PlaceResponse>("place");
            if (!Settings.IsLogin)
            {
                App.Current.MainPage.DisplayAlert(Languages.Error, "Inicia sesión o registrate para dejar comentarios", Languages.Accept);
                _navigationService.NavigateAsync("/RollingPlacesMasterDetailPage/NavigationPage/LoginPage");
            }
        }
    }

}


