using VirtualGallery.BusinessLogic.EMail.Messages;

namespace VirtualGallery.BusinessLogic.EMail.Interfaces
{
    public interface IMailBox
    {
        bool Send(Message message);
    }
}