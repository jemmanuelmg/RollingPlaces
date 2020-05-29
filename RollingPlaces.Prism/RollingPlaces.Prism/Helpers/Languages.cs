using RollingPlaces.Prism.Interfaces;
using RollingPlaces.Prism.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RollingPlaces.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        public static string Culture { get; set; }
        public static string ConfirmNewPassword => Resource.ConfirmNewPassword;
        public static string Source => Resource.Source;
        public static string Qualification => Resource.Qualification;
        public static string UserNotFoundError => Resource.UserNotFoundError;
        public static string UserAlreadyExists => Resource.UserAlreadyExists;
        public static string EmailConfirmationBody => Resource.EmailConfirmationBody;
        public static string EmailConfirmationSubject => Resource.EmailConfirmationSubject;
        public static string EmailConfirmationSent => Resource.EmailConfirmationSent;
        public static string RecoverPasswordSubject => Resource.RecoverPasswordSubject;
        public static string RecoverPasswordBody => Resource.RecoverPasswordBody;
        public static string RecoverPasswordEmailSent => Resource.RecoverPasswordEmailSent;
        public static string ChangePasswordSuccess => Resource.ChangePasswordSuccess;
        public static string TheUser => Resource.TheUser;
        public static string WishToAccept => Resource.WishToAccept;
        public static string WishToReject => Resource.WishToReject;
        public static string ConfirmEmail => Resource.ConfirmEmail;
        public static string Reject => Resource.Reject;
        public static string PlaceHistory => Resource.PlaceHistory;
        public static string Error => Resource.Error;
        public static string Accept => Resource.Accept;
        public static string ConnectionError => Resource.ConnectionError;
        public static string Id => Resource.Id;
        public static string CheckName => Resource.CheckName;
        public static string CreateDate => Resource.CreateDate;
        public static string Loading => Resource.Loading;
        public static string FindASpecificPlace => Resource.FindASpecificPlace;
        public static string Description => Resource.Description;
        public static string Category => Resource.Category;
        public static string ReportanIncident => Resource.ReportanIncident;
        public static string Comentary => Resource.Comentary;
        public static string Welcome => Resource.Welcome;
        public static string NewPlace => Resource.NewPlace;
        public static string SeePlaceHistory => Resource.SeePlaceHistory;
        public static string AddDetailstoPlace => Resource.AddDetailstoPlace;
        public static string ModifyUser => Resource.ModifyUser;
        public static string RegisterNewUser => Resource.RegisterNewUser;
        public static string LogIn => Resource.Login;
        public static string Email => Resource.Email;
        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;
        public static string EmailError => Resource.EmailError;
        public static string Password => Resource.Password;
        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;
        public static string PasswordError => Resource.PasswordError;
        public static string Register => Resource.Register;
        public static string LoginError => Resource.LoginError;
        public static string Logout => Resource.Logout;
        public static string Document => Resource.Document;
        public static string DocumentPlaceHolder => Resource.DocumentPlaceHolder;
        public static string DocumentError => Resource.DocumentError;
        public static string FirstName => Resource.FirstName;
        public static string FirstNamePlaceHolder => Resource.FirstNamePlaceHolder;
        public static string FirstNameError => Resource.FirstNameError;
        public static string LastName => Resource.LastName;
        public static string LastNamePlaceHolder => Resource.LastNamePlaceHolder;
        public static string LastNameError => Resource.LastNameError;
        public static string Phone => Resource.Phone;
        public static string PhoneError => Resource.PhoneError;
        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;
        public static string Confirm => Resource.Confirm;
        public static string PasswordConfirm => Resource.PasswordConfirm;
        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;
        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;
        public static string RegisterAs => Resource.RegisterAs;
        public static string RegisterAsError => Resource.RegisterAsError;
        public static string RegisterAsPlaceHolder => Resource.RegisterAsPlaceHolder;
        public static string Ok => Resource.Ok;
        public static string PictureSource => Resource.PictureSource;
        public static string Cancel => Resource.Cancel;
        public static string FromCamera => Resource.FromCamera;
        public static string FromGallery => Resource.FromGallery;
        public static string PasswordRecover => Resource.PasswordRecover;
        public static string ForgotPassword => Resource.ForgotPassword;
        public static string Save => Resource.Save;
        public static string ChangePassword => Resource.ChangePassword;
        public static string UserUpdated => Resource.UserUpdated;
        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;
        public static string ConfirmNewPasswordError => Resource.ConfirmNewPasswordError;
        public static string ConfirmNewPasswordError2 => Resource.ConfirmNewPasswordError2;
        public static string ConfirmNewPasswordPlaceHolder => Resource.ConfirmNewPasswordPlaceHolder;
        public static string CurrentPassword => Resource.CurrentPassword;
        public static string CurrentPasswordError => Resource.CurrentPasswordError;
        public static string CurrentPasswordPlaceHolder => Resource.CurrentPasswordPlaceHolder;
        public static string NewPassword => Resource.NewPassword;
        public static string NewPasswordError => Resource.NewPasswordError;
        public static string NewPasswordPlaceHolder => Resource.NewPasswordPlaceHolder;
        public static string NamePlace => Resource.NamePlace;
        public static string City => Resource.City;
        public static string PlaceError1 => Resource.PlaceError1;
        public static string PlaceError2 => Resource.PlaceError2;
        public static string PlacePlaceHolder => Resource.PlacePlaceHolder;
        public static string Home => Resource.Home;
        public static string Value => Resource.Value;
        public static string Comment => Resource.Comment;
        public static string Comment1 => Resource.Comment1;
        public static string Comment2 => Resource.Comment2;
        public static string Comment3 => Resource.Comment3;
        public static string Comment4 => Resource.Comment4;
        public static string Comment5 => Resource.Comment5;
        public static string Comment6 => Resource.Comment6;
        public static string Comment7 => Resource.Comment7;
        public static string Comment8 => Resource.Comment8;
        public static string Comment9 => Resource.Comment9;
        public static string Comment10 => Resource.Comment10;
        public static string GenericComment => Resource.GenericComment;
        public static string CommnetPlaceHolder => Resource.CommnetPlaceHolder;
        public static string searchplaces => Resource.searchplaces;
        public static string about => Resource.about;

    }
}