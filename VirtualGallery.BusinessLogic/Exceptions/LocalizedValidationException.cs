using System;
using System.Runtime.Serialization;

namespace VirtualGallery.BusinessLogic.Exceptions
{
    [Serializable]
    public class LocalizedValidationException : Exception
    {
		private readonly string _message;

		private int _errorCode = -1;
        
        public LocalizedValidationException(string message, Exception innerException = null)
            : base(message, innerException)
        {
            _message = message;
        }

        protected LocalizedValidationException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
            _message = info.GetString("_message");
        }


        public override string Message
        {
            get
            {
                return _message;
            }
        }

		public int ErrorCode
		{
			get
			{
				return _errorCode;
			}
			set
			{
				_errorCode = value;
			}
		}

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("_message", _message);
        }
    }
}