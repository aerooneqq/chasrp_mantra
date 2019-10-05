using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    class FacebookMessage : MessageBase
    {
        public FacebookMessage(IMessage nextMessage, string message) 
            : base(nextMessage, message) { }
    }
}
