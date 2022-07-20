namespace Aida64Mobile.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Aida64Mobile.ViewModels;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using SkiaSharp;
    using SkiaSharp.Views.Forms;
    using Aida64Mobile.Services;
    using Aida64Mobile.Models;
    using System.IO;
    using System.Reflection;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public static StartPage Current;

        SKPaint blue = new SKPaint();
        SKPaint blueright = new SKPaint();
        SKPaint lightblue = new SKPaint();
        SKPaint gray = new SKPaint();
        SKPaint grayright = new SKPaint();
        SKPaint red = new SKPaint();
        SKPaint white = new SKPaint();

        SKFont label = new SKFont();
        SKFont value = new SKFont();

        SKBitmap graphbackground;
        SKBitmap hd;
        SKBitmap cpu;

        public StartPage()
        {
            InitializeComponent();
            Current = this;
            StartViewModel vm = new StartViewModel();

            BindingContext = vm ;

            vm.Update += Vm_Update;

            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("Aida64Mobile.Media.graphbackground.png"))
            {
                graphbackground = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Mobile.Media.hd.png"))
            {
                hd = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Mobile.Media.cpu.png"))
            {
                cpu = SKBitmap.Decode(stream);
            }

            blue.Style = SKPaintStyle.Stroke;
            blue.StrokeCap = SKStrokeCap.Round;
            blue.Color = SKColors.Blue;
            blue.StrokeWidth = 2;

            blueright.Style = SKPaintStyle.Stroke;
            blueright.StrokeCap = SKStrokeCap.Round;
            blueright.Color = SKColors.Blue;
            blueright.StrokeWidth = 2;
            blueright.TextAlign = SKTextAlign.Right;

            lightblue.Style = SKPaintStyle.Stroke;
            lightblue.StrokeCap = SKStrokeCap.Round;
            lightblue.Color = new SKColor(86, 96, 255);
            lightblue.StrokeWidth = 2;

            gray.Color = SKColors.Gray;
            gray.StrokeWidth = 1;

            grayright.Color = SKColors.Gray;
            grayright.StrokeWidth = 1;
            grayright.TextAlign = SKTextAlign.Right;

            white.Color = SKColors.White;
            white.StrokeWidth = 1;

            red.Color = SKColors.Red;
            red.StrokeWidth = 1;

            label.Size = 42;
            value.Size = 72;
        }

        private void Vm_Update(object sender)
        {
            canvasView.InvalidateSurface();
        }

        void OnCanvasViewTapped(object sender, EventArgs args)
        {

        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            // Emulator - samsung S9+
            // width 1024 - 1049
            // height 1967 - 2073
            // 1080 - 2157
            // 1080 - 2088

            SKImageInfo info = new SKImageInfo();
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(SKColors.Black);

            SensorData[] d = ((BaseViewModel)(this.BindingContext)).DataStore.GetItems();
            int count = d.Length - 2;
            int margin = (args.Info.Width - graphbackground.Width) / 2;
            int rightmargin = args.Info.Width - margin;

            canvas.DrawBitmap(graphbackground, margin, 125);
            canvas.DrawBitmap(graphbackground, margin, 605);

            canvas.DrawBitmap(graphbackground, margin, 1085);
            canvas.DrawBitmap(graphbackground, margin, 1565);
            //canvas.DrawBitmap(graphbackground, margin, 1740);

            canvas.DrawBitmap(cpu, margin, 20);
            canvas.DrawBitmap(hd, margin, 490);

            //canvas.DrawText($"width {args.Info.Width} height {args.Info.Height} count {count}", 1, 900, label, white);

            canvas.DrawText("CPU", margin + 100, 60, label, gray);
            canvas.DrawText("Utilisation", margin + 100, 110, label, gray);
            canvas.DrawText(d[count].SCPUUTI.ToString() + "%", rightmargin, 110, value, grayright);

            canvas.DrawText("Disk", margin + 90, 525, label, gray);
            canvas.DrawText("Utilisation", margin + 90, 580, label, gray);
            canvas.DrawText(d[count].TCPU.ToString() + "%", rightmargin, 580, value, grayright);

            //canvas.DrawText("GPU Usage", margin, 880, font1, gray);
            //canvas.DrawText(d[count].SGPU1UTI.ToString("0.00") + "%", rightmargin, 880, font1, grayright);

            //canvas.DrawText("CPU Temprature", margin, 1300, font1, gray);
            //canvas.DrawText(d[count].TCPU.ToString("0.00") + "c", rightmargin, 1300, font1, grayright);

            //canvas.DrawText("GPU Temprature", margin, 1710, font1, gray);
            //canvas.DrawText(d[count].TGPU1DIO.ToString("0.00") + " c", rightmargin, 1710, font1, grayright);

            int x1;
            int x2;

            for (int i = 1; i < Op.MaxValues; i++)
            {
                x1 = i * 4 + margin + 3;
                x2 = x1 + 4;

                canvas.DrawLine(x1, 432 - (d[i - 1].SCPUUTI * 3), x2, 432 - (d[i].SCPUUTI * 3), blue);
                canvas.DrawLine(x1, 430 - (d[i - 1].SCPUUTI * 3), x2, 430 - (d[i].SCPUUTI * 3), lightblue);

                canvas.DrawLine(x1, 912 - (d[i - 1].TCPU * 3), x2, 912 - (d[i].TCPU * 3), blue);
                canvas.DrawLine(x1, 910 - (d[i - 1].TCPU * 3), x2, 910 - (d[i].TCPU * 3), lightblue);

                //canvas.DrawLine(x1, 1221 - (d[i - 1].SGPU1UTI * 3), x2, 1221 - (d[i].SGPU1UTI * 3), blue);
                //canvas.DrawLine(x1, 1220 - (d[i - 1].SGPU1UTI * 3), x2, 1220 - (d[i].SGPU1UTI * 3), lightblue);

                //canvas.DrawLine(x1, 1641 - (d[i - 1].TCPU * 3), x2, 1641 - (d[i].TCPU * 3), blue);
                //canvas.DrawLine(x1, 1640 - (d[i - 1].TCPU * 3), x2, 1640 - (d[i].TCPU * 3), lightblue);

                //canvas.DrawLine(x1, 2051 - (d[i - 1].TGPU1DIO * 3), x2, 2051 - (d[i].TGPU1DIO * 3), blue);
                //canvas.DrawLine(x1, 2050 - (d[i - 1].TGPU1DIO * 3), x2, 2050 - (d[i].TGPU1DIO * 3), lightblue);
            }

            ControlMessage startMessage = new ControlMessage("FrameFinished");
            MessagingCenter.Send(startMessage, "FrameFinished");
        }
    }
}