namespace Aida64SignalR
{
    using System;
    using System.IO;
    using System.IO.MemoryMappedFiles;
    using System.Reflection.Metadata;
    using System.Runtime.InteropServices;
    using System.Security.AccessControl;
    using System.Security.Principal;
    using System.Text;
    public sealed class SharedMemory
    {
        private static readonly SharedMemory current = new SharedMemory();

        // https://stackoverflow.com/questions/20363839/opening-a-memory-mapped-file-causes-filenotfoundexception-when-deployed-in-iis



        static SharedMemory()
        {

        }

        private SharedMemory()
        {

        }

        public static SharedMemory Current
        {
            get
            {
                return current;
            }
        }

        public SensorData GetData()
        {
            SensorData data = new SensorData();

            try
            {
                // Local testing.
                //MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("AIDA64_SensorValues");

                // IIS 
                MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("Global\\AIDA64_SensorValues", MemoryMappedFileRights.Read, System.IO.HandleInheritability.Inheritable);

                MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor();

                string xml = "";

                for (int i = 0; i < accessor.Capacity; i++)
                {
                    xml = xml + (char)accessor.ReadByte(i);
                }

                //string xml = (string)Marshal.PtrToStringAnsi(map);
                //System.Diagnostics.Debug.WriteLine(xml);

                xml = xml.Substring(xml.IndexOf("SCPUUTI") + 49);
                string value = xml.Substring(0, xml.IndexOf("</value>"));
                //Debug.WriteLine(value);
                data.SCPUUTI = Convert.ToInt32(value);

                xml = xml.Substring(xml.IndexOf("SMEMUTI") + 52);
                value = xml.Substring(0, xml.IndexOf("</value>"));
                //Debug.WriteLine(value);
                data.SMEMUTI = Convert.ToInt32(value);

                xml = xml.Substring(xml.IndexOf("SGPU1UTI") + 50);
                value = xml.Substring(0, xml.IndexOf("</value>"));
                //Debug.WriteLine(value);
                data.SGPU1UTI = Convert.ToInt32(value);

                xml = xml.Substring(xml.IndexOf("TCPU") + 34);
                value = xml.Substring(0, xml.IndexOf("</value>"));
                //Debug.WriteLine(value);
                data.TCPU = Convert.ToSingle(value);

                xml = xml.Substring(xml.IndexOf("TGPU1DIO") + 44);
                value = xml.Substring(0, xml.IndexOf("</value>"));
                //Debug.WriteLine(value);
                data.TGPU1DIO = Convert.ToSingle(value);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return data;
        }
    }
}
