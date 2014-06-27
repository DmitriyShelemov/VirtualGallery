using VirtualGallery.BusinessLogic.Pictures;

namespace VirtualGallery.BusinessLogic.Orders
{
    public class Lot
    {
        public int Id { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
