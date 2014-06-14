namespace VirtualGallery.BusinessLogic.Preferences.Interfaces
{
    public interface IPreferenceRepository : IBaseRepository<Preference, int>
    {
        Preference Get();
    }
}