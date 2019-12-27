using System;
using System.Collections.Generic;
using System.Text;

namespace Decrorator
{
    class MessageDecorator : IMessage
    {
        private readonly IMessage nextMessage;
        private readonly IMessage thisMessage;

        public MessageDecorator(IMessage thisMessage, IMessage nextMessage)
        {
            this.nextMessage = nextMessage;
            this.thisMessage = thisMessage;
        }

        public void Send()
        {
            thisMessage.Send();

            nextMessage?.Send();
        }
    }
}
