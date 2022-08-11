// https://stackoverflow.com/questions/14306048/controlling-volume-mixer
// https://github.com/filoe/cscore
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
            SensorData[] d = ((BaseViewModel)BindingContext).DataStore.GetItems();
            int count = d.Length - 2;

            canvas.DrawBitmap(Op.GraphBack, 20, 154);
            canvas.DrawBitmap(Op.Temp, 30, 30);

            int x1;
            int x2;

            for (int i = 1; i < Op.QueueLength; i++)
            {
                x1 = (i * 3) + 30;
                x2 = x1 + 3;

                canvas.DrawLine(x1, 1144 - (d[i - 1].THDD1 * 10), x2, 1144 - (d[i].THDD1 * 10), Op.Counter1Paint);
                canvas.DrawLine(x1, 1142 - (d[i - 1].TMOBO * 10), x2, 1142 - (d[i].TMOBO * 10), Op.Counter2Paint);
                canvas.DrawLine(x1, 1144 - (d[i - 1].TCPU * 10), x2, 1144 - (d[i].TCPU * 10), Op.Counter3Paint);
                canvas.DrawLine(x1, 1142 - (d[i - 1].TGPU1DIO * 10), x2, 1142 - (d[i].TGPU1DIO * 10), Op.Counter4Paint);
            }

            canvas.DrawText("Temperatures", 140, 110, Op.LabelFont, Op.GrayPaint);
            //canvas.DrawText(d[count].SMEMUTI.ToString() + "%", 1500, 120, Op.ValueFont, Op.Counter1PaintRight);
            //canvas.DrawText(d[count].SCPUUTI.ToString() + "%", 1000, 120, Op.ValueFont, Op.Counter1PaintRight);

            canvas.DrawText($"CPU", 50, 250, Op.ValueSubFont, Op.GrayPaint);
            canvas.DrawText($"Mobo", 50, 350, Op.ValueSubFont, Op.GrayPaint);
            canvas.DrawText($"GPU", 50, 430, Op.ValueSubFont, Op.GrayPaint);
            canvas.DrawText($"HDD", 50, 510, Op.ValueSubFont, Op.GrayPaint);

            canvas.DrawText($"{d[count].TCPU} %", 400, 250, Op.ValueSubFont, Op.Counter1PaintRight);
            canvas.DrawText($"{d[count].TMOBO} %", 400, 350, Op.ValueSubFont, Op.Counter2PaintRight);
            canvas.DrawText($"{d[count].TGPU1DIO} %", 400, 430, Op.ValueSubFont, Op.Counter3PaintRight);
            canvas.DrawText($"{d[count].THDD1} %", 400, 510, Op.ValueSubFont, Op.Counter4PaintRight);
        }
    }
}