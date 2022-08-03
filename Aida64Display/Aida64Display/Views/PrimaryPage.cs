namespace Aida64Display.Views
{
    using Aida64Common.Views;

    using Aida64Display.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    /// PrimaryPage ContentPage.
    /// </summary>
    public class PrimaryPage : BaseView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimaryPage"/> class.
        /// </summary>
        public PrimaryPage()
        {
            // Create and connect to the view model.
            PrimaryViewModel vm = new PrimaryViewModel();
            BindingContext = vm;
            vm.Update += VmUpdate;
        }

        /// <inheritdoc/>
        public override void OnButtonPress(int button)
        {
            switch (button)
            {
                case 0:
                    PrimaryPage primaryPage = new PrimaryPage();
                    NavigationPage.SetHasNavigationBar(primaryPage, false);
                    _ = Navigation.PushAsync(primaryPage, false);

                    break;

                case 1:
                    CPUPage cpuPage = new CPUPage();
                    NavigationPage.SetHasNavigationBar(cpuPage, false);
                    _ = Navigation.PushAsync(cpuPage, false);

                    break;

                case 2:
                    NetworkPage networkPage = new NetworkPage();
                    NavigationPage.SetHasNavigationBar(networkPage, false);
                    _ = Navigation.PushAsync(networkPage, false);
                    break;
            }
        }
    }
}