using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolowaFrontend.Core.MessageQueue.Messages
{
    public enum MessageTarget
    {
        GATEWAY,
    }

    public class MessageBase
    {
        public string Origin { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public MessageTarget Target { get; set; }
        public string Function { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
