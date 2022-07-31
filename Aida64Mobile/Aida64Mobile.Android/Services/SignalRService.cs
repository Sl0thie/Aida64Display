namespace Aida64Mobile.Droid.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Aida64Common.Models;
using Aida64Common.ViewModels;

    using Android.App;
    using Android.Content;
    using Android.OS;

    using Microsoft.AspNetCore.SignalR.Client;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// SignalRService service manages a connection to the SignalR server.
    /// </summary>
    [Service]
    public class SignalRService : Service
    {
        private CancellationTokenSource cts;

        private bool IsCharging { get; set; } = false;

        private bool IsConnected { get; set; } = false;

        private bool WasCharging { get; set; } = false;

        private bool WasConnected { get; set; } = false;

        private HubConnection connection;

        /// <summary>
        /// SERVICERUNNINGNOTIFICATIONID is the notification Id for the service.
        /// </summary>
        public const int SERVICERUNNINGNOTIFICATIONID = 10000;

        /// <summary>
        /// Return the communication channel to the service.
        /// </summary>
        /// <param name="intent">The Intent that was used to bind to this service, as given to android.content.Context#bindService Context.bindService. Note that any extras that were included with the Intent at that point will <em>not</em> be seen here.</param>
        /// <returns>Return an IBinder through which clients can call on to the service.</returns>
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        /// <summary>
        /// Called by the system every time a client explicitly starts the service by calling
        /// android.content.Context#startService, providing the arguments it supplied and
        /// a unique integer token representing the start request.
        /// </summary>
        /// <param name="intent">The Intent supplied to android.content.Context#startService, as given. This may be null if the service is being restarted after its process has gone away, and it had previously returned anything except #START_STICKY_COMPATIBILITY.</param>
        /// <param name="flags">Additional data about this start request.</param>
        /// <param name="startId">A unique integer representing this specific request to start. Use with #stopSelfResult(int).</param>
        /// <returns>The return value indicates what semantics the system should use for the service's current started state. It may be one of the constants associated with the #START_CONTINUATION_MASK bits.</returns>
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            cts = new CancellationTokenSource();
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

        /// <summary>
        /// Called by the system to notify a Service that it is no longer used and is being removed.
        /// </summary>
        public override void OnDestroy()
        {
            if (cts != null)
            {
                cts.Token.ThrowIfCancellationRequested();
                cts.Cancel();
            }

            base.OnDestroy();
        }

        /// <summary>
        /// Connection_Reconnecting handles the connection reconnecting event.
        /// </summary>
        /// <param name="arg">The exception that caused the reconnection.</param>
        /// <returns>The successfully completed task.</returns>
        private Task Connection_Reconnecting(Exception arg)
        {
            _ = Check();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Connection_Closed method handles the connection closed event.
        /// </summary>
        /// <param name="arg">The exception that caused the connection to close.</param>
        /// <returns>The successfully completed task.</returns>
        private Task Connection_Closed(Exception arg)
        {
            _ = Check();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Connection_Reconnected method handles the reconnection event.
        /// </summary>
        /// <param name="arg">The connection Id.</param>
        /// <returns>The successfully completed task.</returns>
        private Task Connection_Reconnected(string arg)
        {
            _ = Check();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Battery_BatteryInfoChanged method handles the event fired when the battery information changes.
        /// </summary>
        /// <param name="sender">The object where the event originated.</param>
        /// <param name="e">BatteryInfoChangedEventArgs of the changes.</param>
        private void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
        {
            _ = Check();
        }

        /// <summary>
        /// Check method checks to see if the activity needs to change.
        /// </summary>
        /// <returns>Returns true if completed.</returns>
        private bool Check()
        {
            System.Diagnostics.Debug.WriteLine($"Battery.PowerSource {Battery.PowerSource} Battery.State {Battery.State} Battery.ChargeLevel {Battery.ChargeLevel}");

            if (Battery.State == BatteryState.Discharging)
            {
                if ((Battery.PowerSource == BatteryPowerSource.Wireless) || (Battery.PowerSource == BatteryPowerSource.AC) || ((Battery.PowerSource == BatteryPowerSource.Usb) && (Battery.ChargeLevel == 1)))
                {
                    IsCharging = true;
                }
                else
                {
                    IsCharging = false;
                }
            }
            else
            {
                IsCharging = true;
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

            if (IsCharging)
            {
                if (connection.State == HubConnectionState.Disconnected)
                {
                    System.Diagnostics.Debug.WriteLine("Reconnecting...");
                    _ = connection.StartAsync();
                }
            }

            return true;
        }

        /// <summary>
        /// RecieveSensorData method sends incoming sensor data from SignalR to the MessageCenter.
        /// </summary>
        /// <param name="data">Incoming SensorData.</param>
        private void RecieveSensorData(SensorData data)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"RecieveSensorData {data.TCPU} {data.TGPU1DIO}");

                Device.BeginInvokeOnMainThread(() => MessagingCenter.Send<SensorData>(data, "RecieveSensorData"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }
    }
}