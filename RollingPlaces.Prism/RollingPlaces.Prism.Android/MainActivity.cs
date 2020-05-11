using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Prism;
using Prism.Ioc;
<<<<<<< HEAD
using Xamarin.Forms;
using Syncfusion.SfBusyIndicator.XForms.Droid;
=======
using Syncfusion.SfBusyIndicator.XForms.Droid;
using Syncfusion.SfRating.XForms.Droid;
>>>>>>> RamaJulian

namespace RollingPlaces.Prism.Droid
{
    [Activity(Label = "RollingPlaces", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Forms.SetFlags("CollectionView_Experimental");

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            CrossCurrentActivity.Current.Init(this, bundle);

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
<<<<<<< HEAD

            new SfBusyIndicatorRenderer();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
=======
            new SfBusyIndicatorRenderer();
            new SfRatingRenderer();
>>>>>>> RamaJulian
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

