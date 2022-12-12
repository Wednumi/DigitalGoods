using Ardalis.Specification;
using DigitalGoods.Core.Entities;
using System.Collections;

namespace DigitalGoods.Core.Specifications
{
    internal class OffersByUserSpec : Specification<Offer>
    {
        internal OffersByUserSpec(User owner)
        {
            Query.Where(o => o.UserId.Equals(owner.Id));
        }
    }
}
