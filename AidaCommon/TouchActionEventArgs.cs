namespace TouchTracking
{
    using System;

    using Xamarin.Forms;

    /// <summary>
    /// TouchActionEventArgs class.
    /// </summary>
    public class TouchActionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TouchActionEventArgs"/> class.
        /// </summary>
        /// <param name="id">The Id of each individual finger.</param>
        /// <param name="type">The type of touch.</param>
        /// <param name="location">The location of the touch.</param>
        /// <param name="isInContact">Wether the event is still in contact.</param>
        public TouchActionEventArgs(long id, TouchActionType type, Point location, bool isInContact)
        {
            Id = id;
            Type = type;
            Location = location;
            IsInContact = isInContact;
        }

        /// <summary>
        /// Gets the Id. This is the Id of each individual finger.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Gets the type of touch.
        /// </summary>
        public TouchActionType Type { get; private set; }

        /// <summary>
        /// Gets a point of the location.
        /// </summary>
        public Point Location { get; private set; }

        /// <summary>
        /// Gets a value indicating whether IsInContact property is true or false.
        /// This property is always true for Pressed events and false for Released events.
        /// It's also always true for Moved events on iOS and Android.
        /// The IsInContact property might be false for Moved events on the Universal Windows
        /// Platform when the program is running on the desktop and the mouse pointer moves
        /// without a button pressed.
        /// </summary>
        public bool IsInContact { get; private set; }
    }
}
