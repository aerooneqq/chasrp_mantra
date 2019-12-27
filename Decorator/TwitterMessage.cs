using System;
using System.Collections.Generic;
using System.Text;

namespace Decrorator
{
    class TwitterMessage : MessageBase
    {
        public TwitterMessage(string message) : base(message) { }

        public override void Send()
        {
            Console.WriteLine(message);
        }
    }
}
