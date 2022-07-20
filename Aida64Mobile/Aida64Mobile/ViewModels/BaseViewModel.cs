namespace Aida64Mobile.ViewModels
{
    using Aida64Mobile.Services;
    using Aida64Mobile.Models;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Xamarin.Forms;
using System.Diagnostics;
using Aida64Mobile.Views;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<SensorData> DataStore => DependencyService.Get<IDataStore<SensorData>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public delegate void UpdateEventHandler(object sender);
        public event UpdateEventHandler Update;

        public BaseViewModel()
        {

            _ = DataStore.GetItems();

            MessagingCenter.Subscribe<SensorData>(this, "RecieveSensorData", (data) =>
            {
                //System.Diagnostics.Debug.WriteLine("RecieveSensorData");

                DataStore.AddItem(data);
                Update?.Invoke(this);
            });
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
