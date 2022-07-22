namespace Aida64Mobile.Services
{
    using Aida64Mobile.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SensorDataStore : IDataStore<SensorData>
    {
        private readonly Queue<SensorData> items = new Queue<SensorData>();

        public SensorDataStore()
        {
            while (items.Count < Op.MaxValues)
            {
                items.Enqueue(new SensorData());
            }
        }

        public void AddItem(SensorData item)
        {
            if(item.SNIC2DLRATE > 3000)
            {
                item.SNICDLRATE = 300;
            }
            else
            {
                item.SNICDLRATE = item.SNIC2DLRATE / 10;
            }

            if (item.SNIC2ULRATE > 3000)
            {
                item.SNICULRATE = 300;
            }
            else
            {
                item.SNICULRATE = item.SNIC2ULRATE / 10;
            }

            if (item.SDSK7READSPD > 300)
            {
                item.SDSKREADSPD = 300;
            }
            else
            {
                item.SDSKREADSPD = item.SDSK7READSPD;
            }

            if (item.SDSK7WRITESPD > 300)
            {
                item.SDSKWRITESPD = 300;
            }
            else
            {
                item.SDSKWRITESPD = item.SDSK7WRITESPD;
            }




            items.Enqueue(item);

            if (items.Count > Op.MaxValues)
            {
                _ = items.Dequeue();
            }           
        }

        public SensorData[] GetItems()
        {
            return items.ToArray();
        }

        public async Task<bool> AddItemAsync(SensorData item)
        {
            if (items.Count > Op.MaxValues)
            {
                _ = items.Dequeue();
            }

            items.Enqueue(item);

            return await Task.FromResult(true);
        }

        public async Task<SensorData[]> GetItemsAsync()
        {
            return await Task.FromResult(items.ToArray());
        }
    }
}
