namespace Aida64Display.Views
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
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
        private const int MaxValues = 252;
        private readonly SKPaint counter1 = new SKPaint();
        private readonly SKPaint counter2 = new SKPaint();
        private readonly SKPaint counter1shadow = new SKPaint();
        private readonly SKPaint counter2shadow = new SKPaint();

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
        }

        /// <summary>
        /// Vm_Update method is used to redraw the canvas when the veiw model calls this method.
        /// </summary>
        /// <param name="sender">The object where the event originated.</param>
        private void Vm_Update(object sender)
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
        }
    }
}