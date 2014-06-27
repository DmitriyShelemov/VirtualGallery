using VirtualGallery.Infrastructure.Localization;
namespace VirtualGallery.BusinessLogic.Orders
{
    public enum DeliveryType
    {
        [LocalizedDescription(Localization.Names.Delivery_Type_Express)]
        Express,
        [LocalizedDescription(Localization.Names.Delivery_Type_SelfTake)]
        SelfTake
    }
}
