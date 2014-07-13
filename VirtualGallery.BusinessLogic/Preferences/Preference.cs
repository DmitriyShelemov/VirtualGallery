using VirtualGallery.BusinessLogic.StoredFiles;

namespace VirtualGallery.BusinessLogic.Preferences
{
    public class Preference
    {
        public const int MaxFileSize = 20 * 1024 * 1024;

        public int Id { get; set; }

        public string Intro { get; set; }

        public string About { get; set; }

        public string About2 { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string MapUrl { get; set; }

        public string WorkTime { get; set; }

        public int PhotoId { get; set; }

        public virtual StoredFile Photo { get; set; }

        public string DecorSimpleText { get; set; }

        public string DecorFrameText { get; set; }

        public string DecorLuxText { get; set; }
    }
}
