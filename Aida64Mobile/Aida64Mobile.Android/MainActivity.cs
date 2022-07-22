using Aida64Mobile.Droid.Services;

using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

using Android.Content;
using Android.Views;

namespace Aida64Mobile.Droid
{
    [Activity(Label = "Aida64Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Intent serviceMonitor;
        private const int RequestCode = 5469;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Diagnostics.Debug.WriteLine("StartupActivity.OnCreate");

            

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            //Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);


            serviceMonitor = new Intent(this, typeof(SignalRService));
            if (!IsServiceRunning(typeof(SignalRService)))
            {
                _ = StartService(serviceMonitor);
                //_ = StartForegroundService(serviceMonitor);
            }


            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private bool IsServiceRunning(Type cls)
        {
            System.Diagnostics.Debug.WriteLine("StartupActivity.IsServiceRunning");

            ActivityManager manager = (ActivityManager)GetSystemService(Context.ActivityService);
            foreach (ActivityManager.RunningServiceInfo service in manager.GetRunningServices(int.MaxValue))
            {
                if (service.Service.ClassName.Equals(Java.Lang.Class.FromType(cls).CanonicalName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}