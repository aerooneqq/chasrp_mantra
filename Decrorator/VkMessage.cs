using System;
using System.Collections.Generic;
using System.Text;

namespace Decrorator
{
    class VkMessage : MessageBase
    {
        public VkMessage(string message) : base(message) { }

        public override void Send()
        {
            Console.WriteLine(message);
        }
    }
}
