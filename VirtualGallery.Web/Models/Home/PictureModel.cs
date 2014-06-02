using System.ComponentModel.DataAnnotations;

namespace VirtualGallery.Web.Models.Home
{
    public class PictureModel
    {
        public int Id { get; set; }

        public string ThumbnailUrl { get; set; }

        public string OriginalUrl { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(AllowEmptyStrings = true)]
        public decimal PriceRouble { get; set; }

        [Required(AllowEmptyStrings = true)]
        public decimal PriceEuro { get; set; }

        [Required(AllowEmptyStrings = true)]
        public decimal PriceDollar { get; set; }

        public bool Topic { get; set; }
    }
}
