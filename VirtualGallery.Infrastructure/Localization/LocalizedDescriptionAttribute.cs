using System.ComponentModel;

namespace VirtualGallery.Infrastructure.Localization
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        public LocalizedDescriptionAttribute(string key)
            : base( LocalizationManager.GetInstance().GetString(key) )
        {            
        }
    }
}
