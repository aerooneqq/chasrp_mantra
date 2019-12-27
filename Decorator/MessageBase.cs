using System;
using System.Collections.Generic;
using System.Text;

namespace Decrorator
{
    abstract class MessageBase : IMessage
    {
        public readonly string message;
        
        public MessageBase(string message)
        {
            this.message = message;
        }

        public abstract void Send();
    }
}
