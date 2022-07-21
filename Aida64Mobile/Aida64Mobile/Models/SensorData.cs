namespace Aida64Mobile.Models
{
    public class SensorData
    {
        // CPU
        public int SCPUUTI { get; set; } = 0; // CPU Utilization
        public int SCPU1UTI { get; set; } = 0;
        public int SCPU2UTI { get; set; } = 0;
        public int SCPU3UTI { get; set; } = 0;
        public int SCPU4UTI { get; set; } = 0;
        public int SCPU5UTI { get; set; } = 0;
        public int SCPU6UTI { get; set; } = 0;
        public int SCPU7UTI { get; set; } = 0;
        public int SCPU8UTI { get; set; } = 0;

        // Memory
        public int SMEMUTI { get; set; } = 0; // Memory Utilization

        // GPU
        public int SGPU1MEMCLK { get; set; } = 0;
        public int SGPU1UTI { get; set; } = 0;// GPU Utilization
        public int SGPU1MCUTI { get; set; } = 0;
        public int SGPU1VEUTI { get; set; } = 0;
        public int SGPU1BIUTI { get; set; } = 0;
        public int SGPU1USEDDEMEM { get; set; } = 0;
        public int SGPU1USEDDYMEM { get; set; } = 0;
        public int SVMEMUSAGE { get; set; } = 0;
        public int SUSEDVMEM { get; set; } = 0;
        public int SFREEVMEM { get; set; } = 0;

        // Network
        public int SNICDLRATE { get; set; } = 0;
        public int SNICULRATE { get; set; } = 0;

        public float SNIC1DLRATE { get; set; } = 0;
        public float SNIC1ULRATE { get; set; } = 0;
        public float SNIC2DLRATE { get; set; } = 0;
        public float SNIC2ULRATE { get; set; } = 0;
        public float SNIC3DLRATE { get; set; } = 0;
        public float SNIC3ULRATE { get; set; } = 0;





        // Disks
        public int SDSK1ACT { get; set; } = 0;
        public float SDSK1READSPD { get; set; } = 0;
        public float SDSK1WRITESPD { get; set; } = 0;
        public int SDSK2ACT { get; set; } = 0;
        public float SDSK2READSPD { get; set; } = 0;
        public float SDSK2WRITESPD { get; set; } = 0;
        public int SDSK3ACT { get; set; } = 0;
        public float SDSK3READSPD { get; set; } = 0;
        public float SDSK3WRITESPD { get; set; } = 0;
        public int SDSK4ACT { get; set; } = 0;
        public float SDSK4READSPD { get; set; } = 0;
        public float SDSK4WRITESPD { get; set; } = 0;
        public int SDSK5ACT { get; set; } = 0;
        public float SDSK5READSPD { get; set; } = 0;
        public float SDSK5WRITESPD { get; set; } = 0;
        public int SDSK6ACT { get; set; } = 0;
        public float SDSK6READSPD { get; set; } = 0;
        public float SDSK6WRITESPD { get; set; } = 0;
        public int SDSK7ACT { get; set; } = 0;
        public float SDSK7READSPD { get; set; } = 0;
        public float SDSK7WRITESPD { get; set; } = 0;
        public int SDSK8ACT { get; set; } = 0;
        public float SDSK8READSPD { get; set; } = 0;
        public float SDSK8WRITESPD { get; set; } = 0;

        // Temps
        public float TCPU { get; set; } = 0; //CPU

        public float TGPU1DIO { get; set; } = 0; //GPU Diode

        // Fans
        public int FCPU { get; set; } = 0; // CPU fan
    }
}
