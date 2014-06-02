using System;
using System.Linq;
using System.Linq.Expressions;
using VirtualGallery.BusinessLogic.Filtering.Extensions;

namespace VirtualGallery.BusinessLogic.Filtering.Specification
{
    public class Specification<TEntity> : ISpecification<TEntity>
    {
        public Specification(Expression<Func<TEntity, bool>> predicate)
        {
            this.Predicate = predicate;
        }

        public Expression<Func<TEntity, bool>> Predicate { get; set; }

        public Specification<TEntity> And(Specification<TEntity> specification)
        {
            return new Specification<TEntity>(this.Predicate.And(specification.Predicate));
        }

        public Specification<TEntity> And(Expression<Func<TEntity, bool>> predicate)
        {
            return new Specification<TEntity>(this.Predicate.And(predicate));
        }    

        public Specification<TEntity> Or(Specification<TEntity> specification)
        {
            return new Specification<TEntity>(this.Predicate.Or(specification.Predicate));
        }

        public Specification<TEntity> Or(Expression<Func<TEntity, bool>> predicate)
        {
            return new Specification<TEntity>(this.Predicate.Or(predicate));
        }

        public IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query)
        {
            if (this.Predicate != null)
            {
                return query.Where(this.Predicate);
            }

            return query;
        }
    }
}
