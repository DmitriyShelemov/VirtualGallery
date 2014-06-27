using System.Web.Mvc;

namespace VirtualGallery.Web.Models.Contact
{
    public class ContactsModel
    {
        public string Address { get; set; }

        public string Phone { get; set; }

        public string WorkTime { get; set; }

        [AllowHtml]
        public string MapUrl { get; set; }
    }
}