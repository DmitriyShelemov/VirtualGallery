namespace VirtualGallery.BusinessLogic.Categories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
        int GetMaxOrder();

        Category GetByOrder(int order);
    }
}