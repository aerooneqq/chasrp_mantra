using System;

namespace Decrorator
{
    class Program
    {
        static void Main(string[] args)
        {
            IMessage facebookMessage = new FacebookMessage("Facebook message");
            IMessage twitterMessage = new TwitterMessage("Twitter message");
            IMessage vkMessage = new VkMessage("VK message");

            IMessage messageDecorator = new MessageDecorator(new MessageDecorator(facebookMessage, twitterMessage),
                vkMessage);

            messageDecorator.Send();
        }
    }
}
