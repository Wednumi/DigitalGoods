using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class OffersUsingCategorySpec : Specification<Offer>
    {
        public OffersUsingCategorySpec(Category category)
        {
            Query.Where(o => o.Category != null && o.Category.Equals(category));
        }
    }
}
