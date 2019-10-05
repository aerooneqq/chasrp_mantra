using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    class TwitterMessage : MessageBase
    {
        public TwitterMessage(IMessage nextMessage, string messageText)
            :base(nextMessage, messageText)
        { }
    }
}
