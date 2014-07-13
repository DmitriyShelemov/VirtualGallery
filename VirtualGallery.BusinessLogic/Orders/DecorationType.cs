using VirtualGallery.Infrastructure.Localization;
namespace VirtualGallery.BusinessLogic.Orders
{
    public enum DecorationType
    {
        [LocalizedDescription(Localization.Names.Decoration_Type_Simple)]
        SimpleBox,
        [LocalizedDescription(Localization.Names.Decoration_Type_Frame)]
        Frame,
        [LocalizedDescription(Localization.Names.Decoration_Type_Lux)]
        Lux
    }
}
