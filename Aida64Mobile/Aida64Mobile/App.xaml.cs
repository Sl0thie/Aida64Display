namespace Aida64Mobile
{
    using Aida64Common;
    using Aida64Common.Services;
    using Aida64Mobile.Views;

    using Xamarin.Forms;

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
            Op.QueueLength = 252;
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
