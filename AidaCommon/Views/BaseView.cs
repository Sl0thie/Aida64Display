namespace Aida64Common.Views
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using Aida64Common.Models;
    using Aida64Common.ViewModels;
    using SkiaSharp;
    using SkiaSharp.Views.Forms;
    using TouchTracking;
    using Xamarin.CommunityToolkit;
    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Forms;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    /// <summary>
    /// BaseView class.
    /// </summary>
    public class BaseView : ContentPage, IView
    {
        private SKCanvasView canvasView = new SKCanvasView();

        /// <inheritdoc/>
        public SKCanvasView CanvasView
        {
            get { return canvasView; }
            set { canvasView = value; }
        }

        private TouchEffect touchEffect = new TouchEffect();

        /// <inheritdoc/>
        public TouchEffect TouchEffect
        {
            get { return touchEffect; }
            set { touchEffect = value; }
        }

        private CameraView cameraView = new CameraView();

        /// <inheritdoc/>
        public CameraView CameraView
        {
            get { return cameraView; }
            set { cameraView = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseView"/> class.
        /// </summary>
        public BaseView()
        {
            cameraView.CameraOptions = CameraOptions.Front;
            cameraView.CaptureMode = CameraCaptureMode.Photo;
            cameraView.OnAvailable += Cv_OnAvailable;
            cameraView.MediaCaptured += Cv_MediaCaptured;
            canvasView.PaintSurface += CanvasView_PaintSurface;
            touchEffect.Capture = true;
            touchEffect.TouchAction += TouchEffect_TouchAction;
            Grid grid = new Grid();
            RowDefinition row0 = new RowDefinition();
            row0.Height = 1440;
            RowDefinition row1 = new RowDefinition();
            row1.Height = 1440;

            grid.RowDefinitions.Add(row0);
            grid.RowDefinitions.Add(row1);

            grid.Effects.Add(touchEffect);
            grid.Children.Add(canvasView, 0, 0);
            grid.Children.Add(cameraView, 0, 1);
            Content = grid;
        }

        private void Cv_MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Cv_MediaCaptured {e.ImageData.Length} {Environment.CurrentDirectory}");

            try
            {
                ImageData id = new ImageData(e.ImageData, DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".jpg");
                MessagingCenter.Send(id, "SaveImage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR {ex.Message}");
            }
        }

        private void Cv_OnAvailable(object sender, bool e)
        {
            // TODO Replace with message.
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                cameraView.Shutter();
                System.Diagnostics.Debug.WriteLine("Shutter");
                return true;
            });
        }

        /// <inheritdoc/>
        public virtual void OnButtonPress(int button)
        {
        }

        /// <summary>
        /// Vm_Update method handles the Update event sent from the ViewModel. This tells the View to redraw the display.
        /// </summary>
        /// <param name="sender">The object where the event originated.</param>
        /// <param name="e">The arguements parameter.</param>
        public void VmUpdate(object sender, EventArgs e)
        {
            canvasView.InvalidateSurface();
        }

        /// <inheritdoc/>
        public virtual void DrawDisplay(SKCanvas canvas)
        {
        }

        private void TouchEffect_TouchAction(object sender, TouchActionEventArgs args)
        {
            if (args.Type == TouchActionType.Pressed)
            {
                SKPoint point = new SKPoint((float)(canvasView.CanvasSize.Width * args.Location.X / canvasView.Width), (float)(canvasView.CanvasSize.Height * args.Location.Y / canvasView.Height));

                if (point.Y > 1184)
                {
                    if (point.X < 256)
                    {
                        OnButtonPress(0);
                    }
                    else if (point.X < 512)
                    {
                        OnButtonPress(1);
                    }
                    else if (point.X < 768)
                    {
                        OnButtonPress(2);
                    }
                    else if (point.X < 1024)
                    {
                        OnButtonPress(3);
                    }
                    else if (point.X < 1280)
                    {
                        OnButtonPress(4);
                    }
                    else if (point.X < 1536)
                    {
                        OnButtonPress(5);
                    }
                    else if (point.X < 1792)
                    {
                        OnButtonPress(6);
                    }
                    else if (point.X < 2048)
                    {
                        OnButtonPress(7);
                    }
                    else if (point.X < 2304)
                    {
                        OnButtonPress(8);
                    }
                }
            }
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear(SKColors.Black);

            canvas.DrawBitmap(Op.Button, 0, 1184);
            canvas.DrawBitmap(Op.Button, 256, 1184);
            canvas.DrawBitmap(Op.Button, 512, 1184);
            canvas.DrawBitmap(Op.Button, 768, 1184);
            canvas.DrawBitmap(Op.Button, 1024, 1184);
            canvas.DrawBitmap(Op.Button, 1280, 1184);
            canvas.DrawBitmap(Op.Button, 1536, 1184);
            canvas.DrawBitmap(Op.Button, 1792, 1184);
            canvas.DrawBitmap(Op.Button, 2048, 1184);
            canvas.DrawBitmap(Op.Button, 2304, 1184);

            DrawDisplay(canvas);
        }
    }
}
