using System;
using System.Linq;
using VirtualGallery.BusinessLogic.Pictures;
using VirtualGallery.BusinessLogic.Pictures.Interfaces;

namespace VirtualGallery.DataAccess.Repository.Pictures
{
    public class PictureRepository : BaseRepository<Picture, int>, IPictureRepository
    {
        public PictureRepository(IDbContextProvider dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public string GetUniqueName(int categoryId, string name, string currentName = null)
        {
            var names =
                DbContext.Pictures.Where(m => m.Name.StartsWith(name) && m.CategoryId == categoryId)
                        .Select(m => m.Name.ToUpper())
                        .ToLookup(m => m);

            if (names.Any() && names.Contains(name.ToUpper()))
            {
                string uniqName;
                for (var i = 2;
                    names.Contains((uniqName = name + "(" + i + ")").ToUpper()) &&
                    !uniqName.Equals(currentName, StringComparison.InvariantCultureIgnoreCase);
                    i++) ;
                return uniqName;
            }

            return name;
        }
    }
}
