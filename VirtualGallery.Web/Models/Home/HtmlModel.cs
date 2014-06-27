using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VirtualGallery.Web.Models.Home
{
    public class HtmlModel
    {
        [AllowHtml] 
        public string Text { get; set; }
    }
}
