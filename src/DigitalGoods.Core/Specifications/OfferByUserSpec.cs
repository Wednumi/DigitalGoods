using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using System.Collections;

namespace DigitalGoods.Core.Specifications
{
    internal class OfferByUserSpec : Specification<Offer>
    {
        internal OfferByUserSpec(User owner)
        {
            Query.Where(o => o.UserId.Equals(owner.Id));
        }
    }
}
