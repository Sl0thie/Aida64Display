namespace Aida64Display.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Aida64Common;
    using Aida64Common.Models;
    using Aida64Common.ViewModels;
    using Aida64Common.Views;
    using Aida64Display.ViewModels;
    using SkiaSharp;
    using Xamarin.Forms;

    /// <summary>
    /// TempraturesPage content page.
    /// </summary>
    public class TempraturesPage : BaseView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TempraturesPage"/> class.
        /// </summary>
        public TempraturesPage()
        {
            // Create and connect to the view model.
            TempraturesViewModel vm = new TempraturesViewModel();
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