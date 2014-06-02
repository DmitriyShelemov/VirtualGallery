using System.Collections.Generic;

namespace VirtualGallery.Web.Models.Home
{
    public class PicturesModel
    {
        public int ContainerId { get; set; }

        public IList<PictureModel> Items { get; set; }
    }
}
