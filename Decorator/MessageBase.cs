using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    abstract class MessageBase : IMessage
    {
        protected readonly string message;
        protected readonly IMessage nextMessage;

        public MessageBase(IMessage nextMessage, string message)
        {
            this.message = message;
            this.nextMessage = nextMessage;
        }

        public void Send()
        {
            Console.WriteLine(message);
            nextMessage?.Send();
        }
    }
}
