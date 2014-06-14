using System.Web;

namespace VirtualGallery.BusinessLogic.Preferences.Interfaces
{
    public interface IPreferenceService
    {
        void SetPhoto(HttpPostedFileBase photo);

        void Update(Preference preference);

        Preference Get();
    }
}
