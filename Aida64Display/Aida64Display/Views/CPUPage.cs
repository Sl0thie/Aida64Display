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
    internal class CPUPage : BaseView
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
            canvas.DrawBitmap(Op.Cpu, 30, 30);

            int x1;
            int x2;

            for (int i = 1; i < Op.QueueLength; i++)
            {
                x1 = (i * 3) + 30;
                x2 = x1 + 3;

                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPU1UTI * 10), x2, 1154 - (d[i].SCPU1UTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPU2UTI * 10), x2, 1154 - (d[i].SCPU2UTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPU3UTI * 10), x2, 1154 - (d[i].SCPU3UTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPU4UTI * 10), x2, 1154 - (d[i].SCPU4UTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPU5UTI * 10), x2, 1154 - (d[i].SCPU5UTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPU6UTI * 10), x2, 1154 - (d[i].SCPU6UTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPU7UTI * 10), x2, 1154 - (d[i].SCPU7UTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPU8UTI * 10), x2, 1154 - (d[i].SCPU8UTI * 10), Op.Counter1ShadowPaint);

                canvas.DrawLine(x1, 1154 - (d[i - 1].SCPUUTI * 10), x2, 1154 - (d[i].SCPUUTI * 10), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1152 - (d[i - 1].SCPUUTI * 10), x2, 1152 - (d[i].SCPUUTI * 10), Op.Counter1Paint);
            }

            canvas.DrawText("CPU", 140, 110, Op.LabelFont, Op.GrayPaint);

            canvas.DrawText("Usage", 1050, 120, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText(d[count].SCPUUTI.ToString() + "%", 1500, 120, Op.ValueFont, Op.Counter1PaintRight);

            canvas.DrawText("Avg", 1600, 120, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText(d[count].SMEMUTI.ToString() + "%", 2000, 120, Op.ValueFont, Op.Counter1PaintRight);

            canvas.DrawText("Max", 2100, 120, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText(d[count].SMEMUTI.ToString() + "%", 2500, 120, Op.ValueFont, Op.Counter1PaintRight);

            canvas.DrawText($"Core 1", 50, 250, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText($"Core 2", 50, 350, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText($"Core 3", 50, 450, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText($"Core 4", 50, 550, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText($"Core 5", 50, 650, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText($"Core 6", 50, 750, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText($"Core 7", 50, 850, Op.LabelSubFont, Op.GrayPaint);
            canvas.DrawText($"Core 8", 50, 950, Op.LabelSubFont, Op.GrayPaint);

            canvas.DrawText($"{d[count].SCPU1UTI} %", 400, 250, Op.ValueSubFont, Op.Counter1PaintRight);
            canvas.DrawText($"{d[count].SCPU2UTI} %", 400, 350, Op.ValueSubFont, Op.Counter1PaintRight);
            canvas.DrawText($"{d[count].SCPU3UTI} %", 400, 450, Op.ValueSubFont, Op.Counter1PaintRight);
            canvas.DrawText($"{d[count].SCPU4UTI} %", 400, 550, Op.ValueSubFont, Op.Counter1PaintRight);
            canvas.DrawText($"{d[count].SCPU5UTI} %", 400, 650, Op.ValueSubFont, Op.Counter1PaintRight);
            canvas.DrawText($"{d[count].SCPU6UTI} %", 400, 750, Op.ValueSubFont, Op.Counter1PaintRight);
            canvas.DrawText($"{d[count].SCPU7UTI} %", 400, 850, Op.ValueSubFont, Op.Counter1PaintRight);
            canvas.DrawText($"{d[count].SCPU8UTI} %", 400, 950, Op.ValueSubFont, Op.Counter1PaintRight);
        }
    }
}