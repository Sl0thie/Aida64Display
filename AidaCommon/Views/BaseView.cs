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

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseView"/> class.
        /// </summary>
        public BaseView()
        {
            canvasView.PaintSurface += CanvasView_PaintSurface;
            touchEffect.Capture = true;
            touchEffect.TouchAction += TouchEffect_TouchAction;
            Grid grid = new Grid();
            grid.Effects.Add(touchEffect);
            grid.Children.Add(canvasView);

            Content = grid;
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
