using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Specifications.Filters;
using System.Linq;
using System.Linq.Expressions;

namespace DigitalGoods.Core.Specifications
{
    public class OfferByFilterSpec : Specification<Offer>
    {
        public OfferByFilterSpec(OfferFilter filter)
        {
            if(filter.Name is not null)
            {
                Query.Where(o => o.Name.Contains(filter.Name));
            }
            if(filter.BottomPrice is not null)
            {
                Query.Where(o => o.Price >= filter.BottomPrice);
            }
            if (filter.TopPrice is not null)
            {
                Query.Where(o => o.Price <= filter.TopPrice);
            }
            if(filter.CategoryTree is not null)
            {
                Query.Where(o => filter.CategoryTree.Any(c => c!.Equals(o.Category)));
            }
            if (filter.Tags is not null)
            {
                foreach(var tag in filter.Tags)
                {
                    Query.Where(o => o.Tags.Contains(tag));
                }
            }
            Query.Include(o => o.Medias.Where(m => m.IsPreview));
            Query.Where(o => o.State == Enums.OfferState.Active);
            SetOrdering(filter);
        }

        private void SetOrdering(OfferFilter filter)
        {
            if (filter.OrderExpressions is not null && filter.OrderExpressions.Count > 0)
            {
                var first = filter.OrderExpressions.First();
                var query = Query.OrderBy(first);
                for (int i = 1; i < filter.OrderExpressions.Count; i++)
                {
                    query.ThenBy(filter.OrderExpressions[i]);
                }
            }
        }
    }
}
