using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            IMessage twitterMessage = new TwitterMessage(null, "Hello from twitter!");
            IMessage facebookMessage = new FacebookMessage(twitterMessage, "Hello from facebook!");
            IMessage vkMessage = new VKMessage(facebookMessage, "Hello from VK!");

            vkMessage.Send();
        }
    }
}
