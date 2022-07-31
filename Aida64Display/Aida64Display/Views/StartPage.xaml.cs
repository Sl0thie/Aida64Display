namespace Aida64Display.Views
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using Aida64Common;
    using Aida64Common.Models;
    using Aida64Common.ViewModels;
    using Aida64Display.ViewModels;
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
        private readonly SKPaint counter1 = new SKPaint();
        private readonly SKPaint counter2 = new SKPaint();
        private readonly SKPaint counter1shadow = new SKPaint();
        private readonly SKPaint counter2shadow = new SKPaint();
        private readonly SKPaint gray = new SKPaint();
        private readonly SKPaint grayright = new SKPaint();

        private readonly SKFont label = new SKFont();
        private readonly SKFont value = new SKFont();

        private readonly SKBitmap button;
        private readonly SKBitmap graphback;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartPage"/> class.
        /// </summary>
        public StartPage()
        {
            InitializeComponent();

            // Create and connect to the view model.
            StartViewModel vm = new StartViewModel();
            BindingContext = vm;
            vm.Update += Vm_Update;

            // Load media files.
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("Aida64Display.Media.button.png"))
            {
                button = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Display.Media.graphback.png"))
            {
                graphback = SKBitmap.Decode(stream);
            }

            // Setup paints and colors.
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

            gray.Color = SKColors.Gray;
            gray.StrokeWidth = 1;

            label.Size = 42;
            value.Size = 128;
        }

        /// <summary>
        /// Vm_Update method is used to redraw the canvas when the veiw model calls this method.
        /// </summary>
        /// <param name="sender">The object where the event originated.</param>
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
        /// Samsung S6 landscape 2560x1440. 2560x1344 with status bar.
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

            canvas.DrawBitmap(graphback, 20, 154);

            canvas.DrawBitmap(button, 0, 1184);
            canvas.DrawBitmap(button, 256, 1184);
            canvas.DrawBitmap(button, 512, 1184);
            canvas.DrawBitmap(button, 768, 1184);
            canvas.DrawBitmap(button, 1024, 1184);
            canvas.DrawBitmap(button, 1280, 1184);
            canvas.DrawBitmap(button, 1536, 1184);
            canvas.DrawBitmap(button, 1792, 1184);
            canvas.DrawBitmap(button, 2048, 1184);
            canvas.DrawBitmap(button, 2304, 1184);

            //canvas.DrawText($"width {args.Info.Width} height {args.Info.Height} count {count}", 2500, 100, label, counter2);

            canvas.DrawText("CPU", 100, 45, label, gray);

            canvas.DrawText(d[count].SMEMUTI.ToString() + "%", 1500, 120, value, counter2shadow);
            canvas.DrawText(d[count].SCPUUTI.ToString() + "%", 1000, 120, value, counter1shadow);

            int x1;
            int x2;

            for (int i = 1; i < Op.QueueLength; i++)
            {
                x1 = i * 3 + 30;
                x2 = x1 + 3;

                //canvas.DrawLine(x1, 1134 - (d[i - 1].SMEMUTI * 10), x2, 1132 - (d[i].SMEMUTI * 10), counter2shadow);
                //canvas.DrawLine(x1, 1132 - (d[i - 1].SMEMUTI * 10), x2, 1132 - (d[i].SMEMUTI * 10), counter2);
                canvas.DrawLine(x1, 1134 - (d[i - 1].SCPUUTI * 10), x2, 1134 - (d[i].SCPUUTI * 10), counter1shadow);
                canvas.DrawLine(x1, 1132 - (d[i - 1].SCPUUTI * 10), x2, 1132 - (d[i].SCPUUTI * 10), counter1);
            }
        }
    }
}