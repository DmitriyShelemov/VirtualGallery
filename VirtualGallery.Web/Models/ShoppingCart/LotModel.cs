using VirtualGallery.BusinessLogic.Orders;
using VirtualGallery.BusinessLogic.Pictures;

namespace VirtualGallery.Web.Models.ShoppingCart
{
    public class LotModel
    {
        public int Id { get; set; }

        public int PictureId { get; set; }

        public DecorationType Decor { get; set; }

        public CartPictureModel Picture { get; set; }
    }
}
