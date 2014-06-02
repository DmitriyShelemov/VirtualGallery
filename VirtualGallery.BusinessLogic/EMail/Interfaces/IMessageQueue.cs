using System.Collections.Generic;
using VirtualGallery.BusinessLogic.EMail.Messages;

namespace VirtualGallery.BusinessLogic.EMail.Interfaces
{
    public interface IMessageQueue
    {
        int Count { get; }

        Message Dequeue();

        IEnumerable<Message> DequeueAll();

        void Enqueue(Message message);
    }
}