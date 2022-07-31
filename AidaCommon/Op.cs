namespace Aida64Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Op
    {
        private static int queueLength;

        public static int QueueLength
        {
            get { return queueLength; }
            set { queueLength = value; }
        }
    }
}
