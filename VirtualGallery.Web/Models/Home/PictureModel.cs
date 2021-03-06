﻿using System.ComponentModel.DataAnnotations;

namespace VirtualGallery.Web.Models.Home
{
    public class PictureModel : BasePictureModel
    {
        public string OriginalUrl { get; set; }

        [Required(AllowEmptyStrings = true)]
        public int PriceRouble { get; set; }

        [Required(AllowEmptyStrings = true)]
        public int PriceEuro { get; set; }

        [Required(AllowEmptyStrings = true)]
        public int PriceDollar { get; set; }

        public bool Topic { get; set; }
    }
}
