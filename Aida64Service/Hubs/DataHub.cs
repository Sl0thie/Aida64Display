namespace Aida64Service.Hubs
{
    using Aida64Common.Models;

    using Microsoft.AspNetCore.SignalR;

    using Serilog;

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
                await Clients.All.SendAsync("ReceiveData", data);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }
    }
}