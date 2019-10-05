using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    class VKMessage : MessageBase
    {
        public VKMessage(IMessage nextMessage, string message) 
            : base(nextMessage, message) {  }
    }
}
