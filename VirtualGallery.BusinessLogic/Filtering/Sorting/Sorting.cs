using System;
using System.Linq;

namespace VirtualGallery.BusinessLogic.Filtering.Sorting
{
    public class Sorting<TEntity>
    {
        public bool SortInMemory { get; set; }

        public Func<IQueryable<TEntity>, IQueryable<TEntity>> OrderByFilter { get; set; }

        public IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query)
        {
            if (OrderByFilter != null)
            {
                if (SortInMemory)
                {
                    query = query.ToList().AsQueryable();
                }

                return OrderByFilter.Invoke(query);
            }

            return query;
        }
    }
}