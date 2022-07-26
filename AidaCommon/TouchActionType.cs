namespace TouchTracking
{
    /// <summary>
    /// TouchActionType enum.
    /// </summary>
    public enum TouchActionType
    {
        /// <summary>
        /// The touch has entered.
        /// </summary>
        Entered,

        /// <summary>
        /// The touch is a press.
        /// </summary>
        Pressed,

        /// <summary>
        /// The touch is moving.
        /// </summary>
        Moved,

        /// <summary>
        /// The touch is releasing.
        /// </summary>
        Released,

        /// <summary>
        /// The touch has exited.
        /// </summary>
        Exited,

        /// <summary>
        /// The touch is cancelled.
        /// </summary>
        Cancelled,
    }
}
