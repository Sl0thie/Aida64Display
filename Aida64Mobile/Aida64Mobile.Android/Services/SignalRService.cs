namespace Aida64Mobile.Droid.Services
{
    using System;
    using System.Threading;

    using Android.App;
    using Android.Content;
    using Android.OS;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    using Microsoft.AspNetCore.SignalR.Client;
    using Aida64Mobile.Models;
    using System.Threading.Tasks;

    [Service]
    public class SignalRService : Service
    {
        private CancellationTokenSource _cts;
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;

        private bool IsCharging { get; set; } = false;
        private bool IsConnected { get; set; } = false;
        private bool WasCharging { get; set; } = false;
        private bool WasConnected { get; set; } = false;

        private HubConnection connection;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();
            connection = new HubConnectionBuilder().WithUrl("http://192.168.0.6:929/DataHub").Build();
            connection.Closed += Connection_Closed;
            connection.Reconnected += Connection_Reconnected;
            connection.Reconnecting += Connection_Reconnecting;
            _ = connection.On<SensorData>("ReceiveData", (data) => RecieveSensorData(data));
            _ = connection.StartAsync();
            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            MessagingCenter.Subscribe<ControlMessage>(this, "FrameFinished", (data) => _ = connection.InvokeAsync("SendData"));

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                _ = Check();
                return false;
            });

            return StartCommandResult.Sticky;
        }

        private Task Connection_Reconnecting(Exception arg)
        {
            _ = Check();
            return Task.CompletedTask;
        }

        private Task Connection_Closed(Exception arg)
        {
            _ = Check();
            return Task.CompletedTask;
        }

        private Task Connection_Reconnected(string arg)
        {
            _ = Check();
            return Task.CompletedTask;
        }

        public override void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();
                _cts.Cancel();
            }

            base.OnDestroy();
        }

        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            _ = Check();
        }

        private bool Check()
        {
            if ((Battery.PowerSource == BatteryPowerSource.Wireless) || (Battery.PowerSource == BatteryPowerSource.AC) || (Battery.PowerSource == BatteryPowerSource.Usb))
            {
                IsCharging = true;
            }
            else
            {
                if (Battery.State == BatteryState.Discharging)
                {
                    IsCharging = false;
                }
            }

            if (connection.State == HubConnectionState.Connected)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }

            if ((IsConnected != WasConnected) || (IsCharging != WasCharging))
            {
                if (IsCharging)
                {
                    if (IsConnected)
                    {
                        ControlMessage startMessage = new ControlMessage("StartPCDisplay");
                        Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(startMessage, "StartPCDisplay"));
                    }
                }
                else
                {
                    // Phone is off the charger.
                    ControlMessage startMessage = new ControlMessage("StartMonitor");
                    Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(startMessage, "StartMonitor"));
                }
            }

            // Store the previous values.
            WasConnected = IsConnected;
            WasCharging = IsCharging;

            if(IsCharging)
            {
                if (connection.State == HubConnectionState.Disconnected)
                {
                    System.Diagnostics.Debug.WriteLine("Reconnecting...");
                    _ = connection.StartAsync();
                }
            }

            return true;
        }

        private void RecieveSensorData(SensorData data)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(data, "RecieveSensorData"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }
    }
}