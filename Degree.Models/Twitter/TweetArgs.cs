using System;
using System.Collections.Generic;
using System.Text;

namespace Degree.Models.Twitter
{
    public class TweetArgs : EventArgs
    {
        public string Message { get; set; }
        public TweetRaw Tweet { get; set; }
    }
}
