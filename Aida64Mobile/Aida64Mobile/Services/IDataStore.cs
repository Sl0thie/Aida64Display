namespace Aida64Mobile.Services
{
    using System.Threading.Tasks;

    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<T[]> GetItemsAsync();

        void AddItem(T item);

        T[] GetItems();
    }
}
