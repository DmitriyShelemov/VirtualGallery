namespace VirtualGallery.Web.Models.Home
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PicturesModel Pictures { get; set; }
    }
}
