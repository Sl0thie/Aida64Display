namespace Aida64Service.Hubs
{
    using System;
    using System.IO;
    using System.Threading.Channels;
    using Aida64Common.Models;
    using Microsoft.AspNetCore.SignalR;
    using Serilog;
    using Xamarin.Forms.Shapes;

/// <summary>
/// DataHub class.
/// </summary>
    public class DataHub : Hub
    {
        /// <summary>
        /// SendData method.
        /// </summary>
        /// <returns>The completed task.</returns>
        public Task SendData()
        {
            // TODO rewrite this.
            try
            {
                Worker.Expire = DateTime.Now.AddMinutes(1);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// PassData method sends SensorData to all the clients.
        /// </summary>
        /// <param name="data">The SensorData to send.</param>
        /// <returns>The completed task.</returns>
        public async Task PassData(SensorData data)
        {
            try
            {
                Log.Information("PassData");

                await Clients.All.SendAsync("ReceiveData", data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// UploadImageData method is used to upload image data from the display camera.
        /// </summary>
        /// <param name="stream">The stream from the clint.</param>
        /// <returns>A task indication the success of the method.</returns>
        public async Task UploadImageData(IAsyncEnumerable<byte[]> stream)
        {
            Log.Information("UploadImageData");

            try
            {
                byte[] data = Array.Empty<byte>();

                await foreach (byte[] item in stream)
                {
                    data = Combine(data, item);
                }

                File.WriteAllBytes($"C:\\photos\\{DateTime.Now:yyyy-MM-dd-hh-mm-ss}.jpg", data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }
    }
}