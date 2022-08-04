namespace Aida64Mobile.Views
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using Aida64Common;
    using Aida64Common.Models;
    using Aida64Common.ViewModels;

    using Aida64Mobile.ViewModels;

    using SkiaSharp;
    using SkiaSharp.Views.Forms;

    using TouchTracking;

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

            NavigationPage.SetHasNavigationBar(this, false);

            // Create and connect to the view model.
            StartViewModel vm = new StartViewModel();
            BindingContext = vm;
            vm.Update += Vm_Update;
        }

        /// <summary>
        /// Vm_Update method is used to redraw the canvas when the veiw model calls this method.
        /// </summary>
        /// <param name="sender">The object where the event originated.</param>
        /// <param name="e">The arguements for the event.</param>
        private void Vm_Update(object sender, EventArgs e)
        {
            canvasView.InvalidateSurface();
        }

        /// <summary>
        /// OnTouchEffectAction method handles the touch events.
        /// </summary>
        /// <param name="sender">The object where the event originated.</param>
        /// <param name="args">The arguements for the event.</param>
        private void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            switch (args.Type)
            {
                case TouchActionType.Pressed:

                    System.Diagnostics.Debug.WriteLine($"Location {args.Location.X} {args.Location.Y}");

                    break;

                case TouchActionType.Moved:

                    break;

                case TouchActionType.Released:

                    break;

                case TouchActionType.Cancelled:

                    break;
            }
        }

        /// <summary>
        /// OnCanvasViewPaintSurface method redraws the canvas.
        ///
        /// Samsung S6     1440x2560.
        /// Samsung S9/S9+ 1440x2960.
        ///
        /// </summary>
        /// <param name="sender">The object where the event originated.</param>
        /// <param name="args">The arguements for the event.</param>
        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(SKColors.Black);

            SensorData[] d = ((BaseViewModel)BindingContext).DataStore.GetItems();
            int count = d.Length - 2;
            int margin = (args.Info.Width - Op.GraphBackground.Width) / 2;
            int rightmargin = args.Info.Width - margin;

            canvas.DrawBitmap(Op.GraphBackground, margin, 100);
            canvas.DrawBitmap(Op.GraphBackground, margin, 530);
            canvas.DrawBitmap(Op.GraphBackground, margin, 960);
            canvas.DrawBitmap(Op.GraphBackground, margin, 1390);
            canvas.DrawBitmap(Op.GraphBackground, margin, 1820);

            canvas.DrawBitmap(Op.Cpu, margin, 10);
            canvas.DrawBitmap(Op.Hd, margin, 440);
            canvas.DrawBitmap(Op.Net, margin, 870);
            canvas.DrawBitmap(Op.Gpu, margin, 1290);
            canvas.DrawBitmap(Op.Temp, margin, 1720);

            // canvas.DrawText($"width {args.Info.Width} height {args.Info.Height} count {count}", 1, 900, label, white);
            canvas.DrawText("Memory/CPU", margin + 100, 45, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText("Utilisation", margin + 100, 90, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText(d[count].SMEMUTI.ToString() + " %", rightmargin - 320, 90, Op.ValueFont, Op.Counter2ShadowPaint);
            canvas.DrawText(d[count].SCPUUTI.ToString() + " %", rightmargin, 90, Op.ValueFont, Op.Counter1ShadowPaint);

            canvas.DrawText("Disk", margin + 90, 465, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText("Write/Read", margin + 90, 510, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText(d[count].SDSK7WRITESPD.ToString() + " Mb", rightmargin - 320, 520, Op.ValueFont, Op.Counter2ShadowPaint);
            canvas.DrawText(d[count].SDSK7READSPD.ToString() + " Mb", rightmargin, 520, Op.ValueFont, Op.Counter1ShadowPaint);

            canvas.DrawText("Network", margin + 110, 905, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText("Upload/Download", margin + 110, 950, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText(d[count].SNIC2ULRATE.ToString() + " Kb", rightmargin - 320, 950, Op.ValueFont, Op.Counter2ShadowPaint);
            canvas.DrawText(d[count].SNIC2DLRATE.ToString() + " Kb", rightmargin, 950, Op.ValueFont, Op.Counter1ShadowPaint);

            canvas.DrawText("Memory/GPU", margin + 130, 1335, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText("Utilisation", margin + 130, 1380, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText(d[count].SGPU1USEDDEMEM.ToString() + " Mb", rightmargin - 320, 1380, Op.ValueFont, Op.Counter2ShadowPaint);
            canvas.DrawText(d[count].SGPU1UTI.ToString() + " %", rightmargin, 1380, Op.ValueFont, Op.Counter1ShadowPaint);

            canvas.DrawText("GPU/CPU", margin + 60, 1755, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText("Temperature", margin + 60, 1810, Op.LabelFont, Op.GrayPaint);
            canvas.DrawText(d[count].TGPU1DIO.ToString() + " c", rightmargin - 320, 1810, Op.ValueFont, Op.Counter2ShadowPaint);
            canvas.DrawText(d[count].TCPU.ToString() + " c", rightmargin, 1810, Op.ValueFont, Op.Counter1ShadowPaint);

            int x1;
            int x2;

            for (int i = 1; i < Op.QueueLength; i++)
            {
                x1 = (i * 4) + margin + 3;
                x2 = x1 + 4;

                canvas.DrawLine(x1, 410 - (d[i - 1].SMEMUTI * 3), x2, 410 - (d[i].SMEMUTI * 3), Op.Counter2ShadowPaint);
                canvas.DrawLine(x1, 408 - (d[i - 1].SMEMUTI * 3), x2, 408 - (d[i].SMEMUTI * 3), Op.Counter2Paint);
                canvas.DrawLine(x1, 410 - (d[i - 1].SCPUUTI * 3), x2, 410 - (d[i].SCPUUTI * 3), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 408 - (d[i - 1].SCPUUTI * 3), x2, 408 - (d[i].SCPUUTI * 3), Op.Counter1Paint);

                canvas.DrawLine(x1, 840 - d[i - 1].SDSKWRITESPD, x2, 840 - d[i].SDSKWRITESPD, Op.Counter2ShadowPaint);
                canvas.DrawLine(x1, 838 - d[i - 1].SDSKWRITESPD, x2, 838 - d[i].SDSKWRITESPD, Op.Counter2Paint);
                canvas.DrawLine(x1, 840 - d[i - 1].SDSKREADSPD, x2, 840 - d[i].SDSKREADSPD, Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 838 - d[i - 1].SDSKREADSPD, x2, 838 - d[i].SDSKREADSPD, Op.Counter1Paint);

                canvas.DrawLine(x1, 1270 - d[i - 1].SNICULRATE, x2, 1270 - d[i].SNICULRATE, Op.Counter2ShadowPaint);
                canvas.DrawLine(x1, 1268 - d[i - 1].SNICULRATE, x2, 1268 - d[i].SNICULRATE, Op.Counter2Paint);
                canvas.DrawLine(x1, 1270 - d[i - 1].SNICDLRATE, x2, 1270 - d[i].SNICDLRATE, Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1268 - d[i - 1].SNICDLRATE, x2, 1268 - d[i].SNICDLRATE, Op.Counter1Paint);

                canvas.DrawLine(x1, 1700 - d[i - 1].SGPUUSEDDEMEM, x2, 1700 - d[i].SGPUUSEDDEMEM, Op.Counter2ShadowPaint);
                canvas.DrawLine(x1, 1698 - d[i - 1].SGPUUSEDDEMEM, x2, 1698 - d[i].SGPUUSEDDEMEM, Op.Counter2Paint);
                canvas.DrawLine(x1, 1700 - (d[i - 1].SGPU1UTI * 3), x2, 1700 - (d[i].SGPU1UTI * 3), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 1698 - (d[i - 1].SGPU1UTI * 3), x2, 1698 - (d[i].SGPU1UTI * 3), Op.Counter1Paint);

                canvas.DrawLine(x1, 2130 - (d[i - 1].TGPU1DIO * 3), x2, 2130 - (d[i].TGPU1DIO * 3), Op.Counter2ShadowPaint);
                canvas.DrawLine(x1, 2128 - (d[i - 1].TGPU1DIO * 3), x2, 2128 - (d[i].TGPU1DIO * 3), Op.Counter2Paint);
                canvas.DrawLine(x1, 2130 - (d[i - 1].TCPU * 3), x2, 2130 - (d[i].TCPU * 3), Op.Counter1ShadowPaint);
                canvas.DrawLine(x1, 2128 - (d[i - 1].TCPU * 3), x2, 2128 - (d[i].TCPU * 3), Op.Counter1Paint);
            }

            ControlMessage startMessage = new ControlMessage("FrameFinished");
            MessagingCenter.Send(startMessage, "FrameFinished");
        }
    }
}