using System;
using System.Linq.Expressions;
using VirtualGallery.BusinessLogic.Filtering.Sorting;
using VirtualGallery.BusinessLogic.Filtering.Specification;

namespace VirtualGallery.BusinessLogic.Filtering
{
    public class GenericFilter<TObject>
    {
        public GenericFilter(params Expression<Func<TObject, bool>>[] specifications)
        {
            if (specifications == null)
            {
                return;
            }

            foreach (var predicate in specifications)
            {
                AddSpecification(new Specification<TObject>(predicate));
            }
        }

        public int? Take { get; set; }
        public int? Skip { get; set; }

        public Sorting<TObject> Sorting
        {
            get;
            set;
        }

        public Specification<TObject> Specification
        {
            get;
            set;
        }

        public void AddSpecification(Specification<TObject> newSpecification)
        {
            Specification = Specification == null ?
                            newSpecification :
                            Specification.And(newSpecification);
        }
    }
}