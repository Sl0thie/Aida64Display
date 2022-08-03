namespace Aida64Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Aida64Common.Models;
    using Aida64Common.Services;

    /// <summary>
    /// IViewModel interface.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Gets the DataStore interface.
        /// </summary>
        IDataStore<SensorData> DataStore { get; }

        /// <summary>
        /// Update event handler fires when new data is added.
        /// </summary>
        event EventHandler Update;
    }
}