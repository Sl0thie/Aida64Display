namespace Aida64Mobile.ViewModels
{
    using Aida64Mobile.Services;
    using Aida64Mobile.Models;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Xamarin.Forms;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<SensorData> DataStore
        {
            get
            {
                return DependencyService.Get<IDataStore<SensorData>>();
            }
        }

        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { _ = SetProperty(ref isBusy, value); }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { _ = SetProperty(ref title, value); }
        }

        public delegate void UpdateEventHandler(object sender);
        public event UpdateEventHandler Update;

        public BaseViewModel()
        {
            _ = DataStore.GetItems();
            MessagingCenter.Subscribe<SensorData>(this, "RecieveSensorData", (data) =>
            {
                DataStore.AddItem(data);
                Update?.Invoke(this);
            });
        }

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
        public event PropertyChangedEventHandler PropertyChanged;
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
