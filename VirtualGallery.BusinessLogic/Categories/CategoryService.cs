using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualGallery.BusinessLogic.Categories.Interfaces;
using VirtualGallery.BusinessLogic.Configuration;
using VirtualGallery.BusinessLogic.Filtering;
using VirtualGallery.BusinessLogic.Filtering.Sorting;
using VirtualGallery.BusinessLogic.Pictures.Interfaces;
using VirtualGallery.BusinessLogic.UnitOfWork;

namespace VirtualGallery.BusinessLogic.Categories
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        private readonly ICategoryRepository _categoryRepository;

        private readonly IPictureService _pictureService;

        public CategoryService(
            IUnitOfWorkFactory unitOfWorkFactory, 
            ICategoryRepository categoryRepository,
            IPictureService pictureService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _categoryRepository = categoryRepository;
            _pictureService = pictureService;
        }

        public void Add(Category category)
        {
            EnsureNotNull(category, "category");

            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                category.CreateDate = DateTime.UtcNow;
                category.UpdateDate = category.CreateDate;
                category.Order = _categoryRepository.GetMaxOrder() + 1;
                _categoryRepository.Add(category);
                unitOfWork.Commit();
            }

            _pictureService.CleanUp();
        }

        public void Update(Category category)
        {
            EnsureNotNull(category, "category");
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                category.UpdateDate = DateTime.UtcNow;
                _categoryRepository.Update(category);
                unitOfWork.Commit();
            }

            _pictureService.CleanUp();
        }

        public void Remove(Category category)
        {
            EnsureNotNull(category, "category");
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                category.Deleted = true;
                _categoryRepository.Update(category);
                unitOfWork.Commit();
            }

            _pictureService.CleanUp();
        }

        public void Move(Category category, bool up)
        {
            EnsureNotNull(category, "category");
            var newOrder = category.Order + (up ? -1 : 1);
            if (newOrder > 0 && newOrder <= _categoryRepository.GetMaxOrder())
            {
                var prev = _categoryRepository.GetByOrder(newOrder);
                using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    if (prev != null)
                    {
                        prev.UpdateDate = DateTime.UtcNow;
                        prev.Order = category.Order;
                        _categoryRepository.Update(prev);
                    }

                    category.UpdateDate = DateTime.UtcNow;
                    category.Order = newOrder;
                    _categoryRepository.Update(category);

                    unitOfWork.Commit();
                }
            }
        }
        
        public IList<Category> Get(int page)
        {
            return _categoryRepository.GetAll(new GenericFilter<Category>(c => !c.Deleted)
            {
                Sorting = new Sorting<Category>
                {
                    OrderByFilter = q => q.OrderBy(c => c.Order)
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
