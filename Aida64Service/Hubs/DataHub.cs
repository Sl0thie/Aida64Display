namespace Aida64Service.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Serilog;

    public class DataHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendData()
        {
            try
            {
                Worker.Expire = DateTime.Now.AddMinutes(1);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

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