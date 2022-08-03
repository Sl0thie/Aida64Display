namespace Aida64Common.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Aida64Common.Models;

    /// <summary>
    /// SensorDataStore class.
    /// </summary>
    public class SensorDataStore : IDataStore<SensorData>
    {
        // A queue to manage the data items.
        private readonly Queue<SensorData> items = new Queue<SensorData>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorDataStore"/> class.
        /// </summary>
        public SensorDataStore()
        {
            while (items.Count < Op.QueueLength)
            {
                items.Enqueue(new SensorData());
            }
        }

        /// <summary>
        /// AddItem method adds an item to the queue.
        /// </summary>
        /// <param name="item">The data item to add.</param>
        public void AddItem(SensorData item)
        {
            if (item != null)
            {
                if (item.SNIC2DLRATE > 3000)
                {
                    item.SNICDLRATE = 100;
                }
                else
                {
                    item.SNICDLRATE = item.SNIC2DLRATE / 10;
                }

                if (item.SNIC2ULRATE > 3000)
                {
                    item.SNICULRATE = 100;
                }
                else
                {
                    item.SNICULRATE = item.SNIC2ULRATE / 10;
                }

                if (item.SDSK7READSPD > 300)
                {
                    item.SDSKREADSPD = 100;
                }
                else
                {
                    item.SDSKREADSPD = item.SDSK7READSPD;
                }

                if (item.SDSK7WRITESPD > 300)
                {
                    item.SDSKWRITESPD = 100;
                }
                else
                {
                    item.SDSKWRITESPD = item.SDSK7WRITESPD;
                }

                items.Enqueue(item);

                if (items.Count > Op.QueueLength)
                {
                    _ = items.Dequeue();
                }

                if (item.SGPU1USEDDEMEM > 6000)
                {
                    item.SGPU1USEDDEMEM = 100;
                }
                else
                {
                    item.SGPUUSEDDEMEM = item.SGPU1USEDDEMEM / 20;
                }

                items.Enqueue(item);

                if (items.Count > Op.QueueLength)
                {
                    _ = items.Dequeue();
                }
            }
        }

        /// <summary>
        /// GetItems method gets an array of the queue items.
        /// </summary>
        /// <returns>An array of the queue items.</returns>
        public SensorData[] GetItems()
        {
            return items.ToArray();
        }
    }
}
