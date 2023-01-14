using Ardalis.Specification;
using DigitalGoods.Core.Entities;

namespace DigitalGoods.Core.Specifications
{
    internal class CommentsForOfferSpec : Specification<Comment>
    {
        internal CommentsForOfferSpec(Offer offer)
        {
            Query.Where(c => c.Order.Offer.Equals(offer))
                .Include(c => c.Order)
                    .ThenInclude(o => o.Buyer);
        }
    }
}
