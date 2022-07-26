namespace Aida64Common.Models
{
    /// <summary>
    /// ControlMessage class provides an object to be passed via the MessageCenter.
    /// </summary>
    public class ControlMessage
    {
        /// <summary>
        /// Gets or sets the Text property that is used to pass the message.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlMessage"/> class.
        /// ControlMessage constructor.
        /// </summary>
        /// <param name="text">The text message to be passed via MessageCenter.</param>
        public ControlMessage(string text)
        {
            Text = text;
        }
    }
}
