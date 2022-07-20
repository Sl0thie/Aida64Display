namespace Aida64Service.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class DataHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendData()
        {
            SensorData sensorData = Worker.GetData();
            await Clients.All.SendAsync("ReceiveData", sensorData);  
        }
    }
}
