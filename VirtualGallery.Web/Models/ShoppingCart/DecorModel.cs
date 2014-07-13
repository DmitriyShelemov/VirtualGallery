using System.Web.Mvc;

namespace VirtualGallery.Web.Models.ShoppingCart
{
    public class DecorModel
    {
        [AllowHtml]
        public string SimpleBoxText { get; set; }

        [AllowHtml]
        public string FrameText { get; set; }

        [AllowHtml]
        public string LuxText { get; set; }
    }
}