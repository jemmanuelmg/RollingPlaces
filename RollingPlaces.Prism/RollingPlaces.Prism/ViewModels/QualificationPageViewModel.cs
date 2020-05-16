﻿using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RollingPlaces.Common.Helpers;
using RollingPlaces.Common.Models;
using RollingPlaces.Common.Services;
using RollingPlaces.Prism.Helpers;
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
        private int _placeId;
        private bool _isRunning;
        private bool _isEnabled;
        private int _qualification;
        private Comment _comment;
        private ObservableCollection<Comment> _comments;
        private string _remark;
        private int _value;
        private DelegateCommand _saveQualificationCommand;

        public QualificationPageViewModel(INavigationService navigationService, IApiService apiService, IGeolocatorService geolocatorService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            _geolocatorService = geolocatorService;
            Title = Languages.Qualification;
            IsEnabled = true;
            Comments = new ObservableCollection<Comment>(CombosHelper.GetComments());
        }
        public DelegateCommand SaveQualificationCommand => _saveQualificationCommand ?? (_saveQualificationCommand = new DelegateCommand(SaveQualificationAsync));


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
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error,
                    Languages.ConnectionError,
                    Languages.Accept);

                return;
            }
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            QualificationRequest QualificationRequest = new QualificationRequest
            {
                Value = Qualification,
                Comment = Remark,
                UserId= Guid.Parse("e2a7b8dc-a117-4b1a-a3f6-e51f49314cf4"/*user.Id*/),
                PlaceId=1,
                CreatedDate = DateTime.Now
            };

            var response = await _apiService.AddQualification(url, "/api", "/places/AddQualification", "bearer", token.Token,QualificationRequest);
            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, "New added Qualification", Languages.Accept);
            await _navigationService.NavigateAsync("/RollingPlacesMasterDetailPageViewModel/NavigationPage/HomePage");
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
    }

}


