namespace Aida64Display.Views
{
    using System.IO;
    using System.Reflection;

    using Aida64Common;
    using Aida64Common.Models;
    using Aida64Common.ViewModels;
    using Aida64Common.Views;

    using Aida64Display.ViewModels;

    using SkiaSharp;

    using Xamarin.Forms;

    /// <summary>
    /// NetworkPage ContentPage.
    /// </summary>
    public class NetworkPage : BaseView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkPage"/> class.
        /// </summary>
        public NetworkPage()
        {
            // Create and connect to the view model.
            NetworkViewModel vm = new NetworkViewModel();
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
            SensorData[] d = ((BaseViewModel)BindingContext).DataStore.GetItems();
            int count = d.Length - 2;
            SensorData c = d[count];

            canvas.DrawBitmap(Op.GraphBack, 20, 154);

            canvas.DrawText("Network", 100, 45, Op.LabelFont, Op.GrayPaint);

            canvas.DrawText((c.SNIC1ULRATE + c.SNIC2ULRATE + c.SNIC3ULRATE).ToString() + "Kb", 1500, 120, Op.ValueFont, Op.Counter2ShadowPaint);
            canvas.DrawText((c.SNIC1DLRATE + c.SNIC2DLRATE + c.SNIC3DLRATE).ToString() + "Kb", 1000, 120, Op.ValueFont, Op.Counter1ShadowPaint);

            canvas.DrawText($"NIC 1 Download {c.SNIC1DLRATE} Kb", 100, 200, Op.ValueFont, Op.Counter1ShadowPaint);
            canvas.DrawText($"NIC 1 Upload {c.SNIC1ULRATE} Kb", 100, 250, Op.ValueFont, Op.Counter1ShadowPaint);
            canvas.DrawText($"NIC 2 Download {c.SNIC2DLRATE} Kb", 100, 300, Op.ValueFont, Op.Counter1ShadowPaint);
            canvas.DrawText($"NIC 2 Upload {c.SNIC2ULRATE} Kb", 100, 350, Op.ValueFont, Op.Counter1ShadowPaint);
            canvas.DrawText($"NIC 3 Download {c.SNIC3DLRATE} Kb", 100, 400, Op.ValueFont, Op.Counter1ShadowPaint);
            canvas.DrawText($"NIC 3 Upload {c.SNIC3ULRATE} Kb", 100, 450, Op.ValueFont, Op.Counter1ShadowPaint);

            int x1;
            int x2;

            for (int i = 1; i < Op.QueueLength; i++)
            {
                x1 = (i * 3) + 30;
                x2 = x1 + 3;

                canvas.DrawLine(x1, 1134 - d[i - 1].SNIC3ULRATE, x2, 1134 - d[i].SNIC3ULRATE, Op.Counter6ShadowPaint);
                canvas.DrawLine(x1, 1132 - d[i - 1].SNIC3ULRATE, x2, 1132 - d[i].SNIC3ULRATE, Op.Counter6Paint);

                canvas.DrawLine(x1, 1134 - d[i - 1].SNIC3DLRATE, x2, 1134 - d[i].SNIC3DLRATE, Op.Counter5ShadowPaint);
                canvas.DrawLine(x1, 1132 - d[i - 1].SNIC3DLRATE, x2, 1132 - d[i].SNIC3DLRATE, Op.Counter5Paint);

                canvas.DrawLine(x1, 1134 - d[i - 1].SNIC2ULRATE, x2, 1134 - d[i].SNIC2ULRATE, Op.Counter4ShadowPaint);
                canvas.DrawLine(x1, 1132 - d[i - 1].SNIC2ULRATE, x2, 1132 - d[i].SNIC2ULRATE, Op.Counter4Paint);

                canvas.DrawLine(x1, 1134 - d[i - 1].SNIC2DLRATE, x2, 1134 - d[i].SNIC2DLRATE, Op.Counter3ShadowPaint);
                canvas.DrawLine(x1, 1132 - d[i - 1].SNIC2DLRATE, x2, 1132 - d[i].SNIC2DLRATE, Op.Counter3Paint);

                canvas.DrawLine(x1, 1134 - d[i - 1].SNIC1ULRATE, x2, 1134 - d[i].SNIC1ULRATE, Op.Counter2ShadowPaint);
                canvas.DrawLine(x1, 1132 - d[i - 1].SNIC1ULRATE, x2, 1132 - d[i].SNIC1ULRATE, Op.Counter2Paint);

                canvas.DrawLine(x1, 1134 - d[i - 1].SNIC1DLRATE, x2, 1134 - d[i].SNIC1DLRATE, Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1132 - d[i - 1].SNIC1DLRATE, x2, 1132 - d[i].SNIC1DLRATE, Op.Counter1Paint);
            }
        }
    }
}