using System.Linq;
using System.Data.Entity;
using VirtualGallery.BusinessLogic.Preferences;
using VirtualGallery.BusinessLogic.Preferences.Interfaces;

namespace VirtualGallery.DataAccess.Repository.Preferences
{
    public class PreferenceRepository : BaseRepository<Preference, int>, IPreferenceRepository
    {
        public PreferenceRepository(IDbContextProvider dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public Preference Get()
        {
            return
                DbContext.Preferences
                         .Include(s => s.Photo)
                         .FirstOrDefault();
        }
    }
}
