// https://stackoverflow.com/questions/58206527/xamarin-android-hide-unhide-app-from-app-launcher

namespace Aida64Mobile.Droid
{
    using System;

    using Aida64Common.Models;

    using Aida64Mobile.Droid.Services;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;

    using Xamarin.Forms;

    /// <summary>
    /// MainActivity class in the startup Activity for the android application.
    /// </summary>
    [Activity(Label = "Aida64Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const int RequestCode = 5469;
        private Intent serviceMonitor;

        /// <summary>
        /// OnCeeate callback fires when the system first creates the activity. It is used to perform basic application startup logic..
        /// </summary>
        /// <param name="savedInstanceState">Contains the activity's previous state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            System.Diagnostics.Debug.WriteLine("MainActivity.OnCreate");

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            serviceMonitor = new Intent(this, typeof(SignalRService));
            if (!IsServiceRunning(typeof(SignalRService)))
            {
                _ = StartService(serviceMonitor);
            }

            Forms.Init(this, savedInstanceState);

            // HideApp();
            // ComponentName componentName = new ComponentName(this, Java.Lang.Class.FromType(typeof(MainActivity)).Name);
            // PackageManager.SetComponentEnabledSetting(componentName, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);
            ComponentName componentName = new ComponentName(this, Java.Lang.Class.FromType(typeof(MainActivity)).Name);
            PackageManager.SetComponentEnabledSetting(componentName, ComponentEnabledState.Enabled, ComponentEnableOption.DontKillApp);
        }

        /// <summary>
        /// OnRequestPermissionsResult override manages the application's permissions.
        /// </summary>
        /// <param name="requestCode">The request code passed in #requestPermissions(String[], int).</param>
        /// <param name="permissions">The requested permissions. Never null.</param>
        /// <param name="grantResults">The grant results for the corresponding permissions which is either android.content.pm.PackageManager#PERMISSION_GRANTED or android.content.pm.PackageManager#PERMISSION_DENIED. Never null.</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// OnStart callback fires as the activity become visible to the user.
        /// </summary>
        protected override void OnStart()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnStart");

            SubscribeToMessages();

            HideApp();
        }

        /// <summary>
        /// OnResume callback fires when the activity is resumed.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnResume");

            SubscribeToMessages();

            HideApp();
        }

        /// <summary>
        /// OnPause callback fires at the first indication that the user is leaving your activity (though it does not always mean the activity is being destroyed).
        /// It indicates that the activity is no longer in the foreground (though it may still be visible if the user is in multi-window mode).
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnPause");
        }

        /// <summary>
        /// OnStop callback is fires when your activity is no longer visible to the user, it has entered the Stopped state.
        /// </summary>
        protected override void OnStop()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnStop");
        }

        /// <summary>
        /// OnRestart callback is called after onStop when the current activity is being re-displayed to the user (the user has navigated back to it).
        /// </summary>
        protected override void OnRestart()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnRestart");

            SubscribeToMessages();

            HideApp();
        }

        /// <summary>
        /// OnDestroy callback fires before the activity is destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnDestroy");

            UnsubscribeToMessages();
        }

        /// <summary>
        /// OnActivityResult callback fires the result intent returns.
        /// </summary>
        /// <param name="requestCode">The integer request code originally supplied to startActivityForResult(), allowing you to identify who this result came from.</param>
        /// <param name="resultCode">The integer result code returned by the child activity through its setResult().</param>
        /// <param name="data">An Intent, which can return result data to the caller (various data can be attached to Intent "extras").</param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            System.Diagnostics.Debug.WriteLine("MainActivity.OnActivityResult");

            if (requestCode == RequestCode)
            {
                if (Android.Provider.Settings.CanDrawOverlays(this))
                {
                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        /// <summary>
        /// IsServiceRunning method checks to see if a type of service is running.
        /// </summary>
        /// <param name="cls">The type of service.</param>
        /// <returns>Returns true if the service is running.</returns>
        private bool IsServiceRunning(Type cls)
        {
            System.Diagnostics.Debug.WriteLine("MainActivity.IsServiceRunning");

            ActivityManager manager = (ActivityManager)GetSystemService(ActivityService);
            foreach (ActivityManager.RunningServiceInfo service in manager.GetRunningServices(int.MaxValue))
            {
                if (service.Service.ClassName.Equals(Java.Lang.Class.FromType(cls).CanonicalName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// HideApp method hides the application from the user.
        /// </summary>
        private void HideApp()
        {
            Intent intent = new Intent(Intent.ActionMain);
            _ = intent.AddCategory(Intent.CategoryHome);
            _ = intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        /// <summary>
        /// SubscribeToMessages method subscribes to messages with MessageCenter.
        /// </summary>
        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<ControlMessage>(this, "StartPCDisplay", message =>
            {
                Intent intent = new Intent(this, typeof(ChargingActivity));
                StartActivity(intent);
            });
        }

        /// <summary>
        /// UnsubscribeToMessages method un-subscribes to messages with MessageCenter.
        /// </summary>
        private void UnsubscribeToMessages()
        {
            MessagingCenter.Unsubscribe<ControlMessage>(this, "StartPCDisplay");
        }
    }
}