namespace Aida64Display.Droid
{
    using Aida64Mobile.Droid.Services;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;

    using Xamarin.Forms;

    /// <summary>
    /// MainActivity activity.
    /// </summary>
    [Activity(Label = "Aida64Display", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //private const int RequestCode = 5469;
        private Intent serviceMonitor;

        /// <summary>
        /// Called when the activity is starting.
        /// </summary>
        /// <param name="savedInstanceState">If the activity is being re-initialized after previously being shut down then this Bundle contains the data it most recently supplied in #onSaveInstanceState. <b>Note: Otherwise it is null.</b></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            serviceMonitor = new Intent(this, typeof(SignalRService));
            _ = StartService(serviceMonitor);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        /// <summary>
        /// Callback for the result from requesting permissions.
        /// </summary>
        /// <param name="requestCode">The request code passed in #requestPermissions(String[], int).</param>
        /// <param name="permissions">The requested permissions. Never null.</param>
        /// <param name="grantResults">The grant results for the corresponding permissions which is either android.content.pm.PackageManager#PERMISSION_GRANTED or android.content.pm.PackageManager#PERMISSION_DENIED. Never null.</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}