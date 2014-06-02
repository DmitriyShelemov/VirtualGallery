using System.Linq;
using VirtualGallery.BusinessLogic.Filtering.Extensions;

namespace VirtualGallery.BusinessLogic.Filtering.Specification
{
    public class OrSpecification<TEntity> : CompositeSpecification<TEntity>
    {
        public OrSpecification(Specification<TEntity> leftSide, Specification<TEntity> rightSide)
            : base(leftSide, rightSide)
        {
        }        

        public override TEntity SatisfyingEntityFrom(IQueryable<TEntity> query)
        {
            return this.SatisfyingEntitiesFrom(query).FirstOrDefault();
        }

        public override IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query)
        {
            return query.Where(this.LeftSide.Predicate.Or(this.RightSide.Predicate));
        }
    }
}
