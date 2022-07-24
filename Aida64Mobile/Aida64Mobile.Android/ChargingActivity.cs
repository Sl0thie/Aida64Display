namespace Aida64Mobile.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aida64Mobile.Models;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Android.Views;
    using Android.Widget;

    using Xamarin.Forms;

    [Activity(Label = "ChargingActivity", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]

    public class ChargingActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnCreate");

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);

            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            LoadApplication(new App());
        }

        protected override void OnStart()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnStart");

            SubscribeToMessages();
        }

        protected override void OnResume()
        {
            base.OnResume();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnResume");

            SubscribeToMessages();
        }

        protected override void OnPause()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnPause");

            UnsubscribeToMessages();
        }

        protected override void OnStop()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnStop");

            UnsubscribeToMessages();
        }

        protected override void OnDestroy()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnDestroy");

            UnsubscribeToMessages();
        }

        protected override void OnRestart()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnRestart");

            SubscribeToMessages();
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<ControlMessage>(this, "StartMonitor", message =>
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
        }

        private void UnsubscribeToMessages()
        {
            MessagingCenter.Unsubscribe<ControlMessage>(this, "StartMonitor");
        }
    }
}