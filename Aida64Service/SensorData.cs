namespace Aida64Service
{
    public class SensorData
    {
        // CPU
        public int SCPUUTI { get; set; } // CPU Utilization
        public int SCPU1UTI { get; set; }
        public int SCPU2UTI { get; set; }
        public int SCPU3UTI { get; set; }
        public int SCPU4UTI { get; set; }
        public int SCPU5UTI { get; set; }
        public int SCPU6UTI { get; set; }
        public int SCPU7UTI { get; set; }
        public int SCPU8UTI { get; set; }

        // Memory
        public int SMEMUTI { get; set; } // Memory Utilization

        // GPU
        public int SGPU1MEMCLK { get; set; }
        public int SGPU1UTI { get; set; } // GPU Utilization
        public int SGPU1MCUTI { get; set; }
        public int SGPU1VEUTI { get; set; }
        public int SGPU1BIUTI { get; set; }
        public int SGPU1USEDDEMEM { get; set; }
        public int SGPU1USEDDYMEM { get; set; }
        public int SVMEMUSAGE { get; set; }
        public int SUSEDVMEM { get; set; }
        public int SFREEVMEM { get; set; }

        // Network
        public int SNICDLRATE { get; set; } = 0;
        public int SNICULRATE { get; set; } = 0;
        public float SNIC1DLRATE { get; set; }
        public float SNIC1ULRATE { get; set; }
        public float SNIC2DLRATE { get; set; }
        public float SNIC2ULRATE { get; set; }
        public float SNIC3DLRATE { get; set; }
        public float SNIC3ULRATE { get; set; }

        // Disks
        public int SDSK1ACT { get; set; }
        public float SDSK1READSPD { get; set; }
        public float SDSK1WRITESPD { get; set; }
        public int SDSK2ACT { get; set; }
        public float SDSK2READSPD { get; set; }
        public float SDSK2WRITESPD { get; set; }
        public int SDSK3ACT { get; set; }
        public float SDSK3READSPD { get; set; }
        public float SDSK3WRITESPD { get; set; }
        public int SDSK4ACT { get; set; }
        public float SDSK4READSPD { get; set; }
        public float SDSK4WRITESPD { get; set; }
        public int SDSK5ACT { get; set; }
        public float SDSK5READSPD { get; set; }
        public float SDSK5WRITESPD { get; set; }
        public int SDSK6ACT { get; set; }
        public float SDSK6READSPD { get; set; }
        public float SDSK6WRITESPD { get; set; }
        public int SDSK7ACT { get; set; }
        public float SDSK7READSPD { get; set; }
        public float SDSK7WRITESPD { get; set; }
        public int SDSK8ACT { get; set; }
        public float SDSK8READSPD { get; set; }
        public float SDSK8WRITESPD { get; set; }

        // Temps
        public float TCPU { get; set; } //CPU

        public float TGPU1DIO { get; set; } //GPU Diode

        // Fans
        public int FCPU { get; set; } // CPU fan
    }
}
