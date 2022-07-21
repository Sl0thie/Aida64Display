namespace Aida64Service
{
    using System.IO.MemoryMappedFiles;
    using Microsoft.Management.Infrastructure;
    using System.Threading;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
 
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public static SensorData GetData()
        {
            Thread.Sleep(1000);

            SensorData data = new SensorData();

            try
            {
                string value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC1DLRATE'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SNIC1DLRATE = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC1ULRATE'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SNIC1ULRATE = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC2DLRATE'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SNIC2DLRATE = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC2ULRATE'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SNIC2ULRATE = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC3DLRATE'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SNIC3DLRATE = Convert.ToSingle(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SNIC3ULRATE'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SNIC3ULRATE = Convert.ToSingle(value);



                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SCPUUTI'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SCPUUTI = Convert.ToInt32(value);




                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'TCPU'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.TCPU = Convert.ToSingle(value);


                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SGPU1UTI'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SGPU1UTI = Convert.ToInt32(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'TGPU1DIO'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.TGPU1DIO = Convert.ToSingle(value);


                


                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SDSK7ACT'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SDSK7ACT = Convert.ToInt32(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SDSK7READSPD'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SDSK7READSPD = Convert.ToInt32(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SDSK7WRITESPD'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();
                data.SDSK7WRITESPD = Convert.ToInt32(value);

                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return data;
        }
    }
}