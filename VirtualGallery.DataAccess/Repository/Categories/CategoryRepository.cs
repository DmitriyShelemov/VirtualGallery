using System;
using System.Linq;
using System.Data.Entity;
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

        public int GetMaxOrder()
        {
            return DbContext.Categories.Where(c => !c.Deleted).Max(c => (int?)c.Order).GetValueOrDefault(0);
        }

        public Category GetByOrder(int order)
        {
            return Find(c => c.Order == order && !c.Deleted).FirstOrDefault();
        }
    }
}
