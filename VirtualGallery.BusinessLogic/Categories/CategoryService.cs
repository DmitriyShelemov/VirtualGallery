using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualGallery.BusinessLogic.Categories.Interfaces;
using VirtualGallery.BusinessLogic.Configuration;
using VirtualGallery.BusinessLogic.Filtering;
using VirtualGallery.BusinessLogic.Filtering.Sorting;
using VirtualGallery.BusinessLogic.UnitOfWork;

namespace VirtualGallery.BusinessLogic.Categories
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            IUnitOfWorkFactory unitOfWorkFactory, 
            ICategoryRepository categoryRepository)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _categoryRepository = categoryRepository;
        }

        public void Add(Category category)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                category.CreateDate = DateTime.UtcNow;
                category.UpdateDate = category.CreateDate;
                _categoryRepository.Add(category);
                unitOfWork.Commit();
            }
        }

        public void Update(Category category)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                category.UpdateDate = DateTime.UtcNow;
                _categoryRepository.Update(category);
                unitOfWork.Commit();
            }
        }

        public void Remove(Category category)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                _categoryRepository.Delete(category);
                unitOfWork.Commit();
            }
        }
        
        public IList<Category> Get(int page)
        {
            return _categoryRepository.GetAll(new GenericFilter<Category>
            {
                Sorting = new Sorting<Category>
                {
                    OrderByFilter = q => q.OrderBy(c => c.Name)
                },
                Take = AppSettings.PageSize,
                Skip = AppSettings.PageSize*Math.Max(0, page - 1)
            });
        }
        
        public Category GetById(int categoryId)
        {
            EnsureValidId(categoryId, "categoryId");
            return _categoryRepository.GetById(categoryId);
        }
    }
}
