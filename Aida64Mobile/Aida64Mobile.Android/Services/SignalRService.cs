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
    using Android.Telecom;
    using Aida64Mobile.Models;
    using Java.Sql;
    using System.Threading.Tasks;
    using Aida64Mobile.Services;
    using Java.Util;

    [Service]
    public class SignalRService : Service
    {
        CancellationTokenSource _cts;
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;
        bool IsCharging { get; set; } = false;
        bool IsConnected { get; set; } = false;
        bool WasCharging { get; set; } = false;
        bool WasConnected { get; set; } = false;
        HubConnection connection;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _cts = new CancellationTokenSource();

            connection = new HubConnectionBuilder()
                .WithUrl("http://192.168.0.6:929/DataHub").Build();

            connection.Closed += Connection_Closed;
            connection.Reconnected += Connection_Reconnected;
            connection.Reconnecting += Connection_Reconnecting;

            //connection.Closed += async (error) =>
            //{
            //    await Task.Delay(new Random().Next(0, 5) * 1000);
            //    await connection.StartAsync();
            //};

            _ = connection.On<SensorData>("ReceiveData", (data) =>
            {
                RecieveSensorData(data);
            });

            _ = connection.StartAsync();

            Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;

            MessagingCenter.Subscribe<ControlMessage>(this, "FrameFinished", (data) =>
            {
                _ = connection.InvokeAsync("SendData");
            });

            //System.Diagnostics.Debug.WriteLine("SignalRService.OnStartCommand");

            return StartCommandResult.Sticky;
        }

        private Task Connection_Reconnecting(Exception arg)
        {
            //System.Diagnostics.Debug.WriteLine("SignalRService.Connection_Reconnecting");
            _ = Check();
            return Task.CompletedTask;
        }

        private Task Connection_Closed(Exception arg)
        {
            //System.Diagnostics.Debug.WriteLine("SignalRService.Connection_Closed");
            _ = Check();
            return Task.CompletedTask;
        }

        private Task Connection_Reconnected(string arg)
        {
            //System.Diagnostics.Debug.WriteLine("SignalRService.Connection_Reconnected");
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
            //System.Diagnostics.Debug.WriteLine("SignalRService.Battery_BatteryInfoChanged");
            _ = Check();
        }

        private bool Check()
        {
            //System.Diagnostics.Debug.WriteLine("SignalRService.Check");

            if (Battery.State == BatteryState.Discharging)
            {
                IsCharging = false;
            }
            else
            {
                IsCharging = true;
            }

            if(connection.State == HubConnectionState.Connected)
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
                        // Charging in PC.
                        ControlMessage startMessage = new ControlMessage("StartPCDisplay");
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send(startMessage, "StartPCDisplay");
                        });
                    }
                }
            }

            // Store the previous values.
            WasConnected = IsConnected;
            WasCharging = IsCharging;

            if(IsCharging)
            {
                //System.Diagnostics.Debug.WriteLine("Is Charging.");

                if (connection.State == HubConnectionState.Disconnected)
                {
                    System.Diagnostics.Debug.WriteLine("Reconnecting...");
                    _ = connection.StartAsync();
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine($"State {connection.State}");
                }
            }

            return true;
        }

        private void RecieveSensorData(SensorData data)
        {
            //System.Diagnostics.Debug.WriteLine("SignalRService.RecieveSensorData");

            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    MessagingCenter.Send(data, "RecieveSensorData");
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
        }
    }
}