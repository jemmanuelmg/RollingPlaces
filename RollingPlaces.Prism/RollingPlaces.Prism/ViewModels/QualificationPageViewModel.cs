using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RollingPlaces.Prism.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RollingPlaces.Prism.ViewModels
{
    public class QualificationPageViewModel : ViewModelBase
    {
        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }


        public QualificationPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = Languages.Qualification;
            //setting.user.id
            /*UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var travelRequest = new TravelRequest
            {
                UserId = Guid.Parse(user.Id),
                PlaceId=1,
                City = City,
                StartDate = StartDate,
                EndDate = EndDate
            };*/
        }
        }
    }

