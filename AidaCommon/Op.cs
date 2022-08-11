namespace Aida64Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using SkiaSharp;

    /// <summary>
    /// Op static class is used to store globals such as colors and fonts.
    /// </summary>
    public static class Op
    {
        /// <summary>
        /// Gets the queue length.
        /// </summary>
        public static int QueueLength { get; private set; }

        /// <summary>
        /// Gets Counter1Paint.
        /// </summary>
        public static SKPaint Counter1Paint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter2Paint.
        /// </summary>
        public static SKPaint Counter2Paint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter3Paint.
        /// </summary>
        public static SKPaint Counter3Paint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter4Paint.
        /// </summary>
        public static SKPaint Counter4Paint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter5Paint.
        /// </summary>
        public static SKPaint Counter5Paint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter6Paint.
        /// </summary>
        public static SKPaint Counter6Paint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter1ShadowPaint.
        /// </summary>
        public static SKPaint Counter1ShadowPaint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter2ShadowPaint.
        /// </summary>
        public static SKPaint Counter2ShadowPaint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter3ShadowPaint.
        /// </summary>
        public static SKPaint Counter3ShadowPaint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter4ShadowPaint.
        /// </summary>
        public static SKPaint Counter4ShadowPaint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter5ShadowPaint.
        /// </summary>
        public static SKPaint Counter5ShadowPaint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter6ShadowPaint.
        /// </summary>
        public static SKPaint Counter6ShadowPaint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter1Right.
        /// </summary>
        public static SKPaint Counter1PaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter2PaintRight.
        /// </summary>
        public static SKPaint Counter2PaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter3PaintRight.
        /// </summary>
        public static SKPaint Counter3PaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter4PaintRight.
        /// </summary>
        public static SKPaint Counter4PaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter5PaintRight.
        /// </summary>
        public static SKPaint Counter5PaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter6PaintRight.
        /// </summary>
        public static SKPaint Counter6PaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter1ShadowPaintRight.
        /// </summary>
        public static SKPaint Counter1ShadowPaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter2ShadowPaintRight.
        /// </summary>
        public static SKPaint Counter2ShadowPaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter3ShadowPaintRight.
        /// </summary>
        public static SKPaint Counter3ShadowPaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter4ShadowPaintRight.
        /// </summary>
        public static SKPaint Counter4ShadowPaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter5ShadowPaintRight.
        /// </summary>
        public static SKPaint Counter5ShadowPaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets Counter6ShadowPaintRight.
        /// </summary>
        public static SKPaint Counter6ShadowPaintRight { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets GrayPaint.
        /// </summary>
        public static SKPaint GrayPaint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets GrayRightPaint.
        /// </summary>
        public static SKPaint GrayRightPaint { get; private set; } = new SKPaint();

        /// <summary>
        /// Gets LabelFont.
        /// </summary>
        public static SKFont LabelFont { get; private set; } = new SKFont();

        /// <summary>
        /// Gets LabelFont.
        /// </summary>
        public static SKFont LabelSubFont { get; private set; } = new SKFont();

        /// <summary>
        /// Gets ValueFont.
        /// </summary>
        public static SKFont ValueFont { get; private set; } = new SKFont();

        /// <summary>
        /// Gets ValueSubFont.
        /// </summary>
        public static SKFont ValueSubFont { get; private set; } = new SKFont();

        /// <summary>
        /// Gets Button bitmap.
        /// </summary>
        public static SKBitmap Button { get; private set; } = new SKBitmap();

        /// <summary>
        /// Gets GraphBack bitmap.
        /// </summary>
        public static SKBitmap GraphBack { get; private set; } = new SKBitmap();

        /// <summary>
        /// Gets GraphBackground bitmap.
        /// </summary>
        public static SKBitmap GraphBackground { get; private set; } = new SKBitmap();

        /// <summary>
        /// Gets Hd bitmap.
        /// </summary>
        public static SKBitmap Hd { get; private set; } = new SKBitmap();

        /// <summary>
        /// Gets Cpu bitmap.
        /// </summary>
        public static SKBitmap Cpu { get; private set; } = new SKBitmap();

        /// <summary>
        /// Gets Net bitmap.
        /// </summary>
        public static SKBitmap Net { get; private set; } = new SKBitmap();

        /// <summary>
        /// Gets Gpu bitmap.
        /// </summary>
        public static SKBitmap Gpu { get; private set; } = new SKBitmap();

        /// <summary>
        /// Gets Temp bitmap.
        /// </summary>
        public static SKBitmap Temp { get; private set; } = new SKBitmap();

        /// <summary>
        /// Setup method modifies paints and fonts to suit.
        /// </summary>
        /// <param name="queueLength">The queue length for the display.</param>
        public static void Setup(int queueLength)
        {
            // Set the queue length.
            QueueLength = queueLength;

            // Get the assembly used to load media.
            Assembly assembly = Assembly.GetExecutingAssembly();

            // Load Fonts.
            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.digital-7.ttf"))
            {
                ValueFont.Typeface = SKTypeface.FromStream(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.digital-7.ttf"))
            {
                ValueSubFont.Typeface = SKTypeface.FromStream(stream);
            }

            // Load Bitmaps.
            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.button.png"))
            {
                Button = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.graphback.png"))
            {
                GraphBack = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.graphbackground.png"))
            {
                GraphBackground = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.hd.png"))
            {
                Hd = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.cpu.png"))
            {
                Cpu = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.net.png"))
            {
                Net = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.gpu.png"))
            {
                Gpu = SKBitmap.Decode(stream);
            }

            using (Stream stream = assembly.GetManifestResourceStream("Aida64Common.Media.temp.png"))
            {
                Temp = SKBitmap.Decode(stream);
            }

            // Setup paints and colors.
            Counter1Paint.Color = new SKColor(0, 172, 255);
            Counter1Paint.StrokeCap = SKStrokeCap.Round;
            Counter1Paint.StrokeWidth = 2;
            Counter1Paint.Style = SKPaintStyle.StrokeAndFill;
            Counter1Paint.TextAlign = SKTextAlign.Left;

            Counter1ShadowPaint.Color = new SKColor(0, 64, 255);
            Counter1ShadowPaint.StrokeCap = SKStrokeCap.Round;
            Counter1ShadowPaint.StrokeWidth = 2;
            Counter1ShadowPaint.Style = SKPaintStyle.StrokeAndFill;
            Counter1ShadowPaint.TextAlign = SKTextAlign.Left;

            Counter2Paint.Color = new SKColor(0, 192, 0);
            Counter2Paint.StrokeCap = SKStrokeCap.Round;
            Counter2Paint.StrokeWidth = 2;
            Counter2Paint.Style = SKPaintStyle.StrokeAndFill;
            Counter2Paint.TextAlign = SKTextAlign.Left;

            Counter2ShadowPaint.Color = new SKColor(0, 128, 0);
            Counter2ShadowPaint.StrokeCap = SKStrokeCap.Round;
            Counter2ShadowPaint.StrokeWidth = 2;
            Counter2ShadowPaint.Style = SKPaintStyle.StrokeAndFill;
            Counter2ShadowPaint.TextAlign = SKTextAlign.Left;

            Counter3Paint.Color = new SKColor(0, 172, 255);
            Counter3Paint.StrokeCap = SKStrokeCap.Round;
            Counter3Paint.StrokeWidth = 2;
            Counter3Paint.Style = SKPaintStyle.StrokeAndFill;
            Counter3Paint.TextAlign = SKTextAlign.Left;

            Counter3ShadowPaint.Color = new SKColor(0, 64, 255);
            Counter3ShadowPaint.StrokeCap = SKStrokeCap.Round;
            Counter3ShadowPaint.StrokeWidth = 2;
            Counter3ShadowPaint.Style = SKPaintStyle.StrokeAndFill;
            Counter3ShadowPaint.TextAlign = SKTextAlign.Left;

            Counter4Paint.Color = new SKColor(0, 192, 0);
            Counter4Paint.StrokeCap = SKStrokeCap.Round;
            Counter4Paint.StrokeWidth = 2;
            Counter4Paint.Style = SKPaintStyle.StrokeAndFill;
            Counter4Paint.TextAlign = SKTextAlign.Left;

            Counter4ShadowPaint.Color = new SKColor(0, 128, 0);
            Counter4ShadowPaint.StrokeCap = SKStrokeCap.Round;
            Counter4ShadowPaint.StrokeWidth = 2;
            Counter4ShadowPaint.Style = SKPaintStyle.StrokeAndFill;
            Counter4ShadowPaint.TextAlign = SKTextAlign.Left;

            Counter5Paint.Color = new SKColor(0, 172, 255);
            Counter5Paint.StrokeCap = SKStrokeCap.Round;
            Counter5Paint.StrokeWidth = 2;
            Counter5Paint.Style = SKPaintStyle.StrokeAndFill;
            Counter5Paint.TextAlign = SKTextAlign.Left;

            Counter5ShadowPaint.Color = new SKColor(0, 64, 255);
            Counter5ShadowPaint.StrokeCap = SKStrokeCap.Round;
            Counter5ShadowPaint.StrokeWidth = 2;
            Counter5ShadowPaint.Style = SKPaintStyle.StrokeAndFill;
            Counter5ShadowPaint.TextAlign = SKTextAlign.Left;

            Counter6Paint.Color = new SKColor(0, 192, 0);
            Counter6Paint.StrokeCap = SKStrokeCap.Round;
            Counter6Paint.StrokeWidth = 2;
            Counter6Paint.Style = SKPaintStyle.StrokeAndFill;
            Counter6Paint.TextAlign = SKTextAlign.Left;

            Counter6ShadowPaint.Color = new SKColor(0, 128, 0);
            Counter6ShadowPaint.StrokeCap = SKStrokeCap.Round;
            Counter6ShadowPaint.StrokeWidth = 2;
            Counter6ShadowPaint.Style = SKPaintStyle.StrokeAndFill;
            Counter6ShadowPaint.TextAlign = SKTextAlign.Left;

            // Setup paints and colors.
            Counter1PaintRight.Color = new SKColor(0, 172, 255);
            Counter1PaintRight.StrokeCap = SKStrokeCap.Round;
            Counter1PaintRight.StrokeWidth = 2;
            Counter1PaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter1PaintRight.TextAlign = SKTextAlign.Right;

            Counter1ShadowPaintRight.Color = new SKColor(0, 64, 255);
            Counter1ShadowPaintRight.StrokeCap = SKStrokeCap.Round;
            Counter1ShadowPaintRight.StrokeWidth = 2;
            Counter1ShadowPaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter1ShadowPaintRight.TextAlign = SKTextAlign.Right;

            Counter2PaintRight.Color = new SKColor(0, 192, 0);
            Counter2PaintRight.StrokeCap = SKStrokeCap.Round;
            Counter2PaintRight.StrokeWidth = 2;
            Counter2PaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter2PaintRight.TextAlign = SKTextAlign.Right;

            Counter2ShadowPaintRight.Color = new SKColor(0, 128, 0);
            Counter2ShadowPaintRight.StrokeCap = SKStrokeCap.Round;
            Counter2ShadowPaintRight.StrokeWidth = 2;
            Counter2ShadowPaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter2ShadowPaintRight.TextAlign = SKTextAlign.Right;

            Counter3PaintRight.Color = new SKColor(0, 172, 255);
            Counter3PaintRight.StrokeCap = SKStrokeCap.Round;
            Counter3PaintRight.StrokeWidth = 2;
            Counter3PaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter3PaintRight.TextAlign = SKTextAlign.Right;

            Counter3ShadowPaintRight.Color = new SKColor(0, 64, 255);
            Counter3ShadowPaintRight.StrokeCap = SKStrokeCap.Round;
            Counter3ShadowPaintRight.StrokeWidth = 2;
            Counter3ShadowPaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter3ShadowPaintRight.TextAlign = SKTextAlign.Right;

            Counter4PaintRight.Color = new SKColor(0, 192, 0);
            Counter4PaintRight.StrokeCap = SKStrokeCap.Round;
            Counter4PaintRight.StrokeWidth = 2;
            Counter4PaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter4PaintRight.TextAlign = SKTextAlign.Right;

            Counter4ShadowPaintRight.Color = new SKColor(0, 128, 0);
            Counter4ShadowPaintRight.StrokeCap = SKStrokeCap.Round;
            Counter4ShadowPaintRight.StrokeWidth = 2;
            Counter4ShadowPaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter4ShadowPaintRight.TextAlign = SKTextAlign.Right;

            Counter5PaintRight.Color = new SKColor(0, 172, 255);
            Counter5PaintRight.StrokeCap = SKStrokeCap.Round;
            Counter5PaintRight.StrokeWidth = 2;
            Counter5PaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter5PaintRight.TextAlign = SKTextAlign.Right;

            Counter5ShadowPaintRight.Color = new SKColor(0, 64, 255);
            Counter5ShadowPaintRight.StrokeCap = SKStrokeCap.Round;
            Counter5ShadowPaintRight.StrokeWidth = 2;
            Counter5ShadowPaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter5ShadowPaintRight.TextAlign = SKTextAlign.Right;

            Counter6PaintRight.Color = new SKColor(0, 192, 0);
            Counter6PaintRight.StrokeCap = SKStrokeCap.Round;
            Counter6PaintRight.StrokeWidth = 2;
            Counter6PaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter6PaintRight.TextAlign = SKTextAlign.Right;

            Counter6ShadowPaintRight.Color = new SKColor(0, 128, 0);
            Counter6ShadowPaintRight.StrokeCap = SKStrokeCap.Round;
            Counter6ShadowPaintRight.StrokeWidth = 2;
            Counter6ShadowPaintRight.Style = SKPaintStyle.StrokeAndFill;
            Counter6ShadowPaintRight.TextAlign = SKTextAlign.Right;

            GrayPaint.Color = SKColors.Gray;
            GrayPaint.StrokeWidth = 1;

            LabelFont.Size = 96;
            ValueFont.Size = 128;

            LabelSubFont.Size = 72;
            ValueSubFont.Size = 72;
        }
    }
}
