namespace Aida64Display
{
    using System;
    using Aida64Common;
    using Aida64Common.Services;
    using Aida64Display.Views;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// App class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();
            Op.QueueLength = 833;
            DependencyService.Register<SensorDataStore>();
            MainPage = new StartPage();
        }

        /// <summary>
        /// OnStart method.
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// OnSleep method.
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// OnResume method.
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}
