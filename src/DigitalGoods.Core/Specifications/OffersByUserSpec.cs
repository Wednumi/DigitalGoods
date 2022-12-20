using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using System.Collections;

namespace DigitalGoods.Core.Specifications
{
    public class OffersByUserSpec : Specification<Offer>
    {
        public OffersByUserSpec(User owner)
        {
            Query.Where(o => o.UserId.Equals(owner.Id));
        }
    }
}
