namespace Aida64Mobile
{
    using System;

    using Aida64Mobile.Services;
    using Aida64Mobile.Views;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<SensorDataStore>();
            MainPage = new StartPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
