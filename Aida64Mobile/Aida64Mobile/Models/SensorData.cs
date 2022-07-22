namespace Aida64Mobile.Models
{
    public class SensorData
    {
        // CPU
        public float SCPUUTI { get; set; } // CPU Utilization
        public float SCPU1UTI { get; set; }
        public float SCPU2UTI { get; set; }
        public float SCPU3UTI { get; set; }
        public float SCPU4UTI { get; set; }
        public float SCPU5UTI { get; set; }
        public float SCPU6UTI { get; set; }
        public float SCPU7UTI { get; set; }
        public float SCPU8UTI { get; set; }

        // Memory
        public float SMEMUTI { get; set; } // Memory Utilization

        // GPU
        public float SGPU1MEMCLK { get; set; }
        public float SGPU1UTI { get; set; } // GPU Utilization
        public float SGPU1MCUTI { get; set; }
        public float SGPU1VEUTI { get; set; }
        public float SGPU1BIUTI { get; set; }
        public float SGPU1USEDDEMEM { get; set; }
        public float SGPU1USEDDYMEM { get; set; }
        public float SVMEMUSAGE { get; set; }
        public float SUSEDVMEM { get; set; }
        public float SFREEVMEM { get; set; }

        // Network
        public float SNICDLRATE { get; set; } = 0;
        public float SNICULRATE { get; set; } = 0;
        public float SNIC1DLRATE { get; set; }
        public float SNIC1ULRATE { get; set; }
        public float SNIC2DLRATE { get; set; }
        public float SNIC2ULRATE { get; set; }
        public float SNIC3DLRATE { get; set; }
        public float SNIC3ULRATE { get; set; }

        // Disks
        public float SDSKREADSPD { get; set; }
        public float SDSKWRITESPD { get; set; }

        public float SDSK1ACT { get; set; }
        public float SDSK1READSPD { get; set; }
        public float SDSK1WRITESPD { get; set; }
        public float SDSK2ACT { get; set; }
        public float SDSK2READSPD { get; set; }
        public float SDSK2WRITESPD { get; set; }
        public float SDSK3ACT { get; set; }
        public float SDSK3READSPD { get; set; }
        public float SDSK3WRITESPD { get; set; }
        public float SDSK4ACT { get; set; }
        public float SDSK4READSPD { get; set; }
        public float SDSK4WRITESPD { get; set; }
        public float SDSK5ACT { get; set; }
        public float SDSK5READSPD { get; set; }
        public float SDSK5WRITESPD { get; set; }
        public float SDSK6ACT { get; set; }
        public float SDSK6READSPD { get; set; }
        public float SDSK6WRITESPD { get; set; }
        public float SDSK7ACT { get; set; }
        public float SDSK7READSPD { get; set; }
        public float SDSK7WRITESPD { get; set; }
        public float SDSK8ACT { get; set; }
        public float SDSK8READSPD { get; set; }
        public float SDSK8WRITESPD { get; set; }

        // Temps
        public float TCPU { get; set; } //CPU

        public float TGPU1DIO { get; set; } //GPU Diode

        // Fans
        public float FCPU { get; set; } // CPU fan
    }
}
