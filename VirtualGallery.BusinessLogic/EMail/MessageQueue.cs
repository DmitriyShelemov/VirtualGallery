using System.Collections.Generic;
using VirtualGallery.BusinessLogic.EMail.Interfaces;
using VirtualGallery.BusinessLogic.EMail.Messages;

namespace VirtualGallery.BusinessLogic.EMail
{
    public class MessageQueue : IMessageQueue
    {
        private static readonly object SyncRoot = new object();

        private Queue<Message> _messages = new Queue<Message>();

        public int Count
        {
            get
            {
                return _messages.Count;
            }
        }

        public Message Dequeue()
        {
            lock (SyncRoot)
            {
                return _messages.Dequeue();
            }
        }

        public IEnumerable<Message> DequeueAll()
        {
            IEnumerable<Message> queued;
            lock (SyncRoot)
            {
                queued = _messages;
                _messages = new Queue<Message>();
            }

            return queued;
        }

        public void Enqueue(Message message)
        {
            lock (SyncRoot)
            {
                _messages.Enqueue(message);
            }
        }
    }
}