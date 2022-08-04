namespace Aida64Mobile.Droid
{
    using Aida64Common.Models;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Views;

    using Xamarin.Forms;

    /// <summary>
    /// ChargingActivity activity show the display to the user when the mobile device is charging.
    /// </summary>
    [Activity(Label = "ChargingActivity", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class ChargingActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// OnCeeate callback fires when the system first creates the activity. It is used to perform basic application startup logic..
        /// </summary>
        /// <param name="savedInstanceState">Contains the activity's previous state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnCreate");

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            LoadApplication(new App());
        }

        /// <summary>
        /// OnStart callback fires as the activity become visible to the user.
        /// </summary>
        protected override void OnStart()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnStart");

            SubscribeToMessages();
        }

        /// <summary>
        /// OnResume callback fires when the activity is resumed.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnResume");

            SubscribeToMessages();
        }

        /// <summary>
        /// OnPause callback fires at the first indication that the user is leaving your activity (though it does not always mean the activity is being destroyed).
        /// It indicates that the activity is no longer in the foreground (though it may still be visible if the user is in multi-window mode).
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnPause");

            UnsubscribeToMessages();
        }

        /// <summary>
        /// OnStop callback is fires when your activity is no longer visible to the user, it has entered the Stopped state.
        /// </summary>
        protected override void OnStop()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnStop");

            UnsubscribeToMessages();
        }

        /// <summary>
        /// OnDestroy callback fires before the activity is destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnDestroy");

            UnsubscribeToMessages();
        }

        /// <summary>
        /// OnRestart callback is called after onStop when the current activity is being re-displayed to the user (the user has navigated back to it).
        /// </summary>
        protected override void OnRestart()
        {
            base.OnPause();

            System.Diagnostics.Debug.WriteLine("ChargingActivity.OnRestart");

            SubscribeToMessages();
        }

        /// <summary>
        /// SubscribeToMessages method subscribes to messages with MessageCenter.
        /// </summary>
        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<ControlMessage>(this, "StartMonitor", message =>
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
        }

        /// <summary>
        /// UnsubscribeToMessages method un-subscribes to messages with MessageCenter.
        /// </summary>
        private void UnsubscribeToMessages()
        {
            MessagingCenter.Unsubscribe<ControlMessage>(this, "StartMonitor");
        }
    }
}