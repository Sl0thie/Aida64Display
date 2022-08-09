namespace Aida64Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Aida64Common.Models;
    using Aida64Common.Services;

    using Xamarin.Forms;

    /// <summary>
    /// BaseViewModel class.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged, IViewModel
    {
        /// <inheritdoc/>
        public IDataStore<SensorData> DataStore
        {
            get
            {
                return DependencyService.Get<IDataStore<SensorData>>();
            }
        }

        /// <inheritdoc/>
        public event EventHandler Update;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        public BaseViewModel()
        {
            _ = DataStore.GetItems();
            MessagingCenter.Unsubscribe<SensorData>(this, "RecieveSensorData");
            MessagingCenter.Subscribe<SensorData>(this, "RecieveSensorData", (data) => RecieveData(data));

            ControlMessage controlMessage = new ControlMessage("FrameFinished");
            MessagingCenter.Send(controlMessage, "FrameFinished");
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseViewModel"/> class.
        /// Un-subscribes from the messaging center.
        /// </summary>
        ~BaseViewModel()
        {
            MessagingCenter.Unsubscribe<SensorData>(this, "RecieveSensorData");
        }

        private void RecieveData(SensorData data)
        {
            DataStore.AddItem(data);

            if (Update is object)
            {
                Update?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// SetProperty method set the property and calls OnPropertyChanged if required.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="backingStore">The backing store.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="onChanged">The OnChanged parmeter.</param>
        /// <returns>Returns true if the property is changed.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// PropertyChanged event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// OnPropertyChanged method invokes the event if required.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
