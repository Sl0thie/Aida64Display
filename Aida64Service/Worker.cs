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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public static SensorData GetData()
        {
            Thread.Sleep(1000);

            SensorData data = new SensorData();

            try
            {
                string value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'SCPUUTI'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();

                data.SCPUUTI = Convert.ToInt32(value);

                value = CimSession.Create(null).QueryInstances(@"Root\WMI", "WQL", "SELECT * FROM AIDA64_SensorValues WHERE ID = 'TCPU'").FirstOrDefault().CimInstanceProperties["Value"].Value.ToString();

                data.TCPU = Convert.ToSingle(value);



                //// Local testing.
                //MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("Global\\AIDA64_SensorValues");

                //// IIS 
                ////MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("AIDA64_SensorValues", MemoryMappedFileRights.Read, System.IO.HandleInheritability.Inheritable);

                //MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor();

                //string xml = "";

                //for (int i = 0; i < accessor.Capacity; i++)
                //{
                //    xml = xml + (char)accessor.ReadByte(i);
                //}

                ////string xml = (string)Marshal.PtrToStringAnsi(map);
                ////System.Diagnostics.Debug.WriteLine(xml);

                ////SELECT * FROM AIDA64_SensorValues WHERE ID = 'SCPUUTI'

                //xml = xml.Substring(xml.IndexOf("SCPUUTI") + 49);
                //string value = xml.Substring(0, xml.IndexOf("</value>"));
                ////Debug.WriteLine(value);
                //data.SCPUUTI = Convert.ToInt32(value);

                //xml = xml.Substring(xml.IndexOf("SMEMUTI") + 52);
                //value = xml.Substring(0, xml.IndexOf("</value>"));
                ////Debug.WriteLine(value);
                //data.SMEMUTI = Convert.ToInt32(value);

                //xml = xml.Substring(xml.IndexOf("SGPU1UTI") + 50);
                //value = xml.Substring(0, xml.IndexOf("</value>"));
                ////Debug.WriteLine(value);
                //data.SGPU1UTI = Convert.ToInt32(value);

                //xml = xml.Substring(xml.IndexOf("TCPU") + 34);
                //value = xml.Substring(0, xml.IndexOf("</value>"));
                ////Debug.WriteLine(value);
                //data.TCPU = Convert.ToSingle(value);

                //xml = xml.Substring(xml.IndexOf("TGPU1DIO") + 44);
                //value = xml.Substring(0, xml.IndexOf("</value>"));
                ////Debug.WriteLine(value);
                //data.TGPU1DIO = Convert.ToSingle(value);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return data;
        }
    }
}