namespace Aida64Display.Views
{
    using System;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// StartPage class.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartPage"/> class.
        /// </summary>
        public StartPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// OnAppearing method starts a timer the navigates to the CPU page when it expires.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                CPUPage newpage = new CPUPage();
                NavigationPage.SetHasNavigationBar(newpage, false);
                _ = Navigation.PushAsync(newpage, false);
                return false;
            });
        }
    }
}