using VirtualGallery.BusinessLogic.Orders;

namespace VirtualGallery.Web.Models.ShoppingCart
{
    public class DecorTypeModel
    {
        public string ImageUrl { get; set; }

        public DecorationType Type { get; set; }

        public string TextId { get; set; }

        public string Text { get; set; }
    }
}