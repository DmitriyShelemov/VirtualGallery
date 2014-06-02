using System;
using VirtualGallery.Infrastructure.Localization;

namespace VirtualGallery.BusinessLogic.Exceptions
{
    [Serializable]
    public class AuthorizationException : LocalizedValidationException
    {
        public AuthorizationException()
            : base("Security_Access_is_denied")
        {
        }
    }
}