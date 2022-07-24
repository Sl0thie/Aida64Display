namespace Aida64Mobile.Views
{
    using Aida64Mobile.ViewModels;

    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    using SkiaSharp;
    using SkiaSharp.Views.Forms;
    using Aida64Mobile.Models;
    using System.IO;
    using System.Reflection;
    using TouchTracking;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        //public static StartPage Current;
        private readonly SKPaint blue = new SKPaint();
        private readonly SKPaint blueright = new SKPaint();
        private readonly SKPaint lightblue = new SKPaint();
        private readonly SKPaint gray = new SKPaint();
        private readonly SKPaint grayright = new SKPaint();
        private readonly SKPaint red = new SKPaint();
        private readonly SKPaint white = new SKPaint();
        private readonly SKPaint counter1 = new SKPaint();
        private readonly SKPaint counter2 = new SKPaint();
        private readonly SKPaint counter1shadow = new SKPaint();
        private readonly SKPaint counter2shadow = new SKPaint();
        private readonly SKFont label = new SKFont();
        private readonly SKFont value = new SKFont();
        private readonly SKBitmap graphbackground;
        private readonly SKBitmap hd;
        private readonly SKBitmap cpu;
        private readonly SKBitmap net;
        private readonly SKBitmap gpu;
        private readonly SKBitmap temp;

        public StartPage()
        {
            InitializeComponent();
            //Current = this;
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

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Mobile.Media.net.png"))
            {
                net = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Mobile.Media.gpu.png"))
            {
                gpu = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Mobile.Media.temp.png"))
            {
                temp = SKBitmap.Decode(stream);
            }

            counter1.Color = new SKColor(0, 172, 255);
            counter1.StrokeCap = SKStrokeCap.Round;
            counter1.StrokeWidth = 2;
            counter1.Style = SKPaintStyle.StrokeAndFill;
            counter1.TextAlign = SKTextAlign.Right;

            counter1shadow.Color = new SKColor(0, 64, 255);
            counter1shadow.StrokeCap = SKStrokeCap.Round;
            counter1shadow.StrokeWidth = 2;
            counter1shadow.Style = SKPaintStyle.StrokeAndFill;
            counter1shadow.TextAlign = SKTextAlign.Right;

            counter2.Color = new SKColor(0, 192, 0);
            counter2.StrokeCap = SKStrokeCap.Round;
            counter2.StrokeWidth = 2;
            counter2.Style = SKPaintStyle.StrokeAndFill;
            counter2.TextAlign = SKTextAlign.Right;

            counter2shadow.Color = new SKColor(0, 128, 0);
            counter2shadow.StrokeCap = SKStrokeCap.Round;
            counter2shadow.StrokeWidth = 2;
            counter2shadow.Style = SKPaintStyle.StrokeAndFill;
            counter2shadow.TextAlign = SKTextAlign.Right;

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

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(SKColors.Black);

            SensorData[] d = ((BaseViewModel)BindingContext).DataStore.GetItems();
            int count = d.Length - 2;
            int margin = (args.Info.Width - graphbackground.Width) / 2;
            int rightmargin = args.Info.Width - margin;

            canvas.DrawBitmap(graphbackground, margin, 120);
            canvas.DrawBitmap(graphbackground, margin, 565);
            canvas.DrawBitmap(graphbackground, margin, 1010);
            canvas.DrawBitmap(graphbackground, margin, 1445);
            canvas.DrawBitmap(graphbackground, margin, 1890);

            canvas.DrawBitmap(cpu, margin, 10);
            canvas.DrawBitmap(hd, margin, 455);
            canvas.DrawBitmap(net, margin, 900);
            canvas.DrawBitmap(gpu, margin, 1350);
            canvas.DrawBitmap(temp, margin, 1780);

            //canvas.DrawText($"width {args.Info.Width} height {args.Info.Height} count {count}", 1, 900, label, white);

            canvas.DrawText("Memory/CPU", margin + 100, 50, label, gray);
            canvas.DrawText("Utilisation", margin + 100, 100, label, gray);
            canvas.DrawText(d[count].SMEMUTI.ToString() + "%", rightmargin - 320, 100, value, counter2shadow);
            canvas.DrawText(d[count].SCPUUTI.ToString() + "%", rightmargin, 100, value, counter1shadow);

            canvas.DrawText("Disk", margin + 90, 500, label, gray);
            canvas.DrawText("Write/Read", margin + 90, 550, label, gray);
            canvas.DrawText(d[count].SDSK7WRITESPD.ToString() + "Mb", rightmargin - 320, 550, value, counter2shadow);
            canvas.DrawText(d[count].SDSK7READSPD.ToString() + "Mb", rightmargin, 550, value, counter1shadow);

            canvas.DrawText("Network", margin + 110, 945, label, gray);
            canvas.DrawText("Upload/Download", margin + 110, 995, label, gray);
            canvas.DrawText(d[count].SNIC2ULRATE.ToString() + "Kb", rightmargin - 320, 995, value, counter2shadow);
            canvas.DrawText(d[count].SNIC2DLRATE.ToString() + "Kb", rightmargin, 995, value, counter1shadow);

            canvas.DrawText("Memory/GPU", margin + 130, 1380, label, gray);
            canvas.DrawText("Utilisation", margin + 130, 1425, label, gray);
            canvas.DrawText(d[count].SGPU1USEDDEMEM.ToString() + "Mb", rightmargin - 320, 1425, value, counter2shadow);
            canvas.DrawText(d[count].SGPU1UTI.ToString() + "%", rightmargin, 1425, value, counter1shadow);

            canvas.DrawText("GPU/CPU", margin + 60, 1825, label, gray);
            canvas.DrawText("Temperature", margin + 60, 1870, label, gray);
            canvas.DrawText(d[count].TGPU1DIO.ToString() + "c", rightmargin - 320, 1870, value, counter2shadow);
            canvas.DrawText(d[count].TCPU.ToString() + "c", rightmargin, 1870, value, counter1shadow);

            int x1;
            int x2;

            for (int i = 1; i < Op.MaxValues; i++)
            {
                x1 = (i * 4) + margin + 3;
                x2 = x1 + 4;

                canvas.DrawLine(x1, 428 - (d[i - 1].SMEMUTI * 3), x2, 428 - (d[i].SMEMUTI * 3), counter2shadow);
                canvas.DrawLine(x1, 426 - (d[i - 1].SMEMUTI * 3), x2, 426 - (d[i].SMEMUTI * 3), counter2);
                canvas.DrawLine(x1, 428 - (d[i - 1].SCPUUTI * 3), x2, 428 - (d[i].SCPUUTI * 3), counter1shadow);
                canvas.DrawLine(x1, 426 - (d[i - 1].SCPUUTI * 3), x2, 426 - (d[i].SCPUUTI * 3), counter1);

                canvas.DrawLine(x1, 872 - d[i - 1].SDSKWRITESPD , x2, 872 - d[i].SDSKWRITESPD , counter2shadow);
                canvas.DrawLine(x1, 870 - d[i - 1].SDSKWRITESPD , x2, 870 - d[i].SDSKWRITESPD , counter2);
                canvas.DrawLine(x1, 872 - d[i - 1].SDSKREADSPD , x2, 872 - d[i].SDSKREADSPD , counter1shadow);
                canvas.DrawLine(x1, 870 - d[i - 1].SDSKREADSPD , x2, 870 - d[i].SDSKREADSPD , counter1);
                
                canvas.DrawLine(x1, 1320 - d[i - 1].SNICULRATE, x2, 1320 - d[i].SNICULRATE, counter2shadow);
                canvas.DrawLine(x1, 1318 - d[i - 1].SNICULRATE, x2, 1318 - d[i].SNICULRATE, counter2);
                canvas.DrawLine(x1, 1320 - d[i - 1].SNICDLRATE, x2, 1320 - d[i].SNICDLRATE, counter1shadow);
                canvas.DrawLine(x1, 1318 - d[i - 1].SNICDLRATE, x2, 1318 - d[i].SNICDLRATE, counter1);

                canvas.DrawLine(x1, 1762 - d[i - 1].SGPUUSEDDEMEM, x2, 1762 - d[i].SGPUUSEDDEMEM, counter2shadow);
                canvas.DrawLine(x1, 1760 - d[i - 1].SGPUUSEDDEMEM, x2, 1760 - d[i].SGPUUSEDDEMEM, counter2);
                canvas.DrawLine(x1, 1762 - (d[i - 1].SGPU1UTI * 3), x2, 1762 - (d[i].SGPU1UTI * 3), counter1shadow);
                canvas.DrawLine(x1, 1760 - (d[i - 1].SGPU1UTI * 3), x2, 1760 - (d[i].SGPU1UTI * 3), counter1);

                canvas.DrawLine(x1, 2182 - (d[i - 1].TGPU1DIO * 3), x2, 2182 - (d[i].TGPU1DIO * 3), counter2shadow);
                canvas.DrawLine(x1, 2180 - (d[i - 1].TGPU1DIO * 3), x2, 2180 - (d[i].TGPU1DIO * 3), counter2);
                canvas.DrawLine(x1, 2182 - (d[i - 1].TCPU * 3), x2, 2182 - (d[i].TCPU * 3), counter1shadow);
                canvas.DrawLine(x1, 2180 - (d[i - 1].TCPU * 3), x2, 2180 - (d[i].TCPU * 3), counter1);
            }

            ControlMessage startMessage = new ControlMessage("FrameFinished");
            MessagingCenter.Send(startMessage, "FrameFinished");
        }
    }
}