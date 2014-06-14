using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VirtualGallery.Web.Models.About
{
    public class PhotoUploadButtonModel
    {
        [Required]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif,bmp", ErrorMessage = "Specify an image file.")]
        public HttpPostedFileBase File { get; set; }
    }
}