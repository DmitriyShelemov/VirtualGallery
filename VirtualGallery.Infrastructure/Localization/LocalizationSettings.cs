namespace VirtualGallery.Infrastructure.Localization
{
    public class LocalizationSettings
    {
        public bool IsDefault { get; set; }

        public LanguageKey LanguageKey { get; set; }

        public string LocalizationName { get;  set; }

        public string CultureName { get; set; }

        public string ResourceName { get; set; }
    }
}
