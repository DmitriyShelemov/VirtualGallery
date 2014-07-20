using VirtualGallery.BusinessLogic.Orders;

namespace VirtualGallery.Web.Models.Delivery
{
    public class DeliveryItemModel
    {
        public string Title { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public string Summary { get; set; }
    }
}