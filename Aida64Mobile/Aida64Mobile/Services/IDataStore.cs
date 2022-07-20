namespace Aida64Mobile.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Aida64Mobile.Models;

    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<T[]> GetItemsAsync();

        void AddItem(T item);

        T[] GetItems();
    }
}
