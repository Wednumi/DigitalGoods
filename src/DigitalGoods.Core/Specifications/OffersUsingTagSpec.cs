using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    public class OffersUsingTagSpec : Specification<Offer>
    {
        public OffersUsingTagSpec(Tag tag)
        {
            Query.Where(o => o.Tags.Contains(tag));
        }
    }
}
