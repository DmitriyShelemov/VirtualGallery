using System.Net.Mail;

namespace VirtualGallery.BusinessLogic.EMail.Interfaces
{
    public interface ISmtpClientFactory
    {
        SmtpClient CreateSmtpClient();
    }
}