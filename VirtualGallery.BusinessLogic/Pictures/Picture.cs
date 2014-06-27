using VirtualGallery.BusinessLogic.Categories;
using VirtualGallery.BusinessLogic.StoredFiles;

namespace VirtualGallery.BusinessLogic.Pictures
{
    public class Picture
    {
        public const int MaxDescriptionLength = 500;
        public const int MaxFileSize = 20 * 1024 * 1024;

        public int Id { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public bool Topic { get; set; }

        public int FileId { get; set; }

        public virtual StoredFile File { get; set; }

        public int ThumbnailId { get; set; }

        public virtual StoredFile Thumbnail { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public decimal PriceRouble { get; set; }

        public decimal PriceEuro { get; set; }

        public decimal PriceDollar { get; set; }

        public bool Reserved { get; set; }

        public bool Sold { get; set; }
    }
}
