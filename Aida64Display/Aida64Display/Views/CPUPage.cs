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
    /// CPUPage ContentPage.
    /// </summary>
    public class CPUPage : BaseView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CPUPage"/> class.
        /// </summary>
        public CPUPage()
        {
            // Create and connect to the view model.
            CPUViewModel vm = new CPUViewModel();
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

        /// <inheritdoc/>
        public override void DrawDisplay(SKCanvas canvas)
        {
            SensorData[] d = ((BaseViewModel)BindingContext).DataStore.GetItems();
            int count = d.Length - 2;

            canvas.DrawBitmap(Op.GraphBack, 20, 154);

            canvas.DrawText("CPU", 100, 90, Op.LabelFont, Op.GrayPaint);

            canvas.DrawText(d[count].SMEMUTI.ToString() + "%", 1500, 120, Op.ValueFont, Op.Counter2ShadowPaint);
            canvas.DrawText(d[count].SCPUUTI.ToString() + "%", 1000, 120, Op.ValueFont, Op.Counter1ShadowPaint);

            int x1;
            int x2;

            for (int i = 1; i < Op.QueueLength; i++)
            {
                x1 = (i * 3) + 30;
                x2 = x1 + 3;

                canvas.DrawLine(x1, 1134 - (d[i - 1].SCPUUTI * 10), x2, 1134 - (d[i].SCPUUTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1132 - (d[i - 1].SCPUUTI * 10), x2, 1132 - (d[i].SCPUUTI * 10), Op.Counter1Paint);
            }
        }
    }
}