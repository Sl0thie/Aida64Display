namespace Aida64Common.Views
{
    using System;

    using SkiaSharp;
    using SkiaSharp.Views.Forms;

    using TouchTracking;

    /// <summary>
    /// IView interface.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets or sets canvasView object.
        /// </summary>
        SKCanvasView CanvasView { get; set; }

        /// <summary>
        /// Gets or sets touchEffect object.
        /// </summary>
        TouchEffect TouchEffect { get; set; }

        /// <summary>
        /// OnButtonPress method handles the button events.
        /// </summary>
        /// <param name="button">The number of the button that was pressed.</param>
        void OnButtonPress(int button);

        /// <summary>
        /// DrawDisplay method draws the display.
        /// </summary>
        /// <param name="canvas">The cnvas being drawn to. This parameter passes the canvas from the base to the override.</param>
        void DrawDisplay(SKCanvas canvas);
    }
}
