using System;
using System.Collections.Generic;
using System.Text;

namespace Decrorator
{
    class FacebookMessage : MessageBase
    {
        public FacebookMessage(string message) : base(message) { }

        public override void Send()
        {
            Console.WriteLine(message);
        }
    }
}
