using System.Collections.Generic;

namespace VirtualGallery.BusinessLogic.Categories.Interfaces
{
    public interface ICategoryService
    {
        void Add(Category category);

        void Update(Category category);

        void Remove(Category category);

        void Move(Category category, bool up);

        IList<Category> Get(int page);

        Category GetById(int categoryId);
    }
}
