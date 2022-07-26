namespace TouchTracking
{
    using Xamarin.Forms;

    /// <summary>
    /// TouchEffect class.
    /// </summary>
    public class TouchEffect : RoutingEffect
    {
        /// <summary>
        /// TouchAction event handler.
        /// </summary>
        public event TouchActionEventHandler TouchAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="TouchEffect"/> class.
        /// </summary>
        public TouchEffect()
            : base("XamarinDocs.TouchEffect")
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether Capture is true.
        /// </summary>
        public bool Capture { get; set; }

        /// <summary>
        /// OnTouchAction method manages touch actions.
        /// </summary>
        /// <param name="element">The original element.</param>
        /// <param name="args">The parameters of the touch action.</param>
        public void OnTouchAction(Element element, TouchActionEventArgs args)
        {
            TouchAction?.Invoke(element, args);
        }
    }
}
