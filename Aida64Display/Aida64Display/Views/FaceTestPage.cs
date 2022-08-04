namespace Aida64Display.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Aida64Common.Views;
    using Aida64Display.ViewModels;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using SkiaSharp;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// FaceTestPage content page.
    /// </summary>
    public class FaceTestPage : BaseView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FaceTestPage"/> class.
        /// </summary>
        public FaceTestPage()
        {
            // Create and connect to the view model.
            FaceTestViewModel vm = new FaceTestViewModel();
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

                case 3:
                    TempraturesPage tempraturesPage = new TempraturesPage();
                    NavigationPage.SetHasNavigationBar(tempraturesPage, false);
                    _ = Navigation.PushAsync(tempraturesPage, false);

                    break;

                case 4:
                    VolumeMixerPage volumeMixerPage = new VolumeMixerPage();
                    NavigationPage.SetHasNavigationBar(volumeMixerPage, false);
                    _ = Navigation.PushAsync(volumeMixerPage, false);

                    break;

                case 5:
                    FaceTestPage faceTestPage = new FaceTestPage();
                    NavigationPage.SetHasNavigationBar(faceTestPage, false);
                    _ = Navigation.PushAsync(faceTestPage, false);

                    break;
            }
        }

        /// <inheritdoc/>
        public override void DrawDisplay(SKCanvas canvas)
        {
        }
    }
}