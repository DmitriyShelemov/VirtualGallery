using VirtualGallery.BusinessLogic.Categories;
using VirtualGallery.BusinessLogic.StoredFiles;

namespace VirtualGallery.BusinessLogic.Preferences
{
    public class Preference
    {
        public const int MaxFileSize = 20 * 1024 * 1024;

        public int Id { get; set; }

        public string Intro { get; set; }

        public string About { get; set; }

        public int PhotoId { get; set; }

        public virtual StoredFile Photo { get; set; }
    }
}
