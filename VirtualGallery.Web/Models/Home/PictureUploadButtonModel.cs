using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VirtualGallery.Web.Models.Home
{
    public class PictureUploadButtonModel
    {
        public int CategoryId { get; set; }

        [Required]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif,bmp", ErrorMessage = "Specify an image file.")]
        public HttpPostedFileBase File { get; set; }
    }
}