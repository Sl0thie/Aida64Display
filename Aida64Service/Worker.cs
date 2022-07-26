// https://stackoverflow.com/questions/68055247/accessing-a-hubcontext-instance-in-asp-net-core-signalr
// https://docs.microsoft.com/en-us/aspnet/core/signalr/hubcontext?view=aspnetcore-5.0
// https://stackoverflow.com/questions/54459253/asp-net-core-signalr-acces-hub-method-from-anywhere
namespace Aida64Service
{
    using System.Threading;
    using System.Timers;
    using Aida64Common.Models;
    using Aida64Service.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Management.Infrastructure;
    using Serilog;

    /// <summary>
    /// Worker class.
    /// </summary>
    public class Worker : BackgroundService
    {
        private static System.Timers.Timer timer;
        private static IHubContext<DataHub> hubContext;
        private static DateTime expire = DateTime.Now.AddMinutes(1);

        /// <summary>
        /// Gets or sets the expiry time.
        /// </summary>
        public static DateTime Expire
        {
            get
            {
                return expire;
            }

            set
            {
                expire = value;
                if (!timer.Enabled)
                {
                    timer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="hub">A reference to the SignalR hub.</param>
        public Worker(IHubContext<DataHub> hub)
        {
            try
            {
                hubContext = hub;

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

        private static async void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            try
            {
                if (DateTime.Now < expire)
                {
                    SensorData data = GetData();
                    await hubContext.Clients.All.SendAsync("ReceiveData", data);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// ExecuteAsync method is called when the Microsoft.Extensions.Hosting.IHostedService starts. The implementation should return a task that represents the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken) is called.</param>
        /// <returns>A System.Threading.Tasks.Task that represents the long running operations.</returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets the SensorData from the WMI.
        /// </summary>
        /// <returns>The values from Aida64.</returns>
        public static SensorData GetData()
        {
            SensorData data = new ();

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