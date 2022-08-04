namespace Aida64Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// ImageData class is used to pass image data through the messaging center as its seems easier to save them in PCL.
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// Gets the file name of the image.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the byte data of the image.
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageData"/> class.
        /// </summary>
        /// <param name="data">The byte data of the image.</param>
        /// <param name="filename">The file name of the image.</param>
        public ImageData(byte[] data, string filename)
        {
            FileName = filename;
            Data = data;
        }
    }
}
