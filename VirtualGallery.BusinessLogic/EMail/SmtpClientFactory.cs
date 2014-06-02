using System.Net.Mail;
using VirtualGallery.BusinessLogic.EMail.Interfaces;

namespace VirtualGallery.BusinessLogic.EMail
{
    public class SmtpClientFactory : ISmtpClientFactory
    {
        public SmtpClient CreateSmtpClient()
        {
            return new SmtpClient();
        }
    }
}