namespace VirtualGallery.BusinessLogic.EMail.Messages
{
    public class Message
    {
        public Message()
        {
        }
        protected Message(string mail, string subject, string body)
        {
            To = mail;
            Subject = subject;
            Body = body;
        }

        public string Body { get; set; }

        public string Subject { get; set; }

        public string To { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Message)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = To != null ? To.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Body != null ? Body.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(Message other)
        {
            return string.Equals(To, other.To) && string.Equals(Subject, other.Subject)
                   && string.Equals(Body, other.Body);
        }
    }
}
