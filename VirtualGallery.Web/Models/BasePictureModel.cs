namespace VirtualGallery.Web.Models
{
    public class BasePictureModel
    {
        public int Id { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }
    }
}
