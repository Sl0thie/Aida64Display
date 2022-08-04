namespace Aida64Mobile.Droid.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Aida64Common.Models;
using Aida64Display.Droid;

    using Android.App;
    using Android.Content;
    using Android.OS;

    using Microsoft.AspNetCore.SignalR.Client;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// SignalRService service manages a connection to SignalR.
    /// </summary>
    [Service]
    public class SignalRService : Service
    {
        private CancellationTokenSource cts;
        private HubConnection connection;

        /// <summary>
        /// SERVICERUNNINGNOTIFICATIONID is the notification id of the service.
        /// </summary>
        public const int SERVICERUNNINGNOTIFICATIONID = 10001;

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
        /// Called by the system every time a client explicitly starts the service by calling android.content.Context#startService, providing the arguments it supplied and a unique integer token representing the start request.
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

            MessagingCenter.Subscribe<ControlMessage>(this, "FrameFinished", (data) =>
            {
                if (connection.State == HubConnectionState.Disconnected)
                {
                    _ = connection.StartAsync();
                }

                _ = connection.InvokeAsync("SendData");
            });

            MessagingCenter.Subscribe<ImageData>(this, "SaveImage", (data) =>
            {
                storePhotoToGallery(data.Data, data.FileName);
            });

            _ = connection.On<SensorData>("ReceiveData", (data) => RecieveSensorData(data));

            Device.StartTimer(TimeSpan.FromSeconds(60), () =>
            {
                _ = connection.InvokeAsync("SendData");
                return true;
            });

            return StartCommandResult.Sticky;
        }

        /// <summary>
        /// Connection_Reconnecting handles the connection reconnecting event.
        /// </summary>
        /// <param name="arg">The exception that caused the reconnection.</param>
        /// <returns>The successfully completed task.</returns>
        private Task Connection_Reconnecting(Exception arg)
        {
            System.Diagnostics.Debug.WriteLine($"Connection_Reconnecting {arg.Message}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Connection_Closed method handles the connection closed event.
        /// </summary>
        /// <param name="arg">The exception that caused the connection to close.</param>
        /// <returns>The successfully completed task.</returns>
        private Task Connection_Closed(Exception arg)
        {
            System.Diagnostics.Debug.WriteLine($"Connection_Closed {arg.Message}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Connection_Reconnected method handles the reconnection event.
        /// </summary>
        /// <param name="arg">The connection Id.</param>
        /// <returns>The successfully completed task.</returns>
        private Task Connection_Reconnected(string arg)
        {
            System.Diagnostics.Debug.WriteLine($"Connection_Reconnected {arg}");
            return Task.CompletedTask;
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
        /// RecieveSensorData method sends incoming sensor data from SignalR to the MessageCenter.
        /// </summary>
        /// <param name="data">Incoming SensorData.</param>
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

        public async void storePhotoToGallery(byte[] bytes, string fileName)
        {
            System.Diagnostics.Debug.WriteLine($"storePhotoToGallery {fileName}");

            try
            {
                Java.IO.File storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
                System.Diagnostics.Debug.WriteLine($"storagePath {storagePath}");
                string path = System.IO.Path.Combine(storagePath.ToString(), fileName);
                System.IO.File.WriteAllBytes(path, bytes);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error : {ex.Message}");
            }
        }
    }
}