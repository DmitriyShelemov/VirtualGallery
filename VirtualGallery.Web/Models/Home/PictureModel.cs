using System.ComponentModel.DataAnnotations;

namespace VirtualGallery.Web.Models.Home
{
    public class PictureModel : BasePictureModel
    {
        public string OriginalUrl { get; set; }

        [Required(AllowEmptyStrings = true)]
        public decimal PriceRouble { get; set; }

        [Required(AllowEmptyStrings = true)]
        public decimal PriceEuro { get; set; }

        [Required(AllowEmptyStrings = true)]
        public decimal PriceDollar { get; set; }

        public bool Topic { get; set; }
    }
}
