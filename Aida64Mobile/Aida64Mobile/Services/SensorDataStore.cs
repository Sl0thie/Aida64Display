﻿namespace Aida64Mobile.Services
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
            if (items.Count > Op.MaxValues)
            {
                _ = items.Dequeue();
            }

            items.Enqueue(item);
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