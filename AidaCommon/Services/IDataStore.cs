namespace Aida64Common.Services
{
    using System.Threading.Tasks;

    /// <summary>
    /// IDataStore interface manages a queue of data items.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public interface IDataStore<T>
    {
        /// <summary>
        /// AddItem adds an item to the queue.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void AddItem(T item);

        /// <summary>
        /// GetItems get an array of items from the queue.
        /// </summary>
        /// <returns>An array of items.</returns>
        T[] GetItems();
    }
}
