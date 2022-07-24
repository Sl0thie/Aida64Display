namespace Aida64Service
{
    // https://stackoverflow.com/questions/68055247/accessing-a-hubcontext-instance-in-asp-net-core-signalr
    // https://docs.microsoft.com/en-us/aspnet/core/signalr/hubcontext?view=aspnetcore-5.0
    // https://stackoverflow.com/questions/54459253/asp-net-core-signalr-acces-hub-method-from-anywhere


    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Management.Infrastructure;
    using System.Threading;
    using System.Timers;
    using Aida64Service.Hubs;
    using Serilog;

    public class Worker : BackgroundService
    {
        private static System.Timers.Timer timer;
        private static IHubContext<DataHub> _hubContext;
        private static DateTime expire = DateTime.Now.AddMinutes(1);

        public static DateTime Expire
        {
            get { return expire; }
            set 
            { 
                expire = value;
                if (!timer.Enabled)
                {
                    timer.Enabled = true;
                }
            }
        }

        public Worker(IHubContext<DataHub> hub)
        {
            try
            {
                _hubContext = hub;

                timer = new System.Timers.Timer(1000);
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = true;
                timer.Enabled = true;
                timer.Start();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }          
        }

        private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (DateTime.Now < expire)
                {
                    SensorData data = GetData();
                    await _hubContext.Clients.All.SendAsync("ReceiveData", data);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }       
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public static SensorData GetData()
        {
            SensorData data = new();

            try
            {
                string? value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC1DLRATE'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SNIC1DLRATE = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC1ULRATE'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SNIC1ULRATE = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC2DLRATE'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SNIC2DLRATE = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC2ULRATE'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SNIC2ULRATE = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC3DLRATE'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SNIC3DLRATE = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC3ULRATE'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SNIC3ULRATE = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SCPUUTI'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SCPUUTI = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SMEMUTI'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SMEMUTI = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SGPU1UTI'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SGPU1UTI = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SGPU1USEDDEMEM'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SGPU1USEDDEMEM = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'TCPU'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.TCPU = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'TGPU1DIO'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.TGPU1DIO = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SDSK7READSPD'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SDSK7READSPD = Convert.ToSingle(value);
                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SDSK7WRITESPD'").FirstOrDefault()?.CimInstanceProperties["Value"].Value.ToString();
                data.SDSK7WRITESPD = Convert.ToSingle(value);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }

            return data;
        }
    }
}