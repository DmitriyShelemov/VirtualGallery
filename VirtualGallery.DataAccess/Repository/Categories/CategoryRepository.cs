using VirtualGallery.BusinessLogic.Categories;
using VirtualGallery.BusinessLogic.Categories.Interfaces;

namespace VirtualGallery.DataAccess.Repository.Categories
{
    public class CategoryRepository : BaseRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}
