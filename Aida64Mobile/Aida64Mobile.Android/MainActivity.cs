namespace Aida64Mobile.Droid
{
    using Aida64Mobile.Droid.Services;

    using System;

    using Android.App;
    using Android.Content.PM;
    using Android.Runtime;
    using Android.OS;

    using Android.Content;
    using Android.Views;
    using Aida64Mobile.Models;
    using Xamarin.Forms;

    [Activity(Label = "Aida64Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private Intent serviceMonitor;
        private const int RequestCode = 5469;

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

            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            HideApp();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnStart()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnStart");

            SubscribeToMessages();
        }

        protected override void OnResume()
        {
            base.OnResume();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnResume");

            SubscribeToMessages();
        }

        protected override void OnPause()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnPause");
        }

        protected override void OnStop()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnStop");

            //UnsubscribeToMessages();
        }

        protected override void OnDestroy()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnDestroy");

            UnsubscribeToMessages();
        }

        protected override void OnRestart()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("MainActivity.OnRestart");

            SubscribeToMessages();
        }

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

        private void HideApp()
        {
            Intent intent = new Intent(Intent.ActionMain);
            _ = intent.AddCategory(Intent.CategoryHome);
            _ = intent.SetFlags(ActivityFlags.NewTask);
            StartActivity(intent);
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<ControlMessage>(this, "StartPCDisplay", message =>
            {
                Intent intent = new Intent(this, typeof(ChargingActivity));
                StartActivity(intent);
            });
        }

        private void UnsubscribeToMessages()
        {
            MessagingCenter.Unsubscribe<ControlMessage>(this, "StartPCDisplay");
        }
    }
}